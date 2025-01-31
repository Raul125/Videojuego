using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        var health = collision.GetComponent<Health>();
        if (health.currentHealth != health.startingHealth)
        {
            health.Heal(healthAmount);
            Destroy(gameObject);
        }
    }
}
