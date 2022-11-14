using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureHealth : MonoBehaviour
{
    public CreatureStats stats;
    public Image graphics;
    public HealthBar healthBar;

    private void Awake()
    {
        //stats = new CreatureStats("Sutik", 5, 5, 50);
    }

    public void SetHealthAndMaxHealth()
    {
        healthBar.SetMaxHealth(stats.maxHealth);
        healthBar.SetHealth(stats.maxHealth);
    }


    public void Heal()
    {
        if (stats.currentHealth + (stats.maxHealth * 0.1) >= stats.maxHealth)
        {
            stats.currentHealth = stats.maxHealth;
        }
        else
        {
            stats.currentHealth += stats.maxHealth / 10;
        }
        healthBar.SetHealth(stats.currentHealth);
    }

    public bool TakeDamage(int _damage)
    {
        if (stats.currentHealth - _damage <= 0)
        {
            stats.currentHealth = 0;
            healthBar.SetHealth(stats.currentHealth);
            return true;
        }
        else
        {
            stats.currentHealth -= _damage;
            healthBar.SetHealth(stats.currentHealth);
            return false;
        }
    }
}
