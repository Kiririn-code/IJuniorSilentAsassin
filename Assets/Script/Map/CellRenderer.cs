using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CellRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrafab;
    [SerializeField] private NavMeshSurface _surface;

    private CellGenerator _generator;
    private CellMaze[,] _mazeMap;
    private Coroutine _startUpdateNavMesh;

    private void Start()
    {
        _generator = new CellGenerator();
        MazeRender();
    }

    public void RefreshMaze()
    {
        Cell[] cells = FindObjectsOfType<Cell>();

        foreach (var item in cells)
        {
            Destroy(item.gameObject);
        }

        _mazeMap = _generator.RefrashMaze();
        DeleteWall();
        StartNavMeshCoriutime();
    }

    private void MazeRender()
    {
        _mazeMap = _generator.GenerateMaze();
        DeleteWall();
        _surface.BuildNavMesh();
    }

    private void DeleteWall()
    {
        for (int x = 0; x < _mazeMap.GetLength(0); x++)
        {
            for (int y = 0; y < _mazeMap.GetLength(1); y++)
            {
                Cell cell = Instantiate(_cellPrafab, new Vector3(x, 0, y), Quaternion.identity, gameObject.transform).GetComponent<Cell>();
                cell.SetBottomWallActive(_mazeMap[x, y].IsBottomWallWisible);
                cell.SetLeftWallActive(_mazeMap[x, y].IsLeftWallWisible);
            }
        }
    }

    private void StartNavMeshCoriutime()
    {
        if (_startUpdateNavMesh != null)
            StopCoroutine(_startUpdateNavMesh);
        _startUpdateNavMesh = StartCoroutine(UpdateNavMesh());
    }

    private IEnumerator UpdateNavMesh()
    {
        int delay = 3;
        for (int i = 0; i < delay; i++)
        {
            _surface.UpdateNavMesh(_surface.navMeshData);
            var time = new WaitForEndOfFrame();
            yield return time;
            delay--;
        }
    }
}
