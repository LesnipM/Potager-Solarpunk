using UnityEngine;

// Lit et écrit le meilleur score dans les PlayerPrefs.
public static class SaveManager
{
    private const string BestScoreKey = "BestScore";

    // Renvoie 0 au tout premier lancement (valeur par défaut), sans erreur.
    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    // N'écrit que si le score est meilleur : le record ne baisse jamais.
    // Renvoie vrai si un nouveau record a été enregistré.
    public static bool SaveBestScoreIfHigher(int score)
    {
        if (score <= GetBestScore()) return false;

        PlayerPrefs.SetInt(BestScoreKey, score);
        PlayerPrefs.Save();
        return true;
    }
}