using UnityEngine;
using System;

public class LinearEnemy : MonoBehaviour
{
    // Velocidad del enemigo.
    [SerializeField]
    private float _speed = 5f;

    // Distancia máxima de la cámara antes de ser destruido.
    [SerializeField]
    private float _destroyDistance = 20f;

    // Evento para notificar al spawner cuando se destruye.
    public Action OnDestroyed;

    // Dirección del enemigo al moverse.
    private Vector2 _moveDirection;

    private void Update()
    {
        // Mover al enemigo en línea recta en la dirección inicial.
        transform.position += (Vector3)_moveDirection * _speed * Time.deltaTime;

        // Destruir si está fuera del rango permitido.
        if (Mathf.Abs(transform.position.x) > _destroyDistance)
        {
            DestroyEnemy();
        }
    }

    // Inicializar la dirección hacia el jugador.
    public void Initialize(Vector3 playerPosition)
    {
        // Calcular la dirección inicial hacia el jugador, solo en el eje X.
        _moveDirection = new Vector2(Mathf.Sign(playerPosition.x - transform.position.x), 0f);
    }

    private void DestroyEnemy()
    {
        // Notificar al spawner que este enemigo ha sido destruido.
        OnDestroyed?.Invoke();

        // Destruir este objeto.
        Destroy(gameObject);
    }
}