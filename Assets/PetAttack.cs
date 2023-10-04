using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttack : MonoBehaviour
{

    private EnemyHealth enemy;
    private bool attackStatus = true;
    private int attackDamage = 20;
    private float timeBtwAttacks = 2f;

    IEnumerator AttackTime(float timeBtwShots)
    {
        attackStatus = false;
        yield return new WaitForSeconds(timeBtwShots);
        attackStatus = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attackStatus && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamageEnemy(attackDamage);
            
            StartCoroutine(AttackTime(timeBtwAttacks));
        }
    }

}
