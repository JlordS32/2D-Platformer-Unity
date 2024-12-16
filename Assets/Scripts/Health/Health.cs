using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float CurrentHealth {get; private set;}
    private void Awake()
    {
        CurrentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Math.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {

        }
        else
        {

        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            TakeDamage(1);
        }

        Debug.Log("Current Health: " + CurrentHealth);
    }
}
