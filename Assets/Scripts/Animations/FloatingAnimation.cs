using System;
using UnityEditor;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _height = 0.3f;
    [SerializeField, Range(0f, 10f)] private float _speed = 0.3f;

    private float _startYPosition;

    private void Start()
    {
        _startYPosition = transform.position.y;
    }


    private void Update()
    {
        var yOffset = _startYPosition + transform.up.y * Mathf.PingPong(Time.time * _speed, _height);
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }
}
