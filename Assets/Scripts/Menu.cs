using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _personalTeacher, _personalSchoolKid, _registrationKid, _ratingTeacher, _ratingKid;
    [SerializeField] private TMP_Text _teacherLabel;
    [SerializeField] private TMP_Text _kidLabel;
    private void Start()
    {
        if (User.Instance.isTeacher)
        {
            _teacherLabel.text = "Добро пожаловать, " + User.Instance.Name + "!";
            _personalTeacher.SetActive(true);
        }
        else
        {
            _kidLabel.text = "Добро пожаловать, " + User.Instance.Name + "!";
            _personalSchoolKid.SetActive(true);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    public void StartLvl1()
    {
        SceneManager.LoadScene("Labyrinth lvl1");
    }
    
    public void StartLvl2()
    {
        SceneManager.LoadScene("Labyrinth lvl2");
    }
    
    public void StartLvl3()
    {
        SceneManager.LoadScene("Labyrinth lvl3");
    }

    public void ClickReg()
    {
        _personalTeacher.SetActive(false);
        _registrationKid.SetActive(true);
    }

    public void ClickRatingTeacher()
    {
        _ratingTeacher.SetActive(true);
    }
    
    public void ClickRatingKid()
    {
        _ratingKid.SetActive(true);
    }

}
