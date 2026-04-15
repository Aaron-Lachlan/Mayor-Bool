using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BoolSceneManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!SceneManager.GetSceneByName("MayorBoolScene").isLoaded)
        {

            SceneManager.LoadSceneAsync("MayorBoolScene", LoadSceneMode.Additive);
               

        }
    }

    
}
