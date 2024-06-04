using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlayer : MonoBehaviour
{

    [SerializeField] KeyCode _movingForward, _movingBack, _movingRight, _movingLeft;
    [SerializeField] Vector3 _movingForwardBack, _movingRightLeft;

    private void FixedUpdate()
    {
        if (UnityEngine.Input.GetKey(_movingForward))
        {
            GetComponent<Rigidbody>().velocity += _movingForwardBack;
        }
        if (UnityEngine.Input.GetKey(_movingBack))
        {
            GetComponent<Rigidbody>().velocity -= _movingForwardBack;
        }
        if (UnityEngine.Input.GetKey(_movingLeft))
        {
            GetComponent<Rigidbody>().velocity -= _movingRightLeft;
        }
        if (UnityEngine.Input.GetKey(_movingRight))
        {
            GetComponent<Rigidbody>().velocity += _movingRightLeft;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("FirstExample");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
