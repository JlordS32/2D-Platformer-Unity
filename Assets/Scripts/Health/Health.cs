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

    [Header("Sound")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

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
        if (isInvulnerable) return;
        CurrentHealth = Math.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            SoundManager.instance.playSound(hurtSound);
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {

                foreach (Behaviour item in components)
                {
                    item.enabled = false;
                }

                animator.SetTrigger("die");
                animator.SetBool("grounded", true);

                dead = true;
                SoundManager.instance.playSound(deathSound);
            }
        }
    }

    public void AddHealth(float _value)
    {
        CurrentHealth = Math.Clamp(CurrentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invulnerability());

        foreach (Behaviour item in components)
        {
            item.enabled = true;
        }
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
