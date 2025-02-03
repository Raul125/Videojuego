using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float startingHealth;
    public float currentHealth { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numerOfFlashes;

    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
        }
        else if (!isDead)
        {
            animator.SetBool("noBlood", false);
            animator.SetTrigger("Death");
            if (TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.enabled = false;
            }

            if (TryGetComponent(out PlayerAttack playerAttack))
            {
                playerAttack.enabled = false;
            }

            if (TryGetComponent(out WizardEnemy wizardEnemy))
            {
                wizardEnemy.enabled = false;
            }

            if (GetComponentInParent<EnemyPatrol>() != null)
            {
                GetComponentInParent<EnemyPatrol>().enabled = false;
            }

            isDead = true;
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numerOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numerOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numerOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
