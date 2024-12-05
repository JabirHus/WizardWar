using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    public float speed = 15f;
    public GameObject MainTower; // Reference to the central fortress
    public float altitude = 50f; // Desired altitude
    public float damageToFortress = 20f; // Damage dealt to the fortress


    void Start(){
        if (MainTower != null){
        
            Vector3 targetPosition = new Vector3(MainTower.transform.position.x, altitude, MainTower.transform.position.z);

            // Move toward the central fortress 
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            //Face Main Tower
            transform.LookAt(targetPosition);
        }
        else{
            Debug.LogError("MainTower is not assigned");
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

