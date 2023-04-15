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

    [SerializeField] private Animator _animator;

    private bool attackStatus = true;
    private float timeBtwAttacks = 0.5f;
    
    IEnumerator AttackTime(float timeBtwAttacks)
    {
        attackStatus = false;
        yield return new WaitForSeconds(timeBtwAttacks);
        attackStatus = true;
        _animator.SetBool("IsPressed", false);
    }
    
    public void MeleeAttack()
    {
        if (attackStatus)
        {
            Vector2 relativePosition = new Vector2(0f, 0f);
            var hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (var enemy in hitEnemy)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamageEnemy(attackDamage);
                relativePosition = gameObject.transform.InverseTransformPoint(enemy.transform.position);
            }

            _animator.SetFloat("Horizontal", relativePosition.x);
            _animator.SetFloat("Vertical", relativePosition.y);
            
            _animator.SetBool("IsPressed", true);
            
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
