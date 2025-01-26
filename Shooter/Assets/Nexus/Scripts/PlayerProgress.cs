using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    private const string LevelKey = "PlayerLevel";

    public void SaveProgress(int level)
    {
        PlayerPrefs.SetInt(LevelKey, level);
        PlayerPrefs.Save(); // Asegura que los datos se guarden inmediatamente
        Debug.Log($"Progreso guardado: Nivel {level}");
    }

    public int LoadProgress()
    {
        int level = PlayerPrefs.GetInt(LevelKey, 1);
        Debug.Log($"Progreso cargado: Nivel {level}");
        return level;
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(LevelKey);
        Debug.Log("Progreso reiniciado.");
    }
}