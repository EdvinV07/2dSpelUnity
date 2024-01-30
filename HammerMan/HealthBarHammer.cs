using UnityEngine;
using UnityEngine.UI;

public class HealthBarHammer: MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 10f;
    public float currentHealth;
    public int DamageTaken = 1; // Add this line

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthBar();
    }
}