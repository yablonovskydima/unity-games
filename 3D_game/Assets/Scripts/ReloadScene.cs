using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump") && PlayerMovement.instance.isDead == true)
        {
            Reload();
        }
    }

    public void Reload() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
    