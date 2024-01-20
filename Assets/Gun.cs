using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gunTip;
    public float range = 100f;
    public LayerMask targetLayer; // Specify the layer for objects that can be shot
    public float shootCooldown = 0.5f; // Adjust the cooldown time as needed
    public float reloadTime = 2f; // Adjust the reload time as needed
    public int magazineSize = 10; // Adjust the magazine size as needed

    private float currentCooldown = 0f;
    private int currentAmmo;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        // Update the cooldown timer
        currentCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && currentCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            // Reset the cooldown timer
            currentCooldown = shootCooldown;
            currentAmmo--;
        }

        if (Input.GetButtonDown("Reload") && currentAmmo < magazineSize)
        {
            Reload();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(gunTip.position, gunTip.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, range, targetLayer))
        {
            // The ray hit a valid target
            Debug.Log("Hit: " + hitInfo.collider.name);

            
            // Example: If the hit object has a rigidbody, apply force
            Rigidbody hitRigidbody = hitInfo.collider.GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                hitRigidbody.AddForceAtPosition(ray.direction * 10f, hitInfo.point);
            }

            // You can add more actions based on the type of object hit
        }
    }

    void Reload()
    {
        // Perform reload logic here
        Debug.Log("Reloading...");
        currentAmmo = magazineSize;
    }
}




