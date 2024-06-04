using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MazeGenerator
{
    public int Width;
    public int Height;
    
    
    
    public Maze GenerateMaze(GameObject cubePlus, GameObject cubeMinus, GameObject cubeDiv, GameObject cubeMulti)
    {
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell {X = x, Y = y};
            }
        }

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, Height - 1].WallLeft = false;
        }

        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[Width - 1, y].WallBottom = false;
        }

        RemoveWallsWithBacktracker(cells);

        Maze maze = new Maze();

        maze.cells = cells;
        maze.finishPosition = PlaceMazeExit(cells);

        // Размещаем cubePlus и cubeMinus в рандомных префабах Cell
        //PlaceObjectsInRandomCells(cells, cubePlus, cubeMinus, cubeDiv, cubeMulti);
        
        return maze;
    }
    
    
    private void PlaceObjectsInRandomCells(MazeGeneratorCell[,] cells, GameObject cubePlus, GameObject cubeMinus, GameObject cubeDiv, GameObject cubeMulti)
    {
       
        List<MazeGeneratorCell> availableCells = new List<MazeGeneratorCell>();

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                availableCells.Add(cells[x, y]);
            }
        }
        
        MazeGeneratorCell randomCell = availableCells[UnityEngine.Random.Range(0, availableCells.Count)];
        Vector3 cellCenterPosition = new Vector3(randomCell.X, 0f, randomCell.Y);
        Vector3 cubePlusPosition = cellCenterPosition + new Vector3(2.5f, 1f, 5f); 
        cubePlus.transform.position = cubePlusPosition;

        randomCell = availableCells[UnityEngine.Random.Range(0, availableCells.Count)];
        cellCenterPosition = new Vector3(randomCell.X, 0f, randomCell.Y);
        Vector3 cubeMinusPosition = cellCenterPosition + new Vector3(2.5f, 1f, 5f); 
        cubeMinus.transform.position = cubeMinusPosition;
        
        randomCell = availableCells[UnityEngine.Random.Range(0, availableCells.Count)];
        cellCenterPosition = new Vector3(randomCell.X, 0f, randomCell.Y);
        Vector3 cubeDivPosition = cellCenterPosition + new Vector3(2.5f, 1f, 5f); 
        cubeDiv.transform.position = cubeDivPosition;
        
        randomCell = availableCells[UnityEngine.Random.Range(0, availableCells.Count)];
        cellCenterPosition = new Vector3(randomCell.X, 0f, randomCell.Y);
        Vector3 cubeMultiPosition = cellCenterPosition + new Vector3(2.5f, 1f, 5f); 
        cubeMulti.transform.position = cubeMultiPosition;
        
        
    }
    
    

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> wayCells = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                wayCells.Push(chosen);
                chosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = chosen;
            }
            else
            {
                current = wayCells.Pop();
            }
        } while (wayCells.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }

    private Vector2Int PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, Height - 2];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[Width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        if (furthest.X == 0) furthest.WallLeft = false;
        else if (furthest.Y == 0) furthest.WallBottom = false;
        else if (furthest.X == Width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
        else if (furthest.Y == Height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;

        return new Vector2Int(furthest.X, furthest.Y);
    }
}