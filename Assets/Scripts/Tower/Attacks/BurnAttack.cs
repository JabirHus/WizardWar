using System.Collections;
using UnityEngine;

public class BurnProjectile : MonoBehaviour
{
    private Transform target;          // Target for the projectile
    private float speed;               // Speed of the projectile
    public float damage = 10f;         // Initial damage dealt on hit
    public float burnDamage = 5f;      // Damage dealt per second in the burn area
    public float burnDuration = 3f;    // Duration of the burn effect
    public float burnRadius = 5f;      // Radius of the burn area
    public GameObject burnAreaPrefab;  // Prefab for the burn area visual effect

    // Function to initialize the projectile with a target and speed
    public void Seek(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    void Update()
    {
        // If no target, destroy the projectile
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move towards the target
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
        // Apply initial damage to the target
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        // Create the burn area effect at the hit position
        CreateBurnArea(transform.position);

        // Destroy the projectile
        Destroy(gameObject);
    }

    void CreateBurnArea(Vector3 position)
    {
        // Check if the burn area prefab is assigned
        if (burnAreaPrefab == null)
        {
            Debug.LogError("BurnAreaPrefab is not assigned!");
            return;
        }

        // Instantiate the burn area prefab at the position
        GameObject burnArea = Instantiate(burnAreaPrefab, position, Quaternion.identity);

        // Apply burn damage to enemies in the area
        StartCoroutine(ApplyBurnDamage(burnArea));

        // Destroy the burn area visual after the duration ends
        Destroy(burnArea, burnDuration);
    }

    IEnumerator ApplyBurnDamage(GameObject burnArea)
    {
        float elapsedTime = 0f;
        while (elapsedTime < burnDuration)
        {
            // Detect all enemies within the burn radius
            Collider[] hitEnemies = Physics.OverlapSphere(burnArea.transform.position, burnRadius);
            foreach (Collider enemyCollider in hitEnemies)
            {
                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(burnDamage); // Apply burn damage
                }
            }

            elapsedTime += 1f; // Apply burn damage every second
            yield return new WaitForSeconds(1f);
        }
    }

    // Visualize the burn radius in the editor for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, burnRadius);
    }
}
