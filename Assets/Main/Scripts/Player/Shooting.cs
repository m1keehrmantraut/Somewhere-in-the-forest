using System;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private float maxBulletCount = 100f;
    
    [Header("Shoot Logic")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private ManaBar manaBar;
    [SerializeField] private LayerMask enemyLayers;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;

    private GameObject enemy;
    
    private Vector3 difference;
    private float distance = 10f;
    private float rotZ;
    private float offset = -90f;

    private float bulletCount;
    private float timeBtwShots = 0.8f;
    private float time = 10f;
    
    private bool shootStatus = true;

    private void Awake()
    {
        FindClosestObjectInRange();
        audioSource = GameObject.FindGameObjectWithTag("ShootEffect").GetComponent<AudioSource>();
        bulletCount = maxBulletCount;
    }
    

    IEnumerator ShootTime(float timeBtwShots)
    {
        shootStatus = false;
        yield return new WaitForSeconds(timeBtwShots);
        shootStatus = true;
    }

    void FixedUpdate()
    {
        if (enemy != null)
        {
            difference = enemy.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        }
        bulletCount += time * Time.deltaTime / 2;
        bulletCount = Mathf.Clamp(bulletCount, 0f, maxBulletCount);
        manaBar.SetMana(bulletCount);
    }

    public void Shoot()
    {
        if (enemy != null && Vector2.Distance(enemy.transform.position, shotPoint.position) > 5f)
        {
            FindClosestObjectInRange();
        }
        
        if (shootStatus && bulletCount > 19f && enemy != null)
        {
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            audioSource.PlayOneShot(hitSound); 
            bulletCount -= 20f;
            manaBar.SetMana(bulletCount);
            StartCoroutine(ShootTime(timeBtwShots));
        }
    }
    
    public void IncreaseMana(float mana)
    {
        maxBulletCount += mana;
        bulletCount = maxBulletCount;
        manaBar.SetMaxMana(maxBulletCount);
    }
    
    // Это метод для поиска ближайшего объекта в радиусе взгляда. 
    public void FindClosestObjectInRange()
    {
        // Получаем массив объектов в радиусе взгляда
        var objectsInArea = Physics2D.OverlapCircleAll(shotPoint.position, distance, enemyLayers);
        
        // Проходим по объектам и ищем ближайший
        GameObject closestObject = null;
        var minDist = distance;
        foreach (var obj in objectsInArea)
        {
            // Получаем дистанцию до объекта
            var dist = Vector2.Distance(obj.transform.position, shotPoint.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestObject = obj.gameObject;
            }
        }
 
        enemy = closestObject;
        
    }
}