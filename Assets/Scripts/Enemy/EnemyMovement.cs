using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 2f;

    public GameObject MainTower; // Reference to the central fortress
    public float damageToFortress = 10f; 



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

