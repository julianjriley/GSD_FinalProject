using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<float> HealthChanged;

    public float maxHealth = 5;
    float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0 )
        {
            //TODO: What happens when the player dies
        }
        Debug.Log(health);
    }
}
