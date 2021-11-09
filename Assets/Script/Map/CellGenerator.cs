using System.Collections.Generic;

public class CellGenerator
{
    private CellMaze[,] _mazeMap;
    private int _width = 10;
    private int _height = 10;

    public CellMaze[,] GenerateMaze()
    {
        _mazeMap = new CellMaze[_width,_height];

        for (int x = 0; x < _mazeMap.GetLength(0); x++)
        {
            for (int y = 0; y < _mazeMap.GetLength(1); y++)
            {
                _mazeMap[x, y] = new CellMaze { X = x, Y = y };
            }
        }

        RemoveWall(_mazeMap);

        return _mazeMap;
   }

    public CellMaze[,] RefrashMaze()
    {
        for (int x = 0; x < _mazeMap.GetLength(0); x++)
        {
            for (int y = 0; y < _mazeMap.GetLength(1); y++)
            {
                _mazeMap[x, y].IsBottomWallWisible = true;
                _mazeMap[x, y].IsLeftWallWisible = true;
                _mazeMap[x, y].IsVisited = false;
            }
        }
        RemoveWall(_mazeMap);
        return _mazeMap;
    }

    private void RemoveWall(CellMaze[,] maze)
    {
        CellMaze currentCell = maze[0, 0];
        currentCell.IsVisited = true;

        Stack<CellMaze> stackCellMazes = new Stack<CellMaze>();

        do
        {
            List<CellMaze> unvisitedNeighbors = new List<CellMaze>();

            CheckNeighbors(currentCell, maze, unvisitedNeighbors);

            if(unvisitedNeighbors.Count > 0)
            {
                CellMaze choosenCell = unvisitedNeighbors[UnityEngine.Random.Range(0, unvisitedNeighbors.Count)];
                RemoveWall(currentCell, choosenCell);
                choosenCell.IsVisited = true;
                stackCellMazes.Push(choosenCell);
                currentCell = choosenCell;
            }
            else
            {
                currentCell = stackCellMazes.Pop();
            }

        } while (stackCellMazes.Count > 0);
    }

    private void CheckNeighbors(CellMaze currentCell, CellMaze[,] maze,List<CellMaze> unvisitedNeighbors)
    {
        int x = currentCell.X;
        int y = currentCell.Y;

        if (x > 0 && maze[x - 1, y].IsVisited == false) unvisitedNeighbors.Add(maze[x - 1, y]);
        if (y > 0 && maze[x, y - 1].IsVisited == false) unvisitedNeighbors.Add(maze[x, y - 1]);
        if (x < _width - 2 && maze[x + 1, y].IsVisited == false) unvisitedNeighbors.Add(maze[x + 1, y]);
        if (y < _height - 2 && maze[x, y + 1].IsVisited == false) unvisitedNeighbors.Add(maze[x, y + 1]);
    }

    private void RemoveWall(CellMaze currentCell, CellMaze choosenCell)
    {
        if(currentCell.X == choosenCell.X)
        {
            if (currentCell.Y > choosenCell.Y) currentCell.IsBottomWallWisible = false;
            else choosenCell.IsBottomWallWisible = false;
        }
        else
        {
            if (currentCell.X > choosenCell.X) currentCell.IsLeftWallWisible = false;
            else choosenCell.IsLeftWallWisible = false;
        }
    }
}

public class CellMaze
{
    private int _x;
    private int _y;
    private bool _isLeftWallWisible = true;
    private bool _isBottomWallWisible = true;
    private bool _isVisited = false;

    public bool IsLeftWallWisible { get { return _isLeftWallWisible; } set { _isLeftWallWisible = value; } }
    public bool IsBottomWallWisible { get { return _isBottomWallWisible; } set { _isBottomWallWisible = value; } }
    public bool IsVisited { get { return _isVisited; } set { _isVisited = value; } }
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }
}
