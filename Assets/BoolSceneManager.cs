using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class BoolSceneManager : MonoBehaviour
{
    public static BoolSceneManager current;

    private void Awake()
    {
        current = this;     
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadNewDay()
    {
        if (!SceneManager.GetSceneByName("MayorBoolScene").isLoaded)
        {

            SceneManager.LoadSceneAsync("MayorBoolScene", LoadSceneMode.Additive);

        }
    }

    
}
