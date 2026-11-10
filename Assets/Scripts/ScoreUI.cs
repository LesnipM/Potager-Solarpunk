using TMPro;
using UnityEngine;

// Affiche le score dans un texte TextMeshPro et le met à jour à chaque récolte.
public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string prefix = "Score : ";

    private void OnEnable()
    {
        if (scoreManager == null || scoreText == null) return;

        scoreManager.OnScoreChanged += UpdateText;
        UpdateText(scoreManager.CurrentScore);
    }

    private void OnDisable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged -= UpdateText;
        }
    }

    private void UpdateText(int score)
    {
        scoreText.text = prefix + score;
    }
}
