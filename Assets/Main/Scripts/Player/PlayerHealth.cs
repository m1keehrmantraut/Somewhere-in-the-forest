using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    void Start () 
    {
        currentHealth = maxHealth;    
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    
}
