using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Auth : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputLogin, _inputPassword;
    [SerializeField] private TMP_Text _errortext;

    

    public void clickButton()
    {
        if (!(DataBase.CheckUser(_inputLogin.text, _inputPassword.text)))
        {
            _errortext.text = "Такого пользователя не существует!";
        }
        else
        {
            User.Instance.Login = _inputLogin.text;
            User.Instance.Name = DataBase.GetName(_inputLogin.text);
            User.Instance.isTeacher = DataBase.CheckTeacher(_inputLogin.text, _inputPassword.text);
            if (!User.Instance.isTeacher)
            {
                User.Instance.classId = DataBase.GetClass(User.Instance.Login);
            }
            SceneManager.LoadScene("Menu");
            // if (DataBase.checkTeacher(_inputLogin.text, _inputPassword.text))
            // {
            //     _personalTeacher.SetActive(true);
            // }
            // else
            // {
            //     _personalSchoolKid.SetActive(true);
            // }
        }
    }
    
}
