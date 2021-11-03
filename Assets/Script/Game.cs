using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [SerializeField] private CellRenderer _renderer;
    [SerializeField] private MainMenu _menu;

    private void OnEnable()
    {
        _menu.OnRestartButtonClick += OnRestartButtonKlick;
    }
    private void OnDisable()
    {
        _menu.OnRestartButtonClick -= OnRestartButtonKlick;
    }

    private void OnRestartButtonKlick()
    {
        _menu.Close();
        _renderer.RefreshMaze();
    }
}
