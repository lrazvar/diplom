using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] public bool Plus, Minus, Div, Multi = false;
    // Start is called before the first frame update
    public void isActualPlus()
    {
        Plus = true;
    }
    
    public void isActualMinus()
    {
        Minus = true;
    }
    
    public void isActualDiv()
    {
        Div = true;
    }
    
    public void isActualMulti()
    {
        Multi = true;
    }
}
