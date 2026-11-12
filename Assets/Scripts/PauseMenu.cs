using UnityEngine;
using UnityEngine.InputSystem;

// Ouvre et ferme le menu pause avec l'action Pause, et fige le jeu pendant ce temps.
public class PauseMenu : MonoBehaviour
{
    [Header("Entrée")]
    [SerializeField] private InputActionReference pauseAction;

    [Header("Interface")]
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void OnEnable()
    {
        if (pauseAction == null) return;
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        if (pauseAction == null) return;
        pauseAction.action.performed -= OnPausePerformed;
        pauseAction.action.Disable();
    }

    private void Start()
    {
        SetPaused(false);
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        SetPaused(!isPaused);
    }

    // Appelée aussi par le bouton "Reprendre".
    public void Resume()
    {
        SetPaused(false);
    }

    private void SetPaused(bool paused)
    {
        isPaused = paused;

        if (pausePanel != null) pausePanel.SetActive(paused);

        Time.timeScale = paused ? 0f : 1f;
        AudioListener.pause = paused; // coupe aussi le son d'arrosage en boucle
    }
}