using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    public int initialPoints = 200; // Cantidad inicial de puntos que se puede modificar desde el Inspector
    public int points;
    public TMP_Text pointsText;

    private void Start()
    {
        points = initialPoints;
        UpdatePointsUI();
    }

    // Método para agregar puntos
    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsUI();
    }

    // Método para gastar puntos
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

    //Métod para modificar UI
    private void UpdatePointsUI()
    {
        pointsText.text = points.ToString() + "pts";
    }

    // Método para verificar si se pueden gastar puntos
    public bool CanAfford(int amount)
    {
        return points >= amount;
    }
}
