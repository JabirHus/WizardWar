using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab
    public float fireRate = 1.0f; // Time between shots
    public float projectileSpeed = 10.0f; // Speed of the projectile
    public float attackRange = 10.0f; // Range of the tower

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        // Find the nearest enemy within range
        FindTarget();

        // If there is a target within range, fire at it
        if (target != null && fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        // Countdown until the next shot
        fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Set the target if we found an enemy within range
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        // Instantiate the projectile and set its position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Seek(target, projectileSpeed);
        }
    }
}
