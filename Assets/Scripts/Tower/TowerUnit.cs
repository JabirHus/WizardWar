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
        if (projectilePrefab != null)
        {
            FireProjectile(projectilePrefab);
        }
        else
        {
            Debug.LogError("Projectile prefab is not assigned!");
        }
    }

    void FireProjectile(GameObject projectileType)
    {
        // Instantiate the projectile
        GameObject projectileInstance = Instantiate(projectileType, transform.position, Quaternion.identity);

        // Check for BurnProjectile script
        BurnProjectile burnProjectileScript = projectileInstance.GetComponent<BurnProjectile>();
        if (burnProjectileScript != null)
        {
            burnProjectileScript.Seek(target, projectileSpeed);
            Debug.Log("Fired a burn projectile at target: " + target.name);
            return;
        }

        // Check for IceProjectile script
        IceProjectile iceProjectileScript = projectileInstance.GetComponent<IceProjectile>();
        if (iceProjectileScript != null)
        {
            iceProjectileScript.Seek(target, projectileSpeed);
            Debug.Log("Fired an ice projectile at target: " + target.name);
            return;
        }

        // Check for LightningProjectile script
        LightningProjectile lightningProjectileScript = projectileInstance.GetComponent<LightningProjectile>();
        if (lightningProjectileScript != null)
        {
            lightningProjectileScript.Seek(target, projectileSpeed);
            Debug.Log("Fired a lightning projectile at target: " + target.name);
            return;
        }

        // Check for regular Projectile script
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Seek(target, projectileSpeed);
            Debug.Log("Fired a normal projectile at target: " + target.name);
            return;
        }

        // Log an error if no valid script was found
        Debug.LogError("No valid projectile script found on the projectile prefab!");
    }



}