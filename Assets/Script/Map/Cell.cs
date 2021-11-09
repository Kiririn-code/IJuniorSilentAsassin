using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _bottomWall;

    public void SetLeftWallActive(bool value)
    {
        _leftWall.SetActive(value);
    }

    public void SetBottomWallActive(bool value)
    {
        _bottomWall.SetActive(value);
    }
}
