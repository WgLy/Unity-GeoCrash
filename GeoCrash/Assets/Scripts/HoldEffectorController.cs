using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldEffectorController : MonoBehaviour
{
    public int blowSpeed,fadeSpeed;
    public SpriteRenderer spriteRenderer;
    public bool blowing;
    int blowWay;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        spriteRenderer.color = new Color(
            spriteRenderer.color.r, 
            spriteRenderer.color.g, 
            spriteRenderer.color.b, 1.0f
        );
        blowing = false;
        // Debug.Log("Create successfully");
        blowWay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(blowing){
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
        }else{
            if(transform.localScale.x < 1.3f){
                blowWay = 1;
            }
            if(transform.localScale.x > 1.7f){
                blowWay = -1;
            }
            transform.localScale += new Vector3(
                Time.deltaTime*blowSpeed*blowWay, 
                Time.deltaTime*blowSpeed*blowWay, 
                Time.deltaTime*blowSpeed*blowWay
            );
        }
    }
}
