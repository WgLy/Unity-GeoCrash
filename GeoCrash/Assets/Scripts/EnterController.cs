using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterController : MonoBehaviour
{
    public FadingController fadingController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            fadingController.Fade(false, "MainScene");
        }
    }
}
