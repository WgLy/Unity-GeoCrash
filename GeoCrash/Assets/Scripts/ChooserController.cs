using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Math 類別位於 System 命名空間

public class ChooserController : MonoBehaviour
{
    Vector3 idealPosition = new Vector3();
    public float slideSpeed;
    float holdingTime_W;
    float holdingTime_S;
    public float gap;
    
    // Start is called before the first frame update
    void Start()
    {
        idealPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){ // 紀錄W長按時間
            holdingTime_W += Time.deltaTime;  
        }else{
            holdingTime_W = 0; 
        }
        if(Input.GetKey(KeyCode.S)){ // 紀錄S長按時間 
            holdingTime_S += Time.deltaTime; 
        }else{
            holdingTime_S = 0; 
        }
        if(holdingTime_W >= 0.5f){ // 長按W快速上升
            idealPosition += new Vector3(0, gap, 0);
            holdingTime_W -= 0.05f;
        }
        if(holdingTime_S >= 0.5f){ // 長按S快速下降
            idealPosition += new Vector3(0, -1*gap, 0);
            holdingTime_S -= 0.05f;
        }


        if(Input.GetKeyDown(KeyCode.W)){ // 依據輸入改變理想位置
            idealPosition += new Vector3(0, gap, 0);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            idealPosition += new Vector3(0, -1*gap, 0);
        }

        if(transform.position.y > idealPosition.y+0.005f){  // 滑動效果
            transform.position -= new Vector3(
                0, 
                Math.Max( slideSpeed, Math.Abs(transform.position.y-idealPosition.y)*10 ) * Time.deltaTime, 
                0
            );
        }else if(transform.position.y < idealPosition.y-0.005f){
            transform.position += new Vector3(
                0, 
                Math.Max( slideSpeed, Math.Abs(idealPosition.y-transform.position.y)*10 ) * Time.deltaTime, 
                0
            );
        }else{
            transform.position = idealPosition;
        }
    }
}
