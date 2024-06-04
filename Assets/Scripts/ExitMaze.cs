using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMaze : MonoBehaviour
{
    private CubeManager _cubeManager;
    private void OnTriggerEnter(Collider other)
    {
        _cubeManager = FindObjectOfType<CubeManager>();
        string sceneName = SceneManager.GetActiveScene().name;
        Player player = other.attachedRigidbody.GetComponent<Player>();
        if (player)
        {
            if (_cubeManager != null)
            {
                User.Instance.div = _cubeManager.Div;
                User.Instance.multi = _cubeManager.Multi;
                User.Instance.plus = _cubeManager.Plus;
                User.Instance.minus = _cubeManager.Minus;
            }
            if (sceneName.Equals("Labyrinth lvl1"))
            {
                SceneManager.LoadScene("MiniGame");
            }
            else if (sceneName.Equals("Labyrinth lvl2"))
            {
                SceneManager.LoadScene("MiniGameFraction");
            }
            else
            {
                SceneManager.LoadScene("MiniGameGeometry");
            }
                
        }
    }
}
