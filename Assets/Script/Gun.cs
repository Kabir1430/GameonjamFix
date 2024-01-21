
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTip;
    public float range = 100f;
    public LayerMask targetLayer; // Specify the layer for objects that can be shot
    public float shootCooldown = 0.5f; // Adjust the cooldown time as needed
    public float reloadTime = 2f; // Adjust the reload time as needed
    public int magazineSize = 10; // Adjust the magazine size as needed

    private float currentCooldown = 0f;
    private int currentAmmo;

  //  public CameraShake CameraShake;

    [Header("Audio")]

    public AudioSource Fire;

    [Header("Animation")]

    public Animator Anim;


   /* [Header("Script")]

    public CameraShake CameraShake;
    public float instensity, duration;

    */

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        // Update the cooldown timer
        currentCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && currentCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            // Reset the cooldown timer
            currentCooldown = shootCooldown;
            currentAmmo--;
        }
        

        if (Input.GetKey(KeyCode.R) && currentAmmo < magazineSize)
        {
            Reload();
        }
    
    
        if(Anim.GetCurrentAnimatorStateInfo(0).IsName("Shake"))
        {
            Anim.SetBool("Shake", false);

            Debug.Log("Anim is over");
        }
        else if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
       {
            Anim.SetBool("Reload", false);

        }
    }

    void Shoot()
    {
        Ray ray = new Ray(gunTip.position, gunTip.forward);
        RaycastHit hitInfo;
    //    CameraShake.StartShake(instensity,duration);
        // Visualize the ray in the scene for debugging
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);
      //  CameraShaker.Instance.ShakeOnce(0, 0, 0,0);
        Anim.SetBool("Shake", true);
    
        if (Physics.Raycast(ray, out hitInfo, range, targetLayer))
        {
            // The ray hit a valid target
            Debug.Log("Hit: " + hitInfo.collider.name);

            // Example: If the hit object has a health script, decrease its health
            //  Health hitHealth = hitInfo.collider.GetComponent<Health>();
          /* if (hitHealth != null)
            {
                hitHealth.TakeDamage(10); // Adjust the damage as needed
            }
          */
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
        Anim.SetBool("Reload", true);
    }


}


