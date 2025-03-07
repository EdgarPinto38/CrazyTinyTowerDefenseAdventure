using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GameObject cell;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public Grid grid;
    private static GridBehaviour instance;
    public Vector3 gridOriginPosition = new Vector3(0, 0, 0); // Ajuste de la posici�n del grid

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("GridBehaviour instance initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GridBehaviour Instance
    {
        get { return instance; }
    }

    void Start()
    {
        grid = new Grid(6, 4, 2f, cell, gridOriginPosition); // Usar la posici�n de origen ajustable
        Debug.Log("Grid initialized in GridBehaviour Start.");
    }

    // M�todo para obtener la posici�n del mundo basada en la posici�n del mouse
    private Vector3 GetWorldPosition()
    {
        Vector3 vec = GetMouseWorldPosition(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }

    // M�todo para convertir la posici�n del mouse en la posici�n del mundo
    private Vector3 GetMouseWorldPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
