using System.Collections.Generic;

public class CellGenerator
{
    private CellMaze[,] _mazeMap;
    private int _width = 10;
    private int _height = 10;

    public CellMaze[,] GenerateMazeMap()
    {
        _mazeMap = new CellMaze[_width, _height];

        for (int x = 0; x < _mazeMap.GetLength(0); x++)
            for (int y = 0; y < _mazeMap.GetLength(1); y++)
                _mazeMap[x, y] = new CellMaze(x, y);

        RemoveWall(_mazeMap);

        return _mazeMap;
    }

    public CellMaze[,] RefrashMazeMap()
    {
        for (int x = 0; x < _mazeMap.GetLength(0); x++)
            for (int y = 0; y < _mazeMap.GetLength(1); y++)
                _mazeMap[x, y].Reset();

        RemoveWall(_mazeMap);
        return _mazeMap;
    }

    private void RemoveWall(CellMaze[,] maze)
    {
        CellMaze currentCell = maze[0, 0];
        currentCell.Visit();

        Stack<CellMaze> stackCellMazes = new Stack<CellMaze>();

        do
        {
            List<CellMaze> unvisitedNeighbors = new List<CellMaze>();

            FindUnvisitedCellNeighbors(currentCell, maze,unvisitedNeighbors);

            if(unvisitedNeighbors.Count > 0)
            {
                CellMaze choosenCell = unvisitedNeighbors[UnityEngine.Random.Range(0, unvisitedNeighbors.Count)];
                RemoveWall(currentCell, choosenCell);
                choosenCell.Visit();
                stackCellMazes.Push(choosenCell);
                currentCell = choosenCell;
            }
            else
            {
                currentCell = stackCellMazes.Pop();
            }

        } while (stackCellMazes.Count > 0);
    }

    private void FindUnvisitedCellNeighbors(CellMaze currentCell, CellMaze[,] maze,List<CellMaze> unvisitedNeighbors)
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
            if (currentCell.Y > choosenCell.Y) currentCell.RemoveBottomWall();
            else choosenCell.RemoveBottomWall();
        }
        else
        {
            if (currentCell.X > choosenCell.X) currentCell.RemoveLeftWall();
            else choosenCell.RemoveLeftWall();
        }
    }
}

public class CellMaze
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool IsVisited { get; private set; }
    public bool IsLeftWallWisible { get; private set; }
    public bool IsBottomWallWisible { get; private set; }

    public CellMaze(int x, int y)
    {
        if (x < 0 || y < 0)
            throw new System.ArgumentException();
        X = x;
        Y = y;

        Reset();
    }

    public void RemoveLeftWall()
    {
        IsLeftWallWisible = false;
    }

    public void RemoveBottomWall()
    {
        IsBottomWallWisible = false;
    }

    public void Visit()
    {
        IsVisited = true;
    }

    public void Reset()
    {
        IsVisited = false;
        IsLeftWallWisible = true;
        IsBottomWallWisible = true;
    }
}
