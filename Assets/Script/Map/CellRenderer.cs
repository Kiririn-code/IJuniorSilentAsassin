using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CellRenderer : MonoBehaviour
{
    [SerializeField] private Cell _cellPrafab;
    [SerializeField] private NavMeshSurface _surface;

    private CellGenerator _generator;
    private CellMaze[,] _mazeMap;
    private Coroutine _startUpdateNavMesh;

    private void Start()
    {
        _generator = new CellGenerator();
        GenerateMaze();
    }

    public void RebuildMaze()
    {
        Cell[] cells = FindObjectsOfType<Cell>();

        foreach (var item in cells)
        {
            Destroy(item.gameObject);
        }

        _mazeMap = _generator.RefrashMazeMap();
        RenderMaze();
        StartNavMeshDelayCoriutime();
    }

    private void GenerateMaze()
    {
        _mazeMap = _generator.GenerateMazeMap();
        RenderMaze();
        _surface.BuildNavMesh();
    }

    private void RenderMaze()
    {
        for (int x = 0; x < _mazeMap.GetLength(0); x++)
        {
            for (int y = 0; y < _mazeMap.GetLength(1); y++)
            {
                Cell cell = Instantiate(_cellPrafab, new Vector3(x, 0, y), Quaternion.identity, gameObject.transform);

                if (_mazeMap[x, y].IsBottomWallWisible)
                    cell.ActiveBottomWall();
                else
                    cell.DeactiveBottomWall();

                if (_mazeMap[x, y].IsLeftWallWisible)
                    cell.ActiveLeftWall();
                else
                    cell.DeactiveLeftWall();
            }
        }
    }

    private void StartNavMeshDelayCoriutime()
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
