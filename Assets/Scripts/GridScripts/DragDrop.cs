using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    public GameObject towerPrefab;
    public int towerCost;
    private EconomyManager economyManager;
    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        economyManager = FindObjectOfType<EconomyManager>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (economyManager.CanAfford(towerCost))
        {
            image.color = Color.white; // Color normal si se puede comprar
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            image.color = Color.gray; // Color apagado si no se puede comprar
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (economyManager.CanAfford(towerCost))
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (economyManager.CanAfford(towerCost))
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (economyManager.CanAfford(towerCost))
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;

            if (GridBehaviour.Instance.grid.CanPlaceTower(worldPosition))
            {
                if (economyManager.SpendPoints(towerCost)) // Asegurarse de que se restan los puntos correctamente
                {
                    GridBehaviour.Instance.grid.SetTower(worldPosition, towerPrefab);
                }
                else
                {
                    Debug.Log("Not enough points to place the tower.");
                }
            }
            else
            {
                Debug.Log("Cannot place tower, cell is already occupied.");
            }

            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
