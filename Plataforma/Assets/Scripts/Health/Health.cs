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

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private bool invulnerable;

    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private bool isDead;

    [Header("Audio")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            if (hurtSound != null)
            {
                SoundManager.Instance.PlaySound(hurtSound);
            }

            StartCoroutine(Invulnerability());
        }
        else if (!isDead)
        {
            animator.SetTrigger("Death");
            if (dieSound != null)
            {
                SoundManager.Instance.PlaySound(dieSound);
            }

            foreach (Behaviour component in components)
            {
                component.enabled = false;
            }

            isDead = true;
            if (!gameObject.CompareTag("Player"))
            {
                ScoreManager.Instance.AddScore(1);
            }
            else
            {
                gameObject.GetComponent<PlayerMovement>().PetAnimator.SetTrigger("Sit");
            }
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
    }

    public void Respawn()
    {
        isDead = false;
        Heal(startingHealth);
        var playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerMovement.IsWallSliding = false;
        animator.SetBool("WallSlide", false);
        animator.ResetTrigger("Death");
        animator.Play("Idle");

        StartCoroutine(Invulnerability());

        var playerAttack = gameObject.GetComponent<PlayerAttack>();
        if (playerAttack.Blocking)
        {
            playerAttack.StopBlocking();
        }

        Animator petAnimator = gameObject.GetComponent<PlayerMovement>().PetAnimator;
        petAnimator.ResetTrigger("Sit");
        petAnimator.Play("Idle");

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numerOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numerOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numerOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
}
