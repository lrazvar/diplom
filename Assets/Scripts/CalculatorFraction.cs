using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CalculatorFraction : MonoBehaviour
{
    [SerializeField] private TMP_Text _plus, _minus, _div, _multi;

    [SerializeField] private TMP_Text _firstNumerator, _secondNumerator, _firstDenomirator, _secondDenomirator;
    [SerializeField] private GameObject _firstFraction, _secondFraction;
    [SerializeField] private TMP_Text _timerText, _scoreText;

    [SerializeField] private TMP_InputField _answerNumerator, _answerDenominator;
    [SerializeField] private GameObject _finishGame, _answerObject, _timerObject;
    [SerializeField] private TMP_Text _results;
    [SerializeField] private TMP_Text _timerBeforeBegin, _textBeforeBegin;

    private int firstNumerator, secondNumerator, firstDenominator, secondDenominator;
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
        _firstFraction.SetActive(true);
        _secondFraction.SetActive(true);
        _answerObject.SetActive(true);
        _timerObject.SetActive(true);
        GenerateQuestion();
        
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
                firstNumerator = Random.Range(1, 10);
                secondNumerator = Random.Range(1, 10);
                firstDenominator = Random.Range(2, 10);
                secondDenominator = Random.Range(2, 10);
                _plus.gameObject.SetActive(true);
                _minus.gameObject.SetActive(false);
                _div.gameObject.SetActive(false);
                _multi.gameObject.SetActive(false);
                break;
            case '-':
                secondNumerator = Random.Range(1, firstNumerator); // Генерация второго числителя
                firstNumerator = Random.Range(secondNumerator, 10); // Генерация первого числителя
                firstDenominator = Random.Range(2, 10); // Генерация знаменателя
                secondDenominator = Random.Range(2, 10);
                _plus.gameObject.SetActive(false);
                _minus.gameObject.SetActive(true);
                _div.gameObject.SetActive(false);
                _multi.gameObject.SetActive(false);
                break;
            case '/':
                
                firstNumerator = Random.Range(1, 10);
                secondNumerator = Random.Range(1, 10);
                firstDenominator = Random.Range(2, 10);
                secondDenominator = Random.Range(2, 10);
                _plus.gameObject.SetActive(false);
                _minus.gameObject.SetActive(false);
                _div.gameObject.SetActive(true);
                _multi.gameObject.SetActive(false);
                break;
            case '*':
                firstNumerator = Random.Range(1, 10);
                secondNumerator = Random.Range(1, 10);
                firstDenominator = Random.Range(2, 10);
                secondDenominator = Random.Range(2, 10);
                _plus.gameObject.SetActive(false);
                _minus.gameObject.SetActive(false);
                _div.gameObject.SetActive(false);
                _multi.gameObject.SetActive(true);
                break;
        }

        // Отображаем числа на экране
        _firstNumerator.text = firstNumerator.ToString();
        _secondNumerator.text = secondNumerator.ToString();
        _firstDenomirator.text = firstDenominator.ToString();
        _secondDenomirator.text = secondDenominator.ToString();
    }

    public void CheckAnswer()
    {
        // Получаем ответ пользователя и преобразуем его в число
        int userNumerator = int.Parse(_answerNumerator.text);
        int userDenominator = int.Parse(_answerDenominator.text);

        // Сокращаем дробь
        int gcd = GCD(userNumerator, userDenominator);
        userNumerator /= gcd;
        userDenominator /= gcd;
        
        // Вычисляем правильный ответ
        int correctNumerator= 0;
        int correctDenominator= 0;
        switch (operation)
        {
            case '+':
                correctNumerator = firstNumerator * secondDenominator + secondNumerator * firstDenominator;
                correctDenominator = firstDenominator * secondDenominator;
                break;
            case '-':
                correctNumerator = firstNumerator * secondDenominator - secondNumerator * firstDenominator;
                correctDenominator = firstDenominator * secondDenominator;
                break;
            case '/':
                correctNumerator = firstNumerator * secondDenominator;
                correctDenominator = firstDenominator * secondNumerator;
                break;
            case '*':
                correctNumerator = firstNumerator * secondNumerator;
                correctDenominator = firstDenominator * secondDenominator;
                break;
        }

        gcd = GCD(correctNumerator, correctDenominator);
        correctNumerator /= gcd;
        correctDenominator /= gcd;
        
        // Проверяем ответ пользователя
        if (userNumerator == correctNumerator && userDenominator == correctDenominator)
        {
            Debug.Log("Правильно!");
            score += scoreCoef; // Увеличиваем количество баллов при правильном ответе
            UpdateScoreDisplay(); // Обновляем отображение баллов
        }
        else
        {
            Debug.Log("Неправильно!");
        }

        // Генерируем новый вопрос
        GenerateQuestion();

        // Очищаем поле ввода
        _answerNumerator.text = "";
        _answerDenominator.text = "";
    }
    
    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
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
        SceneManager.LoadScene("MiniGameFraction");
    }

    public void ClickMenu()
    {
        DataBase.UpdateScoreLvl2(User.Instance.Login, score);
        SceneManager.LoadScene("Menu");
    }
    
    public void ClickQuit()
    {
        DataBase.UpdateScoreLvl2(User.Instance.Login, score);
        Application.Quit();
    }
    
}

