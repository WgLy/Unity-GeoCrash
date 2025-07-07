using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite squareSprite;
    public Sprite triangleSprite;
    public Sprite hexagonSprite;
    public Sprite capsuleSprite;
    public Sprite silcedSprite;
    public Sprite hexagon2Sprite;
    public int shape;
    // Start is called before the first frame update
    void Start()
    {
        shape = Random.Range(1, 7);
        if(shape == 1){
            spriteRenderer.sprite = squareSprite;
        }else if(shape == 2){
            spriteRenderer.sprite = triangleSprite;
        }else if(shape == 3){
            spriteRenderer.sprite = hexagonSprite;
        }else if(shape == 4){
            spriteRenderer.sprite = capsuleSprite;
        }else if(shape == 5){
            spriteRenderer.sprite = silcedSprite;
        }else if(shape == 6){
            spriteRenderer.sprite = hexagon2Sprite;
        }
        spriteRenderer.color = new Color(
            spriteRenderer.color.r, 
            spriteRenderer.color.g, 
            spriteRenderer.color.b, Random.Range(0.1f, 0.5f)
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
