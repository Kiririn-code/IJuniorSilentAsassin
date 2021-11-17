using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _textScore;
    [SerializeField] private Player _player;

    private Coroutine _valueChangedCoroutine;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _player.ScoreChahged += OnScoreChanged;
        _player.Died += OnViewRestart;
    }
    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _player.ScoreChahged -= OnScoreChanged;
    }

    private void OnViewRestart()
    {
        StopCoroutine(_valueChangedCoroutine);
        _healthSlider.value = _healthSlider.maxValue;
        _textScore.text = 0.ToString();
    }


    private void OnScoreChanged(int score)
    {
        _textScore.text = score.ToString();
    }

    private void OnHealthChanged(float newValue)
    {
        if (_valueChangedCoroutine != null)
            StopCoroutine(_valueChangedCoroutine);

        _valueChangedCoroutine = StartCoroutine(ChangeHealth(newValue));

    }

    private IEnumerator ChangeHealth(float value)
    {
        var time = new WaitForEndOfFrame();
        float delay = 0.1f;
        while(_healthSlider.value != value)
        {
            _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, value, delay);
            yield return time;
        }
    }
}
