using System;
using Saucy.Modules.XP;
using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    public int healthEnemy = 100;
    private XPGranter enemyXP;

    [SerializeField] GameObject deathEffect;

    public void Start()
    {
        enemyXP = gameObject.GetComponent<XPGranter>();
    }
    
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
        enemyXP.GrantXP();
        Destroy(gameObject);
    }
}
