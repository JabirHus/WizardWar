using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] path;  // assigned waypoint path
    private int currentWaypointIndex = 0;
    public float speed = 2f;
    private float defaultSpeed;
    private bool isSlowed = false;
    private Color originalColor;
    private Material enemyMaterial;

    public GameObject MainTower; // Reference to the central fortress
    public float damageToFortress = 10f; 



    void Start()
    {

        // Cache the material and store the original color
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            enemyMaterial = renderer.material; // Cache the material
            originalColor = enemyMaterial.color; // Store the original color
        }
        else
        {
            Debug.LogError("Renderer not found on enemy object. Ensure the enemy has a Renderer component.");
        }


        if (path == null || path.Length == 0)
        {
            Debug.Log("No path assigned or path is empty.");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (currentWaypointIndex < path.Length)
        {
            Transform targetWaypoint = path[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }


    // Check if the enemy collides with the MainTower
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == MainTower) 
        {
            FortressHealth fortressHealth = MainTower.GetComponent<FortressHealth>();
            if (fortressHealth != null)
            {
                fortressHealth.TakeDamage(damageToFortress); // Deal damage to the fortress
            }
            
            // Destroy the enemy after it damages the MainTower
            Destroy(gameObject);
        }
    }

       public void ApplySlow(float slowMultiplier, float duration)
    {
        if (isSlowed) return; // Prevent stacking slows

        isSlowed = true; // Set the slow flag
        defaultSpeed = speed; // Save the current speed as default
        speed *= slowMultiplier; // Apply the slow effect

        // Add the visual effect: Change color to blue
        StartCoroutine(HandleSlowEffect(duration));
    }

    IEnumerator HandleSlowEffect(float duration)
    {
        if (enemyMaterial != null)
        {
            enemyMaterial.color = Color.blue; // Temporarily change the color to blue
        }

        yield return new WaitForSeconds(duration); // Wait for the slow duration

        // Reset speed and visual effects
        speed = defaultSpeed;
        isSlowed = false;

        if (enemyMaterial != null)
        {
            enemyMaterial.color = originalColor; // Restore the original color
        }
    }


}

