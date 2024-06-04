using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TMP_Text _plus, _minus, _div, _multi;

    [SerializeField] private TMP_Text _firstNumber, _secondNumber;
    [SerializeField] private TMP_Text _timerText, _scoreText;
    [SerializeField] private TMP_Text _timerBeforeBegin, _textBeforeBegin;

    [SerializeField] private GameObject _timerObject, _answerObject;
    [SerializeField] private TMP_InputField _answer;
    [SerializeField] private GameObject _finishGame;
    [SerializeField] private TMP_Text _results;

    private int firstNum, secondNum;
    private char operation;
    private float timer = 60f;
    private int score = 0;
    private int scoreCoef = 1;
    private List<char> availableOperations = new List<char>();

    private void Start()
    {
        GenerateOptions();
        _scoreText.text = score.ToString();
        StartCoroutine(StartCountdown());
    }

    
    
    
    private IEnumerator StartCountdown()
    {
        // Начнем отсчет обратного времени с 3
        for (int count = 3; count > 0; count--)
        {
            _timerBeforeBegin.text = count.ToString();
            yield return new WaitForSeconds(1f); // Ждем 1 секунду
        }

        _textBeforeBegin.text = "";
        _timerBeforeBegin.text = "ПОЕХАЛИ!"; // После отсчета выводим сообщение "ПОЕХАЛИ!"
        yield return new WaitForSeconds(1f);// Ждем 1 секунду перед началом уровня
        _timerBeforeBegin.text = "";

        StartCoroutine(MainTimer());
        GenerateQuestion();
        _answerObject.SetActive(true);
        _timerObject.SetActive(true);
        
       
    }
    
    private IEnumerator MainTimer()
    {
        // Основной таймер начинает работать
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();
            yield return null;
        }

        // Когда время вышло
        _answerObject.SetActive(false);
        _timerObject.SetActive(false);
        Debug.Log("Время вышло!");
        _scoreText.text = "";
        _results.text = "ТВОЙ РЕЗУЛЬТАТ: " + score;
        _finishGame.SetActive(true);
    }

    private void GenerateOptions()
    {
        availableOperations.Add('+');
        if (User.Instance.minus)
        {
            availableOperations.Add('-');
            scoreCoef++;
        }

        if (User.Instance.div)
        {
            availableOperations.Add('/');
            scoreCoef++;
        }

        if (User.Instance.multi)
        {
            availableOperations.Add('*');
            scoreCoef++;
        }
    }
    
    private void GenerateQuestion()
    {
        int opIndex = Random.Range(0, availableOperations.Count);
        operation = availableOperations[opIndex];
        
        switch (operation)
        {
            case '+':
                firstNum = Random.Range(1, 11);
                secondNum = Random.Range(1, 11);
                _plus.gameObject.SetActive(true);
                _minus.gameObject.SetActive(false);
                _div.gameObject.SetActive(false);
                _multi.gameObject.SetActive(false);
                break;
            case '-':
                secondNum = Random.Range(1, firstNum);
                firstNum = Random.Range(secondNum, 11);
                _plus.gameObject.SetActive(false);
                _minus.gameObject.SetActive(true);
                _div.gameObject.SetActive(false);
                _multi.gameObject.SetActive(false);
                break;
            case '/':
                // Генерируем числа таким образом, чтобы результат был целым числом
                secondNum = Random.Range(1, 11);
                firstNum = Random.Range(1, 11) * secondNum;
                _plus.gameObject.SetActive(false);
                _minus.gameObject.SetActive(false);
                _div.gameObject.SetActive(true);
                _multi.gameObject.SetActive(false);
                break;
            case '*':
                firstNum = Random.Range(1, 11);
                secondNum = Random.Range(1, 11);
                _plus.gameObject.SetActive(false);
                _minus.gameObject.SetActive(false);
                _div.gameObject.SetActive(false);
                _multi.gameObject.SetActive(true);
                break;
        }

        // Отображаем числа на экране
        _firstNumber.text = firstNum.ToString();
        _secondNumber.text = secondNum.ToString();
    }

    public void CheckAnswer()
    {
        // Получаем ответ пользователя и преобразуем его в число
        int userAnswer = int.Parse(_answer.text);

        // Вычисляем правильный ответ
        int correctAnswer = 0;
        switch (operation)
        {
            case '+':
                correctAnswer = firstNum + secondNum;
                break;
            case '-':
                correctAnswer = firstNum - secondNum;
                break;
            case '/':
                correctAnswer = firstNum / secondNum;
                break;
            case '*':
                correctAnswer = firstNum * secondNum;
                break;
        }

        // Проверяем ответ пользователя
        if (userAnswer == correctAnswer)
        {
            score += scoreCoef; // Увеличиваем количество баллов при правильном ответе
            UpdateScoreDisplay(); // Обновляем отображение баллов
        }
        
        GenerateQuestion();
        _answer.text = "";
    }
    private void UpdateTimerDisplay()
    {
        // Преобразуем таймер в секунды и миллисекунды
        int seconds = Mathf.FloorToInt(timer);
        int milliseconds = Mathf.FloorToInt((timer - seconds) * 100);

        // Форматируем текст для отображения в формате "секунды:миллисекунды"
        string timerString = string.Format("{0:00},{1:00}", seconds, milliseconds);

        // Обновляем текстовое поле с таймером
        _timerText.text = timerString;
    }
    
    private void UpdateScoreDisplay()
    {
        _scoreText.text = score.ToString(); // Отображаем количество баллов
    }
    
    public void ClickAgain()
    {
        SceneManager.LoadScene("MiniGame");
    }

    public void ClickMenu()
    {
        DataBase.UpdateScoreLvl1(User.Instance.Login, score);
        SceneManager.LoadScene("Menu");
    }
    
    public void ClickQuit()
    {
        DataBase.UpdateScoreLvl1(User.Instance.Login, score);
        Application.Quit();
    }
    
}
