using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgColorController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Color idealColor;
    bool isChanging;
    public float colorChangeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = new Color(1 ,1 ,1 ,0.07f);
        idealColor = new Color(1 ,1 ,1 ,0.07f);
        isChanging = false;
        colorChangeSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isChanging){
            spriteRenderer.color = new Color(
                Mathf.MoveTowards(
                    spriteRenderer.color.r,
                    idealColor.r,
                    Time.deltaTime * colorChangeSpeed
                ),
                Mathf.MoveTowards(
                    spriteRenderer.color.g,
                    idealColor.g,
                    Time.deltaTime * colorChangeSpeed
                ),
                Mathf.MoveTowards(
                    spriteRenderer.color.b,
                    idealColor.b,
                    Time.deltaTime * colorChangeSpeed
                ),
                0.07f
            );
            if (Mathf.Approximately(spriteRenderer.color.r, idealColor.r) &&
                Mathf.Approximately(spriteRenderer.color.g, idealColor.g) &&
                Mathf.Approximately(spriteRenderer.color.b, idealColor.b) )
            {
                spriteRenderer.color = idealColor;
                isChanging = false;
            }
        }
    }

    public void ChangeColor(float r, float g, float b){
        idealColor = new Color(r ,g ,b ,0.07f);
        isChanging = true;
    }
}
