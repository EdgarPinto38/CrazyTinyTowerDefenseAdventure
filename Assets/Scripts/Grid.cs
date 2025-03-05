using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private GameObject cell;
    private int[,] gridArray;
    private GameObject[,] arrayTower;

    public Grid(int width, int height, float cellSize, GameObject cell, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cell = cell;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        arrayTower = new GameObject[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                arrayTower[x, y] = GameObject.Instantiate(cell, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    private void SetLocationTower(int x, int y, GameObject tower)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            arrayTower[x, y] = GameObject.Instantiate(tower, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity);
        }
    }

    public void SetTower(Vector3 worldPosition, GameObject tower)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetLocationTower(x, y, tower);
    }
}
