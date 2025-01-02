using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack1 : MonoBehaviour
{
    private Transform target;
    private float speed;
    public float damage = 20f; // Damage amount the projectile deals

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

        // Move towards the target
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If the projectile reaches the target, hit the target
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // Check if the target has an normal EnemyHealth component
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Apply damage to the enemy
        }

        Destroy(gameObject); // Destroy the projectile when it hits the target
    }
}
