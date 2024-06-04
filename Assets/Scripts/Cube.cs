using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "CubePlus")
        {
            FindObjectOfType<CubeManager>().isActualPlus();
        } 
        if (gameObject.name == "CubeMinus")
        {
            FindObjectOfType<CubeManager>().isActualMinus();
        } 
        if (gameObject.name == "CubeDiv")
        {
            FindObjectOfType<CubeManager>().isActualDiv();
        } 
        if (gameObject.name == "CubeMulti")
        {
            FindObjectOfType<CubeManager>().isActualMulti();
        } 
        Destroy(gameObject);
    }
}
