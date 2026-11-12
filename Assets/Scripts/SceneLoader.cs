using UnityEngine;
using UnityEngine.SceneManagement;

// Change de scène, recommence ou quitte. Les boutons appellent ces méthodes depuis l'Inspector.
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        ResetPauseState();
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        ResetPauseState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        // Application.Quit ne fait rien dans l'éditeur : on arrête le Play Mode.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Si on quitte depuis le menu pause, le temps et le son sont encore figés.
    private void ResetPauseState()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}