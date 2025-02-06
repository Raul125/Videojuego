using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected AudioClip blockedSound;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out PlayerAttack playerAttack) && playerAttack.Blocking)
            {
                SoundManager.Instance.PlaySound(blockedSound);
                return;
            }

            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
