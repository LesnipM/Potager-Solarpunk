using System;
using UnityEngine;

/// <summary>
/// Tient le score de la partie et prévient les joueurs à chaque changement.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int pointsPerHarvest = 1;

    public int CurrentScore { get; private set; }

    public event Action<int> OnScoreChanged;

    public void AddHarvest()
    {
        CurrentScore += pointsPerHarvest;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}
