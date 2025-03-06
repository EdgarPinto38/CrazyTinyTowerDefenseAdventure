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

    public void ExitGame()
    {
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Si estamos en una build del juego, cerrar la aplicación
        Application.Quit();
#endif
    }
}
