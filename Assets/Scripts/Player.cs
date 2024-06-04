using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float Speed = 2;
    private float _startEulerY;
    private float _targetEulerY;
    private Rigidbody componentRigidbody;
    public float RotationSpeed = 5;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
        _startEulerY = transform.rotation.eulerAngles.y;
        _targetEulerY = _startEulerY;
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
            movement += Vector3.left;
            _targetEulerY = _startEulerY - 90f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
            movement += Vector3.right;
            _targetEulerY = _startEulerY + 90f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * Speed * Time.deltaTime;
            movement += Vector3.forward;
            _targetEulerY = _startEulerY;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * Speed * Time.deltaTime;
            movement += Vector3.back;
            _targetEulerY = _startEulerY + 180f;
        }

        // Плавное изменение текущего угла поворота к целевому
        float currentEulerY = Mathf.LerpAngle(transform.rotation.eulerAngles.y, _targetEulerY, RotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, currentEulerY, 0f);
        bool isRunning = movement.magnitude > 0;
        _animator.SetBool("Run", isRunning);
        
    }
}
