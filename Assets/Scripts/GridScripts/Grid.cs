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

    // M�todo para obtener la posici�n del mundo basada en las coordenadas de la grilla
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    // M�todo para convertir una posici�n del mundo en coordenadas de la grilla
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    // M�todo para colocar una torre en una ubicaci�n espec�fica de la grilla
    private void SetLocationTower(int x, int y, GameObject tower)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (arrayTower[x, y] == null)  // Verificar si la celda est� vac�a
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

    // M�todo p�blico para establecer una torre en la posici�n del mundo especificada
    public void SetTower(Vector3 worldPosition, GameObject tower)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        Debug.Log($"SetTower called for position: ({x}, {y})");
        SetLocationTower(x, y, tower);
    }

    // M�todo para verificar si se puede colocar una torre en una posici�n del mundo espec�fica
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
