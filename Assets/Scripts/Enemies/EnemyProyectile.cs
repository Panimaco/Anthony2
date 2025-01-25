using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float _lifetime = 5f; // Tiempo de vida del proyectil antes de destruirse.

    private void Start()
    {
        Destroy(gameObject, _lifetime); // Destruir el proyectil después de un tiempo.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Aquí puedes añadir lógica adicional, como daño al jugador o efectos.
        Destroy(gameObject); // Destruir el proyectil al chocar con algo.
    }
}
