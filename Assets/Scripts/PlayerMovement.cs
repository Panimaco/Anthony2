using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Zona de Variables Globales
    [Header("Fuerzas")]
    [SerializeField]
    private float _recoilForce = 10f;
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

    [Header("Variables para Detection del Suelo")]
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private bool _isOnGround;
    [SerializeField]
    private float _groundCheckRadius = 0.2f;

    private bool _canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckGround();
        ImputPlayer();
    }

    private void ImputPlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _shootDirection = new Vector2(Mathf.Round(horizontal), Mathf.Round(vertical)).normalized;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Shoot());
        }
    }

    
    IEnumerator Shoot()
    {
        if (!_canShoot) yield break; // Salimos si ya estamos disparando.
        _canShoot = false;

        Vector2 recoilDirection = -_shootDirection.normalized;

        if (_isOnGround && (_shootDirection == Vector2.down || (_shootDirection.x != 0 && _shootDirection.y < 0)))
        {
            _rb.AddForce(recoilDirection * _recoilForce, ForceMode2D.Impulse);
        }

        GameObject projectile = Instantiate(_proyectilePrefab, _shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Proyectile>().Direction = _shootDirection;

        yield return new WaitForSeconds(_cadence); // Tiempo de espera.

        _canShoot = true; // Permitir disparar de nuevo.
    }


    private void CheckGround()
    {
        _isOnGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }
    private void OnDrawGizmos()
    {
        if(_groundCheck != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
