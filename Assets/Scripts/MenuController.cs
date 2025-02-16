using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Load game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    // Quit the game
    public void QuitGame()
    {
    #if UNITY_EDITOR
            // This works in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // This works in the built application
        Application.Quit();
    #endif
    }
}

