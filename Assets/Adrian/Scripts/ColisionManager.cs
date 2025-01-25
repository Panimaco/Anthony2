using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionManager : MonoBehaviour
{
    [SerializeField]
    //Manager de salud y vida
    HealthManager healthManager;

    //Función de colisión con enemigos
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Compara tag de Enemigo
<<<<<<< Updated upstream
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyObstacle"))
=======
        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("EnemyBullet"))
>>>>>>> Stashed changes
        {
            Debug.Log("Tan dado");
            healthManager.LoseHealth();
        }
    }
}
