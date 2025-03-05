using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void SetEasyDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Easy;
        SceneManager.LoadScene("game"); 
    }

    public void SetMediumDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Medium;
        SceneManager.LoadScene("game");
    }

    public void SetHardDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Hard;
        SceneManager.LoadScene("game");
    }
}
