using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    
    private PlayerHealth player;
    private Vector3 difference;
    
    private float offset = -90f;
    private float rotZ;
    private float distance = 15f;

    private float timeBtwShots = 3f;
    private int bulletCount = 10;
    private float time = 5f;
    private bool shootStatus = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    IEnumerator Recharge(float time)
    {
        yield return new WaitForSeconds(time);
        bulletCount = 10;
    }

    IEnumerator ShootTime(float timeBtwShots)
    {
        shootStatus = false;
        yield return new WaitForSeconds(timeBtwShots);
        shootStatus = true;
    }
    
    void FixedUpdate()
    {
        difference = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        Shoot();
    }
    
    public void Shoot()
    {
        if (shootStatus)
        {
            if (Vector3.Distance(transform.position, player.transform.position)
                <= distance && bulletCount != 0)
            {
                Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                bulletCount--;
                StartCoroutine(ShootTime(timeBtwShots));
            }
            else if (bulletCount == 0)
            {
                StartCoroutine(Recharge(time));
            }
        }
    }

    public void StopShooting()
    {
        shootStatus = false;
    }
}
