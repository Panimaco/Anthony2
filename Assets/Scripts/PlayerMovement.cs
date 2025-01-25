using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Fuerzas")]
    [SerializeField]
    private float _recoilForce = 10f;
    [SerializeField]
    private float _shootForce = 5f;
    [SerializeField]
    private Vector2 _shootDirection;
    private Rigidbody2D _rb;

    [Header("Variables de Disparo")]
    [SerializeField]
    private GameObject _proyectilePrefab;
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private float _cadence;

    [Header("Detección de Suelo")]
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private bool _isOnGround;
    [SerializeField]
    private float _groundCheckRadius = 0.2f;

    private bool _canShoot = true;

    private Animator _anim;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();
        HandleInput();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _shootDirection = new Vector2(Mathf.Round(horizontal), Mathf.Round(vertical)).normalized;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        if (!_canShoot) yield break;
        _canShoot = false;

        if (_shootDirection == Vector2.zero)
        {
            _shootDirection = Vector2.right; // Dirección predeterminada si no hay entrada.
        }

        Vector2 recoilDirection = -_shootDirection.normalized;

        if (_isOnGround && (_shootDirection == Vector2.down || (_shootDirection.x != 0 && _shootDirection.y < 0)))
        {
            _rb.AddForce(recoilDirection * _recoilForce, ForceMode2D.Impulse);

            if (_shootDirection == Vector2.down)
            {
                SetTrigger("isJumping");
            }
            else if (_shootDirection.x < 0 && _shootDirection.y < 0)
            {
                SetTrigger("isJumpingToRight");
            }
            else if (_shootDirection.x > 0 && _shootDirection.y < 0)
            {
                SetTrigger("isJumpingToLeft");
            }
        }
        else
        {
            if (_shootDirection.x < 0 && _shootDirection.y == 0)
            {
                SetTrigger("isShootingPatras");
            }
            else if (_shootDirection.x > 0 && _shootDirection.y == 0)
            {
                SetTrigger("isShootingPalante");
            }
            else if (_shootDirection.x < 0 && _shootDirection.y > 0)
            {
                SetTrigger("isShootingPatrasArriba");
            }
            else if (_shootDirection.x > 0 && _shootDirection.y > 0)
            {
                SetTrigger("isShootingPalanteArriba");
            }
            else if (_shootDirection.x == 0 && _shootDirection.y > 0)
            {
                SetTrigger("isShootingArriba");
            }
        }

        // Instanciar el proyectil.
        GameObject projectile = Instantiate(_proyectilePrefab, _shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Proyectile>().Direction = _shootDirection;
        projectile.GetComponent<Proyectile>().Speed = _shootForce;

        yield return new WaitForSeconds(_cadence);
        _canShoot = true;
    }

    private void CheckGround()
    {
        _isOnGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        _anim.SetBool("isOnGround", _isOnGround);
    }

    private void SetTrigger(string triggerName)
    {
        _anim.ResetTrigger("isShootingPatras");
        _anim.ResetTrigger("isShootingPalante");
        _anim.ResetTrigger("isShootingPatrasArriba");
        _anim.ResetTrigger("isShootingPalanteArriba");
        _anim.ResetTrigger("isShootingArriba");

        _anim.SetTrigger(triggerName);
        Debug.Log($"Trigger activado: {triggerName}");
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
