using TMPro;
using UnityEngine;

// Affiche le temps restant de la partie.
public class TimerUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private string prefix = "Temps : ";

    private void Update()
    {
        if (gameManager == null || timerText == null) return;

        timerText.text = prefix + Mathf.CeilToInt(gameManager.RemainingTime);
    }
}