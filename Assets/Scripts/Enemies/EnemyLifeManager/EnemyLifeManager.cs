using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public List<EnemyModel> Enemies;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            EnemyLoseLife();
        }
    }

    public void EnemyLoseLife()
    {

    }
}
