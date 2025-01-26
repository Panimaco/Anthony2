using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform _actualCheckpoint;

    [SerializeField]
    private Transform _firstCheckpoint;
    [SerializeField]
    private Transform _secondCheckpoint;
    [SerializeField]
    private Transform _thirdCheckpoint;
    [SerializeField]
    private Transform _fourthCheckpoint;

    private void Update()
    {
        CheckPointChecker();
    }
    private void SetCheckpoint(Transform checkpoint)
    {
        _actualCheckpoint = checkpoint;
    }
    private void CheckPointChecker() 
    {
        if (transform.position.x < _firstCheckpoint.position.x)
        {
            return;
        }
        if (transform.position.x > _firstCheckpoint.position.x && transform.position.x < _secondCheckpoint.position.x)
        {
            SetCheckpoint(_firstCheckpoint);
        }
        if (transform.position.x > _secondCheckpoint.position.x && transform.position.x < _thirdCheckpoint.position.x)
        {
            SetCheckpoint(_secondCheckpoint);
        }
        if (transform.position.x > _thirdCheckpoint.position.x && transform.position.x < _fourthCheckpoint.position.x)
        {
            SetCheckpoint(_thirdCheckpoint);
        }
        if (transform.position.x > _fourthCheckpoint.position.x)
        {
            SetCheckpoint(_fourthCheckpoint);
        }
    }
    public void TeleportToCheckPoint()
    {
        transform.position = new Vector3(_actualCheckpoint.position.x, _actualCheckpoint.position.y, transform.position.z);
    }
}
