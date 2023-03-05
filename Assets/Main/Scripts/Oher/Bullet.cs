using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public int damage = 10;

    public LayerMask whatIsSolid;
    public GameObject destroyEffect;

    [SerializeField] bool enemyBullet;

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyHealth>().TakeDamageEnemy(damage);
            }
            
            if(hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitInfo.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void DestroyBullet()
    {
        GameObject im = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(im, 1f);
        Destroy(gameObject);
    }
}
