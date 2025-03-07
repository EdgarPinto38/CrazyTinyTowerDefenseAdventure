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
        arrayTower = new GameObject[width, height];  // Inicializa con null por defecto

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                arrayTower[x, y] = null;
                GameObject.Instantiate(cell, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity);
            }
        }
    }

    // Método para obtener la posición del mundo basada en las coordenadas de la grilla
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    // Método para convertir una posición del mundo en coordenadas de la grilla
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    // Método para colocar una torre en una ubicación específica de la grilla
    private void SetLocationTower(int x, int y, GameObject tower)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (arrayTower[x, y] == null)  // Verificar si la celda está vacía
            {
                arrayTower[x, y] = GameObject.Instantiate(tower, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity);
                Debug.Log($"Tower placed at: ({x}, {y})");
            }
            else
            {
                Debug.Log($"Cell ({x}, {y}) is already occupied.");
            }
        }
    }

    // Método público para establecer una torre en la posición del mundo especificada
    public void SetTower(Vector3 worldPosition, GameObject tower)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        Debug.Log($"SetTower called for position: ({x}, {y})");
        SetLocationTower(x, y, tower);
    }

    // Método para verificar si se puede colocar una torre en una posición del mundo específica
    public bool CanPlaceTower(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return arrayTower[x, y] == null;
        }
        return false;
    }
}
