using TMPro;
using UnityEngine;

// Affiche le dernier score et/ou le meilleur score. Les deux champs sont facultatifs.
public class ScoreboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private string lastScorePrefix = "Score : ";
    [SerializeField] private string bestScorePrefix = "Meilleur score : ";

    private void Start()
    {
        if (lastScoreText != null)
        {
            lastScoreText.text = lastScorePrefix + GameSession.LastScore;
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = bestScorePrefix + SaveManager.GetBestScore();
        }
    }
}