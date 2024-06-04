using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rating : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _class;
    [SerializeField] private TMP_Text _rating;

    private void Start()
    {
        int idClass;
        if (_class != null)
        {
            List<string> classes = DataBase.GetClasses();
            _class.ClearOptions();
            _class.AddOptions(classes);
            int selectedIndex = _class.value;
            string selectedClass = _class.options[selectedIndex].text;
            idClass = DataBase.GetIdClass(selectedClass);
            _class.onValueChanged.AddListener(delegate {
                OnDropdownValueChanged(_class);
            });
        }
        else
        {
            idClass = User.Instance.classId;
        }
        _rating.text = DataBase.Rating(idClass);
    }
    
    private void OnDropdownValueChanged(TMP_Dropdown change)
    {
        int selectedIndex = _class.value;
        string selectedClass = _class.options[selectedIndex].text;
        int idClass = DataBase.GetIdClass(selectedClass);

        // Отправляем запрос на сервер базы данных
        _rating.text = DataBase.Rating(idClass);
    }

    public void CloseRating()
    {
        gameObject.SetActive(false);
    }
    
}
