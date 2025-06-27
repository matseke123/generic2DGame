using Unity.VisualScripting;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private float _offset;
    [SerializeField] private float _followSpeed = 2f;
    private Vector3 _targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _offset = transform.position.z - _playerTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition = new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z + _offset);
        Vector3.MoveTowards(transform.position, _targetPosition, _followSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _followSpeed * Time.deltaTime);
        this.transform.position = new Vector3(transform.position.x, transform.position.y, _targetPosition.z);

    }
}
