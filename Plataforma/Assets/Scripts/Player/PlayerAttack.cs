using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    private int currentAttack;
    private float timeSinceAttack;

    [Header("Attack Settings")]
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Settings")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    public LayerMask enemyLayer;

    public bool Blocking { get; private set; }

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
        RaycastHit2D[] hits =
            Physics2D.BoxCastAll(boxCollider.bounds.center + (transform.right * range * playerMovement.FacingDirection * colliderDistance),
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        foreach (RaycastHit2D enemy in hits)
        {
            if (enemy.collider.TryGetComponent(out Health healthComponent))
            {
                healthComponent.TakeDamage(damage);
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + (transform.right * range * playerMovement.FacingDirection * colliderDistance),
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }*/


    private void StartBlocking()
    {
        Blocking = true;
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
    }

    private void StopBlocking()
    {
        animator.SetBool("IdleBlock", false);
        Blocking = false;
    }
}