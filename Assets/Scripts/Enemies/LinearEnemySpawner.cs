using UnityEngine;

public class LinearEnemySpawner : MonoBehaviour
{
    // Prefab del enemigo.
    [SerializeField]
    private GameObject _enemyPrefab;

    // Transform donde puede aparecer el enemigo.
    [SerializeField]
    private Transform[] _spawnPoints;

    // Referencia al jugador.
    [SerializeField]
    private Transform _player;

    // Tiempo entre spawns.
    [SerializeField]
    private float _spawnInterval = 3f;

    // Máximo número de enemigos en pantalla.
    [SerializeField]
    private int _maxEnemies = 5;

    // Lista de enemigos actuales en la escena.
    private int _currentEnemyCount = 0;

    private void Start()
    {
        // Comenzar a generar enemigos periódicamente.
        InvokeRepeating(nameof(SpawnEnemy), 0f, _spawnInterval);
    }

    private void SpawnEnemy()
    {
        // Verificar si ya hay demasiados enemigos.
        if (_currentEnemyCount >= _maxEnemies) return;

        // Seleccionar un punto de spawn aleatorio.
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        // Instanciar al enemigo.
        GameObject enemy = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Configurar la dirección del enemigo hacia el jugador.
        enemy.GetComponent<LinearEnemy>().Initialize(_player.position);

        // Incrementar el conteo de enemigos.
        _currentEnemyCount++;

        // Escuchar cuando el enemigo sea destruido para restar del conteo.
        enemy.GetComponent<LinearEnemy>().OnDestroyed += HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed()
    {
        _currentEnemyCount--;
    }
}
