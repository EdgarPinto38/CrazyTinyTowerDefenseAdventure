using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // M�todo para establecer la dificultad f�cil y cargar la escena del juego
    public void SetEasyDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Easy;
        SceneManager.LoadScene("game");
    }

    // M�todo para establecer la dificultad media y cargar la escena del juego
    public void SetMediumDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Medium;
        SceneManager.LoadScene("game");
    }

    // M�todo para establecer la dificultad dif�cil y cargar la escena del juego
    public void SetHardDifficulty()
    {
        GameSettings.SelectedDifficulty = WaveDifficulty.Hard;
        SceneManager.LoadScene("game");
    }

    // M�todo para salir del juego
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una build del juego, cerrar la aplicaci�n
        Application.Quit();
#endif
    }
}
