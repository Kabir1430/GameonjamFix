
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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


    [Header("Script")]

    public EnemyAi Enemy;
    public int Damage;
    [SerializeField]public RaycastHit hitInfo,check;


[Header("Script")]

    public GameObject BrickEffect;
    public string targetTag;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
     //   CheckShoot();
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
 
   
   
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);
 
        Anim.SetBool("Shake", true);
       // Enemy = hitInfo.collider.GetComponent<EnemyAi>();
        //De
        if (Physics.Raycast(ray, out hitInfo, range))
        {
         
           // Debug.Log("Hit: " + hitInfo.collider.name);

            // Example: If the hit object has a rigidbody, apply force
            Rigidbody hitRigidbody = hitInfo.collider.GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                hitRigidbody.AddForceAtPosition(ray.direction * 10f, hitInfo.point);
            }

            if(hitInfo.collider.tag=="Enemy")  
            {
                Enemy = hitInfo.collider.GetComponent<EnemyAi>();
                
              //  Enemy = check.collider.GetComponent<EnemyAi>();
                Debug.Log("Enemy");

                Enemy.TakeDamage(Damage);
            }
            else if(hitInfo.collider.tag=="Brick")
            {
                Instantiate(BrickEffect, hitInfo.point, Quaternion.identity);

                Debug.Log("Brick");
            }
            else if (hitInfo.collider.CompareTag(targetTag))
            {

                Debug.Log("Brick");// Instantiate the objectToInstantiate at the collision point
                Instantiate(BrickEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
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



    void CheckShoot()
    {
        Ray ray = new Ray(gunTip.position, gunTip.forward);

        Debug.DrawRay(ray.origin, ray.direction * range, Color.blue, 0.1f);
             
      
        if (Physics.Raycast(ray, out check,range))
        {

            if (check.collider.tag == "Enemy")
            {
                Debug.Log("Enemy Get");

               // Enemy = check.collider.GetComponent<EnemyAi>();

               // Enemy.TakeDamage(Damage);
            }
        }
    }


}


