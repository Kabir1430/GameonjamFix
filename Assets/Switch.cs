using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{




    private void Start()
    {
        
    StartCoroutine(Check());

    }



      IEnumerator Check()

    {

        yield return new WaitForSeconds(24);


        SceneManager.LoadScene(2);
        //SceneManager.LoadScene();
    }


    // Start is called before the first frame update
  }
