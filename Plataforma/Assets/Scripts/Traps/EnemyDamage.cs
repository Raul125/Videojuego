using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected AudioClip blockedSound;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
        {
            PlayerAttack playerAttack = collision.GetComponentInParent<PlayerAttack>();
            if (playerAttack != null && playerAttack.Blocking)
            {
                SoundManager.Instance.PlaySound(blockedSound);
            }
            else
            {
                collision.GetComponentInParent<Health>().TakeDamage(damage);
            }
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
