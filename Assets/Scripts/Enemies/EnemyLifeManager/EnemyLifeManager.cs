using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLifeManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public SpriteRenderer _sR;
    public Color _savedColor;

    void Start()
    {
        _sR = this.gameObject.GetComponent<SpriteRenderer>();
        _savedColor = _sR.color;
        if (this.gameObject.name == "FlyingEnemy")
        {
            maxHealth = 1;
            Debug.Log("El enemigo es volador");
            currentHealth = maxHealth;
        }
        else if (this.gameObject.name == "UpDownEnemy") 
        {
            maxHealth = 1;
            Debug.Log("El enemigo es arriba y abajo");
            currentHealth = maxHealth;
        }
        else if (this.gameObject.name == "MoveStraight")
        {
            maxHealth = 2;
            Debug.Log("El enemigo es rata");
            currentHealth = maxHealth;
        }

        
    }
    public void LoseEnemyLife()
    {
        _sR.color = Color.red;
        _sR.color = _savedColor;
        currentHealth--;
        Debug.Log("Se ha restado vida");
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy is death");
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("Ha habido choque");
            LoseEnemyLife();
        }
    }
}
