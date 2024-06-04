using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RegistrationKids : MonoBehaviour
{
    [SerializeField] private TMP_InputField _login, _password, _name;
    [SerializeField] private TMP_Dropdown _class;
    [SerializeField] private TMP_Text _feedback;
    [SerializeField] private GameObject _menu;
    
    
    void Start()
    {
        List<string> classes = DataBase.GetClasses();
        _class.ClearOptions();
        _class.AddOptions(classes);
    }

    public void ClickButton()
    {
        if (string.IsNullOrEmpty(_login.text) || string.IsNullOrEmpty(_password.text) ||
            string.IsNullOrEmpty(_name.text))
        {
            _feedback.text = "Заполнены не все поля!";
            StartCoroutine(ClearFeedback(3f));
        }
        else
        {
            if (DataBase.CheckDoubleLogin(_login.text))
            {
                _feedback.text = "Пользователь с таким логином уже существует!";
                StartCoroutine(ClearFeedback(3f));
            }
            else
            {
                int selectedIndex = _class.value;
                string selectedClass = _class.options[selectedIndex].text;
                int idClass = DataBase.GetIdClass(selectedClass);
                DataBase.CreateUser(_login.text, _password.text, _name.text, false, idClass);
                _feedback.text = "Ученик добавлен!";
                StartCoroutine(ClearFeedback(3f));
            }
        }
    }

    IEnumerator ClearFeedback(float delay)
    {
        yield return new WaitForSeconds(delay);
        _feedback.text = "";
    }

    public void BackToMenu()
    {
        gameObject.SetActive(false);
        _menu.SetActive(true);
    }
}
