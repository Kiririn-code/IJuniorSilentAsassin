using UnityEngine;
using UnityEngine.AI;

public class CellRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrafab;

    void Awake()
    {
        CellGenerator generator = new CellGenerator();

        CellMaze[,] mazeMap= generator.GenerateMaze();

        for (int x = 0; x < mazeMap.GetLength(0); x++)
        {
            for (int y = 0; y < mazeMap.GetLength(1); y++)
            {
                Cell cell = Instantiate(_cellPrafab, new Vector3(x, 0, y), Quaternion.identity, gameObject.transform).GetComponent<Cell>();

                cell.BottomWall.SetActive(mazeMap[x, y].IsBottomWallWisible);
                cell.LeftWall.SetActive(mazeMap[x, y].IsLeftWallWisible);
            }
        }
    }

    private void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
