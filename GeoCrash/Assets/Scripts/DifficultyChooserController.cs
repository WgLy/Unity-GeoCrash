using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChooserController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int selfIndex;
    public int currentIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(selfIndex == currentIndex){
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                1.0f
            );
        }else{
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                0.5f
            );
        }

        
        
        if(Input.GetKeyDown(KeyCode.A)){
            currentIndex-=1;
            if(currentIndex==-1) currentIndex = 2;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            currentIndex+=1;
            if(currentIndex==3) currentIndex = 0;
        }
    }
}
