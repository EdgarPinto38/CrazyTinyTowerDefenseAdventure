using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    public GameObject towerPrefab;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Aqu� llamas a la funci�n para instanciar la torre
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f; // Ajusta la posici�n en Z si es necesario

        Debug.Log("Attempting to place tower at world position: " + worldPosition);

        // Llama a la funci�n de la clase Grid para colocar la torre si la celda est� vac�a
        if (GridBehaviour.Instance.grid.CanPlaceTower(worldPosition))
        {
            Debug.Log("Can place tower, calling SetTower");
            GridBehaviour.Instance.grid.SetTower(worldPosition, towerPrefab);
        }
        else
        {
            Debug.Log("Cannot place tower, cell is already occupied.");
        }

        // Restaurar la posici�n original de la imagen
        rectTransform.anchoredPosition = originalPosition;
    }

}
