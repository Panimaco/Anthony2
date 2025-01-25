using UnityEngine;

public class UpDownEnemy : MonoBehaviour
{
    // Punto A (abajo) donde comienza el enemigo.
    [SerializeField]
    private Transform _pointA;

    // Punto B (arriba) donde el enemigo se detiene.
    [SerializeField]
    private Transform _pointB;

    // Velocidad de movimiento del enemigo.
    [SerializeField]
    private float _moveSpeed = 2f;

    // Tiempo que el enemigo espera en el punto A.
    [SerializeField]
    private float _waitTimeAtA = 1f;

    // Tiempo que el enemigo espera en el punto B.
    [SerializeField]
    private float _waitTimeAtB = 1f;

    // Variables internas para manejar el estado del movimiento.
    private bool _movingUp = true;
    private bool _isWaiting = false;

    private void Update()
    {
        // Si está esperando, no se mueve.
        if (_isWaiting) return;

        // Determinar hacia dónde moverse según el estado (_movingUp).
        if (_movingUp)
        {
            MoveTowardsPoint(_pointB.position, _waitTimeAtB);
        }
        else
        {
            MoveTowardsPoint(_pointA.position, _waitTimeAtA);
        }
    }

    private void MoveTowardsPoint(Vector3 target, float waitTime)
    {
        // Movimiento hacia el punto objetivo.
        transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);

        // Si el enemigo ha alcanzado el punto objetivo.
        if (Vector3.Distance(transform.position, target) <= 0.01f)
        {
            // Cambiar el estado de movimiento.
            _movingUp = !_movingUp;

            // Esperar en el punto alcanzado.
            StartCoroutine(WaitAtPoint(waitTime));
        }
    }

    private System.Collections.IEnumerator WaitAtPoint(float waitTime)
    {
        _isWaiting = true; // Detener el movimiento.
        yield return new WaitForSeconds(waitTime); // Esperar el tiempo especificado.
        _isWaiting = false; // Reanudar el movimiento.
    }
}
