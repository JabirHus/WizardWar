using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    public float damage = 10f;           // Damage dealt on hit
    public float slowDuration = 3f;      // Duration of the slow effect
    public float slowMultiplier = 0.5f;  // 50% speed reduction

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

        // If the projectile reaches the target, hit it
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // Deal damage and apply the slow effect
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ApplySlow(slowMultiplier, slowDuration); // Apply the slow effect
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Apply damage
            }
        }

        Destroy(gameObject); // Destroy the projectile after impact
    }
}
