using UnityEngine;
using System;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    private float _moveDistance = 2f; // Distancia que se mueve cada vez.

    [SerializeField]
    private float _moveSpeed = 3f; // Velocidad de movimiento.

    [SerializeField]
    private float _destroyDistance = 20f; // Distancia desde la cámara para destruir.

    [SerializeField]
    private GameObject _projectilePrefab; // Prefab del proyectil.

    [SerializeField]
    private Transform _shootPoint; // Punto desde donde se spawnea el proyectil.

    [SerializeField]
    private float _projectileSpeed = 5f; // Velocidad del proyectil.

    public Action OnDestroyed; // Evento para notificar cuando el enemigo se destruya.

    private Transform _player; // Referencia al jugador.
    private bool _isMoving = false; // Estado interno de movimiento.

    private void Update()
    {
        // Destruir si está fuera del rango permitido.
        if (Mathf.Abs(transform.position.x) > _destroyDistance)
        {
            DestroyEnemy();
        }
    }

    public void Initialize(Transform player)
    {
        _player = player; // Asignar referencia al jugador.
        StartNextMove(); // Iniciar el primer movimiento.
    }

    private void StartNextMove()
    {
        if (_isMoving) return; // Evitar que se superpongan movimientos.

        _isMoving = true;

        // Determinar la dirección hacia el jugador (solo en X).
        float direction = Mathf.Sign(_player.position.x - transform.position.x);

        // Calcular el destino del movimiento.
        Vector3 targetPosition = transform.position + new Vector3(direction * _moveDistance, 0, 0);

        // Iniciar el movimiento.
        StartCoroutine(MoveToTarget(targetPosition));
    }

    private System.Collections.IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // Moverse hacia el objetivo.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Finalizar movimiento.
        _isMoving = false;

        // Disparar proyectil.
        ShootProjectile();

        // Iniciar el siguiente movimiento.
        StartNextMove();
    }

    private void ShootProjectile()
    {
        if (!_projectilePrefab || !_shootPoint) return; // Verificar que el prefab y el punto de disparo existan.

        // Crear el proyectil en el punto de disparo.
        GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);

        // Calcular dirección hacia el jugador.
        Vector2 direction = (_player.position - transform.position).normalized;

        // Configurar la dirección y velocidad del proyectil.
        projectile.GetComponent<Rigidbody2D>().velocity = direction * _projectileSpeed;
    }

    private void DestroyEnemy()
    {
        // Notificar al spawner.
        OnDestroyed?.Invoke();

        // Destruir este objeto.
        Destroy(gameObject);
    }
}