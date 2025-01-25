using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionManager : MonoBehaviour
{
    [SerializeField]
    HealthManager healthManager;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Tan dado");
            healthManager.LoseHealth();
        }
    }
}
