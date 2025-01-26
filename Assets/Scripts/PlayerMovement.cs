using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Fuerzas")]
    [SerializeField]
    private float _maxRecoilForce = 6f; // Fuerza máxima de impulso
    [SerializeField]
    private float _shootForce = 5f; // Fuerza constante del proyectil
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

    [SerializeField, Range(0, 2)]
    private float _chargeTime = 0f; // Tiempo de carga del disparo (mostrado en Inspector)

    [SerializeField, Range(2, 6)]
    private float _currentRecoilForce = 1f; // Fuerza de retroceso actual (mostrada en Inspector)

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

        // Iniciar carga al presionar el botón
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _chargeTime = 0f;
        }

        // Incrementar carga mientras el botón esté pulsado (máximo 4 segundos)
        if (Input.GetKey(KeyCode.Z))
        {
            _chargeTime += Time.deltaTime;
            _chargeTime = Mathf.Min(_chargeTime, 2f); // Limitar la carga a 4 segundos

            // Calcular la fuerza actual de retroceso (entre 1 y _maxRecoilForce)
            _currentRecoilForce = Mathf.Lerp(2f, _maxRecoilForce, _chargeTime / 2f);
        }

        // Disparar al soltar el botón
        if (Input.GetKeyUp(KeyCode.Z))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        if (!_canShoot) yield break;
        _canShoot = false;

        // Usar la fuerza de retroceso calculada
        float recoilForce = _currentRecoilForce;

        if (_shootDirection == Vector2.zero)
        {
            _shootDirection = Vector2.right;
        }

        Vector2 recoilDirection = -_shootDirection.normalized;

        // Propulsión solo si la dirección del disparo es abajo, abajo-izquierda o abajo-derecha
        if (_isOnGround && (_shootDirection == Vector2.down || (_shootDirection.x != 0 && _shootDirection.y < 0)))
        {
            _rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        }

        // Instanciar el proyectil
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

    private void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
