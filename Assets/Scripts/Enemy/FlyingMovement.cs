using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 10f;
    public GameObject MainTower; // Reference to the central fortress
    public float altitude = 20f; // Desired altitude
    public float damageToFortress = 20f; // Damage dealt to the fortress

    void Start()
    {
        waypoints = new Transform[3]; 

        waypoints[0] = GameObject.Find("Waypoint1").transform;
        waypoints[1] = GameObject.Find("Waypoint2").transform;
        waypoints[2] = GameObject.Find("Waypoint3").transform;
    }

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            Vector3 targetPosition = targetWaypoint.position;
            targetPosition.y = altitude; //  desired altitude set as target position

            // Move the enemy towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            

            // Check if the enemy has reached the waypoint
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
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

