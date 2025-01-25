using UnityEngine;

public class FlyingEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _flyingEnemyPrefab; // Prefab del enemigo volador.

    [SerializeField]
    private Transform[] _spawnPoints; // Puntos de spawn de los enemigos.

    [SerializeField]
    private Transform _player; // Referencia al jugador.

    [SerializeField]
    private int _maxEnemies = 5; // Máxima cantidad de enemigos simultáneos.

    [SerializeField]
    private float _spawnInterval = 3f; // Tiempo entre spawns.

    private int _currentEnemyCount = 0; // Conteo actual de enemigos en la escena.

    private void Start()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de spawn asignados.");
            return;
        }

        if (_flyingEnemyPrefab == null)
        {
            Debug.LogError("El prefab del enemigo volador no está asignado.");
            return;
        }

        if (_player == null)
        {
            Debug.LogError("El jugador no está asignado.");
            return;
        }

        // Iniciar la generación de enemigos.
        StartCoroutine(SpawnEnemies());
    }

    private System.Collections.IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Esperar el intervalo de tiempo antes de intentar spawnear.
            yield return new WaitForSeconds(_spawnInterval);

            // Solo spawnear si no hemos alcanzado el máximo de enemigos.
            if (_currentEnemyCount < _maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        // Elegir un punto de spawn aleatorio.
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        // Instanciar el enemigo en la posición del punto de spawn.
        GameObject enemy = Instantiate(_flyingEnemyPrefab, spawnPoint.position, Quaternion.identity);

        // Configurar el enemigo.
        FlyingEnemy flyingEnemy = enemy.GetComponent<FlyingEnemy>();
        if (flyingEnemy != null)
        {
            flyingEnemy.Initialize(_player); // Pasar la referencia del jugador al enemigo.
            flyingEnemy.OnDestroyed += HandleEnemyDestroyed; // Suscribir al evento OnDestroyed.
        }

        // Incrementar el contador de enemigos.
        _currentEnemyCount++;

        Debug.Log($"Enemigo volador spawneado en: {spawnPoint.position}");
    }

    private void HandleEnemyDestroyed()
    {
        // Decrementar el contador de enemigos al ser destruido.
        _currentEnemyCount--;
    }
}
