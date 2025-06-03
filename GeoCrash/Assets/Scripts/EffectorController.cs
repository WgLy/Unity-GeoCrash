using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorController : MonoBehaviour
{
    public int blowSpeed,fadeSpeed;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        spriteRenderer.color = new Color(
            spriteRenderer.color.r, 
            spriteRenderer.color.g, 
            spriteRenderer.color.b, 1.0f
        );
        // Debug.Log("Create successfully");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(
            Time.deltaTime*blowSpeed, 
            Time.deltaTime*blowSpeed, 
            Time.deltaTime*blowSpeed
        );

        float newAlpha = spriteRenderer.color.a - fadeSpeed * Time.deltaTime;
        if(newAlpha<=0.001) Destroy(gameObject);
        else spriteRenderer.color = new Color(
            spriteRenderer.color.r, 
            spriteRenderer.color.g, 
            spriteRenderer.color.b, 
            newAlpha
        );
        
    }
}
