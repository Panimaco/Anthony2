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

    [SerializeField]
    private float _timeToDestroy = 0.1f;

    private Animator _anim;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Direction * Speed;
        _anim = GetComponent<Animator>();
        Destroy(gameObject, LifeTime);
    }

    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if (infoCollision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            _anim.SetTrigger("Impact");
            Destroy(gameObject, _timeToDestroy);
        }
    }
}
