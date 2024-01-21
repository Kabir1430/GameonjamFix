using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gunTip;
    public float range = 100f;
    public LayerMask targetLayer; // Specify the layer for objects that can be shot
    

    public RaycastHit hitInfo;
    public bool isCollide;


    public Door door;

    public bool DoorOpen;

    void Update()
    {
        Shoot();

        Check();
    }
    void Shoot()
    {

        Ray ray = new Ray(gunTip.position, gunTip.forward);
        RaycastHit hitInfo;
        //    CameraShake.StartShake(instensity,duration);
        // Visualize the ray in the scene for debugging

        Debug.DrawRay(ray.origin, ray.direction * range, Color.green, 0.1f);
      //  Fire.Play();
        //  CameraShaker.Instance.ShakeOnce(0, 0, 0,0);
      //  Anim.SetBool("Shake", true);
        if (Physics.Raycast(ray, out hitInfo, range))
        {
            // The ray hit a valid target
            if(hitInfo.collider.tag=="Door")
            {
              door =  hitInfo.collider.GetComponent<Door>();
                //De
                if (Input.GetKeyDown(KeyCode.E) && !door.DoorisOpen)

                {
                    DoorOpen = true;
                  //  StartCoroutine(Open());

                    //isCollide = true;
                    Debug.Log("Door");

                }
                else if(Input.GetKeyDown(KeyCode.E) && door.DoorisOpen)
                {
                    DoorOpen = false;
                 //   StartCoroutine(Close());
                }

            }
        }
    }
    /*
IEnumerator Open()
    {

              
        yield return new WaitForSeconds(2);
    
        DoorOpen = false;
    }



    IEnumerator Close()
    {


        yield return new WaitForSeconds(2);

        DoorOpen = false;
    }
    */
    void Check()

    {

        if(DoorOpen)
        {
            door.Open();
            Debug.Log("Door Update");
        }
        else if(!DoorOpen)
        {
            door.Close();
        }
    }

}   
