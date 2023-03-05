using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int healthEnemy = 100; 
    [SerializeField] private LevelDisplay levelDisplay;

    [SerializeField] GameObject deathEffect;

    public void  TakeDamageEnemy (int damage)
    {
        healthEnemy -= damage;

        if (healthEnemy <= 0)
        {
            DieEnemy();
        }
    }

    void DieEnemy ()
    {
        levelDisplay.levelSystem.AddExperience(100);
        Destroy(gameObject);
    }
}
