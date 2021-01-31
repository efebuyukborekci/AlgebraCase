using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRatio : MonoBehaviour
{
    private Camera _cam;
    public SpriteRenderer boundry;
    public float orthographicSize;

    private float _screenRatio;
    private float _targetRatio;
    private float _difference;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        SetScale();
    }

    private void SetScale()
    {
        _screenRatio = (float)Screen.width / (float)Screen.height;
        var bounds = boundry.bounds;
        _targetRatio = bounds.size.x / bounds.size.y;

        if (_screenRatio >= _targetRatio)
        {
            orthographicSize = boundry.bounds.size.y / 2;
        }
        else
        {
            _difference = _targetRatio / _screenRatio;
            orthographicSize = boundry.bounds.size.y / 2 * _difference;
        }

        _cam.orthographicSize = orthographicSize;
    }
}
