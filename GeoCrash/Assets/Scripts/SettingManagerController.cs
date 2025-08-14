using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManagerController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    public SpriteRenderer[] squares = new SpriteRenderer[6];
    public FadingController fadingController;
    public bool initialFade;
    /*
    1 2
    3 4
    5 6
    */
    public int nowPosIndex;
    public Vector3[] pos = {
        new Vector3(-7, 2, 0),
        new Vector3(-5, 2, 0),
        new Vector3(-7, -1, 0),
        new Vector3(-5, -1, 0),
        new Vector3(-7, -4, 0),
        new Vector3(-5, -4, 0)
    };

    public int[,] movingChart = {
      //{W, A, S, D}
        {-1, -1, 3, 2},
        {-1, 1, 4, -1},
        {1, -1, 5, 4},
        {2, 3, 6, -1},
        {3, -1, -1, 6},
        {4, 5, -1, -1}
    };
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pos[nowPosIndex];
        initialFade = false;
    }

    // Update is called once per frame
    void Update()
    {
        dataSenderController = FindObjectOfType<DataSenderController>();

        if(!initialFade){
            fadingController.Fade(true, "");
            initialFade = true;
        }

        transform.position = pos[nowPosIndex-1]; // 移動
        if(Input.GetKeyDown(KeyCode.W)){
            if(movingChart[nowPosIndex-1, 0] != -1) nowPosIndex = movingChart[nowPosIndex-1, 0];
        }
        if(Input.GetKeyDown(KeyCode.A)){
            if(movingChart[nowPosIndex-1, 1] != -1) nowPosIndex = movingChart[nowPosIndex-1, 1];
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(movingChart[nowPosIndex-1, 2] != -1) nowPosIndex = movingChart[nowPosIndex-1, 2];
        }
        if(Input.GetKeyDown(KeyCode.D)){
            if(movingChart[nowPosIndex-1, 3] != -1) nowPosIndex = movingChart[nowPosIndex-1, 3];
        }
        
        if(Input.GetKeyDown(KeyCode.Return)){  // 選取
            dataSenderController.SettingIsOn[nowPosIndex-1] = true;
            dataSenderController.SettingIsOn[(nowPosIndex-1)/2*2+(nowPosIndex%2)] = false;
            }

        for(int i=0;i<6;i++){
            squares[i].color = new Color(
                squares[i].color.r,
                squares[i].color.g,
                squares[i].color.b,
                (dataSenderController.SettingIsOn[i] == true)? 1:0
            );
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            fadingController.Fade(false, "MainScene");
        }
        
    }
}
