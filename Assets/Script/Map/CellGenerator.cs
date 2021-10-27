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

        RemoveWallWithBackTracking(_mazeMap);

        return _mazeMap;
    }

    private void RemoveWallWithBackTracking(CellMaze[,] maze)
    {
        CellMaze currentCell = maze[0, 0];
        currentCell.IsVisited = true;

        Stack<CellMaze> stackCellMazes = new Stack<CellMaze>();

        do
        {
            List<CellMaze> unvisitedNeighbors = new List<CellMaze>();

            int x = currentCell.X;
            int y = currentCell.Y;

            if (x > 0 && maze[x - 1, y].IsVisited == false) unvisitedNeighbors.Add(maze[x - 1, y]);
            if (y > 0 && maze[x, y - 1].IsVisited == false) unvisitedNeighbors.Add(maze[x, y - 1]);
            if (x < _width - 2 && maze[x + 1, y].IsVisited == false) unvisitedNeighbors.Add(maze[x + 1, y]);
            if (y < _height - 2 && maze[x, y + 1].IsVisited == false) unvisitedNeighbors.Add(maze[x, y + 1]);

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
    public int X;
    public int Y;

    public bool IsLeftWallWisible = true;
    public bool IsBottomWallWisible = true;

    public bool IsVisited = false;
}
