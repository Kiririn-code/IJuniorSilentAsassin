using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [SerializeField] private CellRenderer _renderer;
    [SerializeField] private MainMenu _menu;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyGenerator _generator;

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
        _generator.RestartSpawn();
        _player.RestartPlayer();
    }
}
