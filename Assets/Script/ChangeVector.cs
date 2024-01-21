using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVector : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Enemy;
    [SerializeField]
     float outsideY, insideY;



    private void OnTriggerStay(Collider other)
    {
     if(other.gameObject.tag == "outside")
     {
      Vector3 newPosition = new Vector3(transform.position.x,outsideY, transform.position.z); // Change this to your desired position
            Enemy.position = newPosition;
            Debug.Log("enemy out");
     }
     if(other.gameObject.tag == "inside")
     {
      Vector3 newPosition = new Vector3(transform.position.x, insideY , transform.position.z); // Change this to your desired position
            Enemy.position = newPosition;
            Debug.Log("enemy in");
            
     }
    }














}
