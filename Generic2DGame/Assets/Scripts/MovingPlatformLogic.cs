using System;
using UnityEngine;

public class MovingPlatformLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject _platform;
    [SerializeField] private float speed = 2f;

    private int _index = 0;
    private float _cooldown = 1f;
    private float _timer = 0f;
    private bool _movingRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_movingRight)
        {
            _platform.transform.position = Vector3.MoveTowards(_platform.transform.position, waypoints[_index].position, speed * Time.deltaTime);
            if (_platform.transform.position == waypoints[_index].position)
            {
                _index++;
            }
        }
        if (!_movingRight)
        {
            _platform.transform.position = Vector3.MoveTowards(_platform.transform.position, waypoints[_index].position, speed * Time.deltaTime);
            if (_platform.transform.position == waypoints[_index].position)
            {
                _index--;
            }
        }
        if(_index >= waypoints.Length - 1)
        {
            _movingRight = false;
        }
        if(_index <= 0)
        {
            _movingRight = true;
            _index = 0;
        }

    }
}
