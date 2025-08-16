using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPrefebController : MonoBehaviour
{
    public int type;
    public SpriteRenderer spriteRenderer;
    public int blowSpeed,fadeSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        if(type==1) transform.localEulerAngles = new Vector3(0, 0, -90); // 下
        if(type==2) transform.localEulerAngles = new Vector3(0, 0, +90); // 上
        if(type==3) transform.localEulerAngles = new Vector3(0, 0, +0); // 右
        if(type==4) transform.localEulerAngles = new Vector3(0, 0, +180); // 左
        if(type==5) Destroy(gameObject);
        if(type==9) transform.localEulerAngles = new Vector3(0, 0, -90); // 下
        if(type==10) transform.localEulerAngles = new Vector3(0, 0, +90); // 上
        if(type==11) transform.localEulerAngles = new Vector3(0, 0, +60); // 右上
        if(type==12) transform.localEulerAngles = new Vector3(0, 0, +240); // 左下
        if(type==13) transform.localEulerAngles = new Vector3(0, 0, -60); // 右下
        if(type==14) transform.localEulerAngles = new Vector3(0, 0, +120); // 左上
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
