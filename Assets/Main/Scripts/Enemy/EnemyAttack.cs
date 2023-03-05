using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth player;

    private bool attackStatus = true;
    private int attackDamage = 5;
    private float timeBtwAttacks = 2f;
    
    // private float powerForce = 100f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    
    IEnumerator AttackTime(float timeBtwShots)
    {
        attackStatus = false;
        yield return new WaitForSeconds(timeBtwShots);
        attackStatus = true;
    }

    private void OnCollisionStay2D()
    {
        if (attackStatus)
        {
            player.TakeDamage(attackDamage);
            
            /*var pushDirection = player.gameObject.transform.position - this.transform.position;
            player.GetComponent<Rigidbody2D>().AddForce(pushDirection.normalized * powerForce);*/
            
            StartCoroutine(AttackTime(timeBtwAttacks));
        }
    }
}
