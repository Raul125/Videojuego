using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out PlayerAttack playerAttack) && playerAttack.Blocking)
            {
                return;
            }

            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
