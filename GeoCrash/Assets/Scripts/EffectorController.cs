using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorController : MonoBehaviour
{
    public int blowSpeed,fadeSpeed;
    public SpriteRenderer spriteRenderer;
    public CharacterController characterController;

    // 改變形狀
    public Sprite squareSprite;
    public Sprite triangleSprite;
    public Sprite hexagonSprite;
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
        characterController = FindObjectOfType<CharacterController>();
        ChangeShape(characterController.shape);
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

    public void ChangeShape(int targetShape){
        if(targetShape == 1){
            spriteRenderer.sprite = squareSprite;
        }
        if(targetShape == 2){
            spriteRenderer.sprite = triangleSprite;
        }
        if(targetShape == 3){
            spriteRenderer.sprite = hexagonSprite;
        }
    }
}
