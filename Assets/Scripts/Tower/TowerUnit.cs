using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab
    public float fireRate = 1.0f; // Time between shots
    public float projectileSpeed = 10.0f; // Speed of the projectile
    public float attackRange = 10.0f; // Range of the tower
    public float baseDamage = 10.0f; // Base damage of the tower

    // Upgrade costs
    public int rangeUpgradeCost = 50;
    public int damageUpgradeCost = 75;
    public int fireRateUpgradeCost = 100;

    public GameObject floatingTextPrefab;
    public Material highlightMaterial; // Material for highlight effect
    public float highlightDuration = 2.0f; // Duration of highlight effect

    private float fireCountdown = 0f;
    private Transform target;
    private List<GameObject> activeFloatingTexts = new List<GameObject>(); // To track active floating text instances


    private Material originalMaterial; // To store the original material
    private Renderer towerRenderer; // Renderer for the tower

    void Start()
    {
        towerRenderer = GetComponent<Renderer>();
        if (towerRenderer != null)
        {
            originalMaterial = towerRenderer.material;
        }
    }

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
        ProjectileBehaviour projectileScript = projectileInstance.GetComponent<ProjectileBehaviour>();
        if (projectileScript != null)
        {
            projectileScript.Seek(target, projectileSpeed);
            Debug.Log("Fired a normal projectile at target: " + target.name);
            return;
        }

        // Log an error if no valid script was found
        Debug.LogError("No valid projectile script found on the projectile prefab!");
    }


    public void ShowFloatingText(string message)
    {
        // Check if there is already an active floating text for this tower
        if (activeFloatingTexts.Count > 0)
        {
            // Destroy the previous floating text
            foreach (var text in activeFloatingTexts)
            {
                if (text != null)
                {
                    Destroy(text);
                }
            }
            activeFloatingTexts.Clear();
        }

        if (floatingTextPrefab != null)
        {
            // Instantiate the new floating text
            GameObject floatingTextInstance = Instantiate(floatingTextPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            FloatingText floatingTextScript = floatingTextInstance.GetComponent<FloatingText>();

            if (floatingTextScript != null)
            {
                floatingTextScript.SetText(message); // Set the message
            }

            // Add the new text to the list
            activeFloatingTexts.Add(floatingTextInstance);
        }
    }


    // Highlight the tower temporarily
    public void Highlight()
    {
        if (towerRenderer != null && highlightMaterial != null)
        {
            towerRenderer.material = highlightMaterial; // Apply the highlight material
            StartCoroutine(RemoveHighlightAfterDelay());
        }
    }

    private IEnumerator RemoveHighlightAfterDelay()
    {
        yield return new WaitForSeconds(highlightDuration);
        if (towerRenderer != null)
        {
            towerRenderer.material = originalMaterial; // Revert to the original material
        }
    }


    // Upgrade Methods
    public void UpgradeRange()
    {
        attackRange += 2.0f; // Increase the attack range
        ShowFloatingText("+2 Range"); // Ensure this message is correct
        Highlight(); // Highlight the tower
        Debug.Log("Range upgraded! New range: " + attackRange);
    }

    public void UpgradeDamage()
    {
        baseDamage += 5.0f; // Increase the damage
        ShowFloatingText("+5 Damage"); // Ensure this message is correct
        Highlight(); // Highlight the tower
        Debug.Log("Damage upgraded! New damage: " + baseDamage);
    }

    public void UpgradeFireRate()
    {
        fireRate += 0.5f; // Increase the fire rate
        ShowFloatingText("+0.5 Fire Rate"); // Ensure this message is correct
        Highlight(); // Highlight the tower
        Debug.Log("Fire rate upgraded! New fire rate: " + fireRate);
    }



}