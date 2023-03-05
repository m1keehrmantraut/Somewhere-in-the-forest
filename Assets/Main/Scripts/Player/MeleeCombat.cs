using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRange = 1f;

    private bool attackStatus = true;
    private float timeBtwAttacks = 1f;
    
    IEnumerator AttackTime(float timeBtwAttacks)
    {
        attackStatus = false;
        yield return new WaitForSeconds(timeBtwAttacks);
        attackStatus = true;
    }
    
    public void MeleeAttack()
    {
        if (attackStatus)
        {
            var hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (var enemy in hitEnemy)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamageEnemy(attackDamage);
            }    
            
            StartCoroutine(AttackTime(timeBtwAttacks));
        }
        
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
