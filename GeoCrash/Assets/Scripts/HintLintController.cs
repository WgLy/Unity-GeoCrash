using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintLintController : MonoBehaviour
{
    public float gameTime;
    public SpriteRenderer spriteRenderer;
    public DataSenderController dataSenderController;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        
        dataSenderController = FindObjectOfType<DataSenderController>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if(gameTime>=3.5f * 60.0f / dataSenderController.BPM && gameTime<=3.7f * 60.0f / dataSenderController.BPM){
            float newAlpha = spriteRenderer.color.a + 3 * Time.deltaTime;
            spriteRenderer.color = new Color(
                spriteRenderer.color.r, 
                spriteRenderer.color.g, 
                spriteRenderer.color.b, 
                newAlpha
            );
        }else if(gameTime>=3.7f * 60.0f / dataSenderController.BPM){
            float newAlpha = spriteRenderer.color.a - 3 * Time.deltaTime;
            if(newAlpha<=0.001) Destroy(gameObject);
            else spriteRenderer.color = new Color(
                spriteRenderer.color.r, 
                spriteRenderer.color.g, 
                spriteRenderer.color.b, 
                newAlpha
            );
        }
    }
}
