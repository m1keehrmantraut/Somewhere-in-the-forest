using System;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private const int maxBulletCount = 100;
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private ManaBar manaBar;
    [SerializeField] private LayerMask enemyLayers;

    private Vector3 difference;

    private float distance = 20f;
    private float rotZ;
    private float offset = -90f;

    private GameObject enemy;

    private float bulletCount = 100f;
    private float timeBtwShots = 0.7f;
    private float time = 10f;
    private bool shootStatus = true;

    private void Start()
    {
        enemy = FindClosestObjectInRange();
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
            enemy = FindClosestObjectInRange();
        }
        
        if (shootStatus && bulletCount > 19 && enemy != null)
        {
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            bulletCount -= 20;
            manaBar.SetMana(bulletCount);
            StartCoroutine(ShootTime(timeBtwShots));
        }

        enemy = FindClosestObjectInRange();
    }
    
    // Это метод для поиска ближайшего объекта в радиусе взгляда. 
    private GameObject FindClosestObjectInRange()
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
 
        return closestObject;
    }

}