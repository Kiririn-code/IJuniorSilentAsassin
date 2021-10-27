using UnityEngine;
using UnityEngine.AI;

public class CellRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrafab;

    void Start()
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

        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
