using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] path;  // assigned waypoint path
    private int currentWaypointIndex = 0;
    public float speed = 2f;

    public GameObject MainTower; // Reference to the central fortress
    public float damageToFortress = 10f; 



    void Start()
    {
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

}

