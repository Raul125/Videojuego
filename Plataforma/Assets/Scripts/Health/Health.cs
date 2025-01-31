using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth { get; private set; }

    private Animator animator;
    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
        }
        else if (!isDead)
        {
            animator.SetBool("noBlood", false);
            animator.SetTrigger("Death");
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            isDead = true;
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(1);
        }
    }
}
