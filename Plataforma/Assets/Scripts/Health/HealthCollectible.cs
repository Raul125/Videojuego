using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthAmount;
    [SerializeField] private AudioClip healthSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Health health = collision.GetComponent<Health>();
        if (health.currentHealth != health.startingHealth)
        {
            SoundManager.Instance.PlaySound(healthSound);
            health.Heal(healthAmount);
            Destroy(gameObject);
        }
    }
}
