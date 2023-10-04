using Saucy.Modules.XP;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public float healthEnemy = 100f;
    private XPGranter enemyXP;

    private PetAI _foxe;
    private AttackMode _attackMode;

    [SerializeField] private DialogueManager manager;
        
    [SerializeField] GameObject deathEffect;

    [SerializeField] private Animator _animator;
    
    private void Start()
    {
        enemyXP = gameObject.GetComponent<XPGranter>();
        _foxe = GameObject.FindWithTag("Pet").GetComponent<PetAI>();
        _attackMode = GameObject.FindWithTag("ShootButton").GetComponent<AttackMode>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AttackPoint"))
        {
            _attackMode.SetMelee();    
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AttackPoint"))
        {
            _attackMode.SetShooting();
        }
    }
    
    public void TakeDamageEnemy (float damage)
    {
        
        _animator.SetBool("IsHit", true);
        StartCoroutine(HitTime());
        healthEnemy -= damage;
        
        if (healthEnemy <= 0)
        {
            DieEnemy();
        }
    }
    
    IEnumerator HitTime()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("IsHit", false);
    }
    
    void DieEnemy ()
    {
        _foxe.SetMainTarget();
        gameObject.GetComponent<SpawnDead>().Spawn(transform);
        GlobalEventManager.SendEnemyKilled(1);
        enemyXP.GrantXP();
        Destroy(gameObject);
    }
}
