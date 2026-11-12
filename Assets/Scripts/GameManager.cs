using UnityEngine;
using UnityEngine.SceneManagement;

// Surveille la fin de partie (temps écoulé et/ou objectif de score atteint).
public class GameManager : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("Fin de partie")]
    [Tooltip("Durée de la partie en secondes. 0 = pas de timer.")]
    [SerializeField] private float timeLimit = 90f;
    [Tooltip("Score à atteindre pour finir la partie. 0 = pas d'objectif.")]
    [SerializeField] private int scoreGoal = 0;
    [SerializeField] private string endSceneName = "End";

    public float RemainingTime { get; private set; }

    private bool isGameOver;

    private void OnEnable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged += OnScoreChanged;
        }
    }

    private void OnDisable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged -= OnScoreChanged;
        }
    }

    private void Start()
    {
        RemainingTime = timeLimit;
    }

    private void Update()
    {
        if (isGameOver || scoreManager == null || timeLimit <= 0f) return;

        // Time.deltaTime vaut 0 en pause : le timer s'arrête tout seul.
        RemainingTime -= Time.deltaTime;

        if (RemainingTime <= 0f)
        {
            RemainingTime = 0f;
            EndGame();
        }
    }

    private void OnScoreChanged(int score)
    {
        if (scoreGoal > 0 && score >= scoreGoal)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        int finalScore = scoreManager.CurrentScore;
        GameSession.LastScore = finalScore;
        SaveManager.SaveBestScoreIfHigher(finalScore);

        SceneManager.LoadScene(endSceneName);
    }
}