using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACalcular : MonoBehaviour
{

    int firstN, secondN, intN, result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ACalcularFn("sum");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ACalcularFn("sub");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ACalcularFn("multi");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ACalcularFn("div");
        }
    }

    public void ACalcularFn(string operation)
    {
        firstN = Random.Range(1, 10);
        secondN = Random.Range(1, 10);

        if (firstN < secondN)
        {
            intN = secondN;
            secondN = firstN;
            firstN = intN;
        }

        if (operation == "sum")
        {
            result = firstN + secondN;
        }

        if (operation == "sub")
        {
            result = firstN - secondN;
        }

        if (operation == "multi")
        {
            result = firstN * secondN;
        }

        if (operation == "div")
        {
            result = firstN / secondN;
        }

        Debug.Log("fisrt: " + firstN + "; second: " + secondN + "; res = " + result);
    }
}
