using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    // Properties
    public float CurrentHealth { get; private set; }
    private bool dead;
    public bool isInvulnerable { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    // References
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

                foreach (Behaviour item in components)
                {
                    item.enabled = false;
                }

                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        CurrentHealth = Math.Clamp(CurrentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

            spriteRenderer.color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
        isInvulnerable = false;
    }
}
