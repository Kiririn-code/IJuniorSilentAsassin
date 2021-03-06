using UnityEngine;
using UnityEngine.Events;

public class MainMenu : Menu
{
    public event UnityAction RestartButtonClick;

    public override void Close()
    {
        _canvas.alpha = 0;
        _button.interactable = false;
        Time.timeScale = 1;
    }

    public override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
    }

    public override void Open()
    {
        Time.timeScale = 0;
        _canvas.alpha = 1;
        _button.interactable = true;
    }
}
