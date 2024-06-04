using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMove : MonoBehaviour
{
    private bool move, finish;
    private Vector2 mousePosition;
    private float startPosY, startPosX;
    [SerializeField] private GameObject _figure;
    [SerializeField] private FigureLvl _figureLvl;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            move = true;
            mousePosition = Input.mousePosition;
            startPosX = mousePosition.x - this.transform.localPosition.x;
            startPosY = mousePosition.y - this.transform.localPosition.y;
        }
    }

    void OnMouseUp()
    {
        move = false;
        if (Math.Abs(transform.localPosition.x - _figure.transform.localPosition.x) <= 5f &&
            Math.Abs(transform.localPosition.y - _figure.transform.localPosition.y) <= 5f)
        {
            gameObject.transform.position = new Vector3(_figure.transform.position.x, _figure.transform.position.y, _figure.transform.position.z);
            finish = true;
            _figureLvl.AddScore();
        }
            
    }
    
    
    private void Update()
    {
       if (move && !finish)
       {
            mousePosition = Input.mousePosition;
            gameObject.transform.localPosition = new Vector3(mousePosition.x - startPosX, mousePosition.y - startPosY, 0f);
       }
    }
}
