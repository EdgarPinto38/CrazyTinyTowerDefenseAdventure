using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GameObject cell;
    public GameObject tower;
    public Grid grid;
    private static GridBehaviour instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        grid = new Grid(4, 3, 2f, cell, new Vector3(-4, -2));
    }

    // La función Update ya no necesita manejar el clic derecho.
    private void Update()
    {
        // Hemos eliminado la funcionalidad de clic derecho.
    }

    private Vector3 GetWorldPosition()
    {
        Vector3 vec = GetMouseWorldPosition(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }

    private Vector3 GetMouseWorldPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
