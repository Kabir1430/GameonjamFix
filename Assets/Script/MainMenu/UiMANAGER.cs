using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMANAGER : MonoBehaviour
{
    // Start is called bef
    // ore the first frame update
   public void LoadLevel(string Level)
    {
        SceneManager.LoadScene(1    );
    }
    public void Quit()
    {
      
        //Application.Quit();
    }
}
