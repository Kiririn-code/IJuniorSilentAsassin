using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _bottomWall;
     
    public void ActiveLeftWall()
    {
        _leftWall.SetActive(true);
    }

    public void DeactiveLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ActiveBottomWall()
    {
        _bottomWall.SetActive(true);
    }

    public void DeactiveBottomWall()
    {
        _bottomWall.SetActive(false);
    }

}
