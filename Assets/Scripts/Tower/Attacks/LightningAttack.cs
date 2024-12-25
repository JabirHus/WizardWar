using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    private Transform target;               // Target for the projectile
    private float speed;                    // Speed of the projectile
    public float damage = 15f;              // Damage dealt to the main target
    public float chainRadius = 5f;          // Radius for the chain reaction
    public float chainDamage = 10f;         // Damage dealt to chained enemies
    public int maxChains = 3;               // Maximum number of chain reactions
    public GameObject lightningBurstEffect; // Prefab for the lightning burst visual effect

    // Method to set the target and speed
    public void Seek(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move toward the target
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Check if the projectile reached the target
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

  void HitTarget()
    {
        // Apply damage to the main target directly through EnemyHealth component
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Apply damage
        }

        // Spawn the lightning burst visual effect
        if (lightningBurstEffect != null)
        {
            Instantiate(lightningBurstEffect, transform.position, Quaternion.identity);
        }

        // Apply the chain reaction
        ApplyChainReaction();

        // Destroy the projectile after hitting the target
        Destroy(gameObject);
    }

    void ApplyChainReaction()
    {
        // Get all enemies within the chain radius
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, chainRadius);
        int chainsLeft = maxChains;

        foreach (Collider collider in hitEnemies)
        {
            if (collider.CompareTag("Enemy") && chainsLeft > 0)
            {
                // Damage each enemy in the chain radius directly through EnemyHealth component
                EnemyHealth chainedEnemyHealth = collider.GetComponent<EnemyHealth>();
                if (chainedEnemyHealth != null && chainedEnemyHealth != target.GetComponent<EnemyHealth>())
                {
                    chainedEnemyHealth.TakeDamage(chainDamage);
                    chainsLeft--;

                    // Optionally spawn visual effects on chained enemies
                    if (lightningBurstEffect != null)
                    {
                        Instantiate(lightningBurstEffect, collider.transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        // Visualize the chain radius in the editor for debugging
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chainRadius);
    }
}
