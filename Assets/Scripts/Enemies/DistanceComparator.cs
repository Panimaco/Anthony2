using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceComparator : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GiantCrabBoss _giantCrabBoss;
    private void Awake()
    {
        _giantCrabBoss.gameObject.SetActive(false);
    }
    void Update()
    {
        if(transform.position.x - _player.position.x > 4)
        {
            if (_giantCrabBoss.gameObject.activeSelf)
            {
                _giantCrabBoss.gameObject.SetActive(false);
            }
        }
        else
        {
            _giantCrabBoss.gameObject.SetActive(true);
        }
    }
}
