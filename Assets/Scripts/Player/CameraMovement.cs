using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;

    private Vector3 _targetPreviousPosition;

    private void Start()
    {
        _targetPreviousPosition = _followingTarget.position;
    }

    private void Update()
    {
        var delta = (_followingTarget.position - _targetPreviousPosition);
        delta.y = 0;

        _targetPreviousPosition = _followingTarget.position;

        transform.position += delta;
    }
}
