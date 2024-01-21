using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVector : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Enemy;



    private void OnTriggerStay(Collider other)
    {
     if(other.gameObject.tag == "outside")
     {
      Vector3 newPosition = new Vector3(0f, 1f, 0f); // Change this to your desired position
            Enemy.position = newPosition;
            Debug.Log("enemy out");
     }
     if(other.gameObject.tag == "inside")
     {
      Vector3 newPosition = new Vector3(0f, -1f, 0f); // Change this to your desired position
            Enemy.position = newPosition;
            
     }
    }














}
