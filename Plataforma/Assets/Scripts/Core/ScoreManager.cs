using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI text;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        text.text = "Score: " + score;
    }

    public void AddScore(int value)
    {
        score += value;
        text.text = "Score: " + score;
    }

    public void ResetScore()
    {
        score = 0;
        text.text = "Score: " + score;
    }
}
