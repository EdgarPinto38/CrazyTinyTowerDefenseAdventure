using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    public int initialPoints = 300; // Cantidad inicial de puntos que se puede modificar desde el Inspector
    public int points;
    public TMP_Text pointsText; // Cambiado a TMP_Text para usar TextMeshPro

    private void Start()
    {
        points = initialPoints; // Asignar los puntos iniciales al empezar el juego
        UpdatePointsUI();
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsUI();
    }

    public bool SpendPoints(int amount)
    {
        if (points >= amount)
        {
            points -= amount;
            Debug.Log("Spent points: " + amount + ", Remaining points: " + points);
            UpdatePointsUI();
            return true;
        }
        Debug.Log("Not enough points to spend: " + amount + ", Current points: " + points);
        return false;
    }


    private void UpdatePointsUI()
    {
        pointsText.text = points.ToString() + "pts";
    }

    public bool CanAfford(int amount)
    {
        return points >= amount;
    }
}
