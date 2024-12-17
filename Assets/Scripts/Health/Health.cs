using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    public float CurrentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Math.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;

                // Stop player from moving when dead
                body.gravityScale = 0;
                body.linearVelocity = Vector3.zero;
            }
        }
    }

    public void AddHealth(float _value)
    {
        CurrentHealth = Math.Clamp(CurrentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

            spriteRenderer.color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}
