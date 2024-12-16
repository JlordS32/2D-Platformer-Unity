using System;
using UnityEngine;

public class Heatlh : MonoBehaviour
{
    [SerializeField] private float _startingHealth;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        _currentHealth = Math.Clamp(_currentHealth - _damage, 0, _startingHealth);

        if (_currentHealth > 0)
        {

        }
        else
        {

        }
    }
}
