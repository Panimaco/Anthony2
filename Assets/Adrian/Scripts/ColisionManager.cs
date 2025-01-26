using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionManager : MonoBehaviour
{
    [SerializeField]
    //Manager de salud y vida
    HealthManager healthManager;

    //Funci�n de colisi�n con enemigos
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Compara tag de Enemigo
        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Tan dado");
            healthManager.LoseHealth();
        }
    }
}
