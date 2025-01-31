using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;

    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Fire1") && timeSinceAttack > 0.25f && !playerMovement.Rolling)
        {
            PerformAttack();
        }
        else if (Input.GetButtonDown("Fire2") && !playerMovement.Rolling)
        {
            StartBlocking();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            StopBlocking();
        }
    }

    private void PerformAttack()
    {
        currentAttack = (timeSinceAttack > 1.0f) ? 1 : (currentAttack % 3) + 1;
        animator.SetTrigger("Attack" + currentAttack);
        timeSinceAttack = 0.0f;

        DealDamage();
    }

    private void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            // Deal Damage
        }
    }


    private void StartBlocking()
    {
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
    }

    private void StopBlocking()
    {
        animator.SetBool("IdleBlock", false);
    }
}