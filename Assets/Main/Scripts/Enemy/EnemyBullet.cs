using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int enemyDamage = 10;
    public GameObject hitEffect;
    public GameObject impactEffect; 
    
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(enemyDamage);
        }

        GameObject imEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(imEffect, 1f);
        Destroy(gameObject);
    }
    
}