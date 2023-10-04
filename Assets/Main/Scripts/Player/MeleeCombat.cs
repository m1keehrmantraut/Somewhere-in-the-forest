using System.Collections;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private float attackDamage = 10;
    [SerializeField] private float attackRange = 1f;

    [SerializeField] private Animator _animator;

    private float boost = 0f;

    private bool attackStatus = true;
    private float timeBtwAttacks = 1f;

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
                enemy.GetComponent<EnemyHealth>().TakeDamageEnemy(attackDamage + attackDamage * boost);
                Debug.Log(boost);
                relativePosition = gameObject.transform.InverseTransformPoint(enemy.transform.position);
            }

            _animator.SetFloat("Horizontal", relativePosition.x);
            _animator.SetFloat("Vertical", relativePosition.y);
            
            _animator.SetBool("IsPressed", true);
            
            StartCoroutine(AttackTime(timeBtwAttacks));
        }
        
    }

    public void IncreaseBoost(float velocity)
    {
        boost += velocity;
        Debug.Log("incr");
    }
    
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
