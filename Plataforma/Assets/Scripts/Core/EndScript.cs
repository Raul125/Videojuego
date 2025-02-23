using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    [SerializeField] private int level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int currentLevel = PlayerPrefs.GetInt("level", 1);
            if (level > currentLevel)
            {
                PlayerPrefs.SetInt("level", level);
            }

            SceneManager.LoadScene(0);
        }
    }
}
