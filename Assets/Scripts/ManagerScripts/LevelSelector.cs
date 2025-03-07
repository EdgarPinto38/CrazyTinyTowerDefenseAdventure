using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Método para establecer la dificultad fácil y cargar la escena del juego
    public void SetEasyDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Easy;
        SceneManager.LoadScene("game");
    }

    // Método para establecer la dificultad media y cargar la escena del juego
    public void SetMediumDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Medium;
        SceneManager.LoadScene("game");
    }

    // Método para establecer la dificultad difícil y cargar la escena del juego
    public void SetHardDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Hard;
        SceneManager.LoadScene("game");
    }

    // Método para salir del juego
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una build del juego, cerrar la aplicación
        Application.Quit();
#endif
    }
}
