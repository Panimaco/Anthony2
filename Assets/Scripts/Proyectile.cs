using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    //Zona de Variables Globales
    [Header("Referencias")]
    public float Speed = 20f;
    public float LifeTime = 2f;
    public Vector2 Direction;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Direction * Speed;
        Destroy(gameObject, LifeTime);
    }

    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        Destroy(gameObject);
    }
}
