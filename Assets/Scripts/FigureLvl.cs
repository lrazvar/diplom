using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FigureLvl : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText, _scoreText;
    [SerializeField] private GameObject _finishGame, _emptyFigures, _form, _figures, _timerObject;
    [SerializeField] private TMP_Text _results;
    [SerializeField] private TMP_Text _timerBeforeBegin, _textBeforeBegin;
    private float timer = 20f;
    private int score = 0;

    
    private void Start()
    {
        _scoreText.text = score.ToString();
        StartCoroutine(StartCountdown());
    }
    
    public void AddScore()
    {
        score++;
        UpdateScoreDisplay();
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
        _timerObject.SetActive(true);
        _emptyFigures.SetActive(true);
        _form.SetActive(true);
        _figures.SetActive(true);
        
        
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
        _emptyFigures.SetActive(false);
        _form.SetActive(false);
        _figures.SetActive(false);
        _timerObject.SetActive(false);
        Debug.Log("Время вышло!");
        _scoreText.text = "";
        _results.text = "ТВОЙ РЕЗУЛЬТАТ: " + score;
        _finishGame.SetActive(true);
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
        SceneManager.LoadScene("MiniGameGeometry");
    }

    public void ClickMenu()
    {
        DataBase.UpdateScoreLvl3(User.Instance.Login, score);
        SceneManager.LoadScene("Menu");
    }
    
    public void ClickQuit()
    {
        DataBase.UpdateScoreLvl3(User.Instance.Login, score);
        Application.Quit();
    }
}
