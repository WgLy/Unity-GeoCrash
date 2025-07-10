using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 載入其他場景
using System; // Math 類別位於 System 命名空間

public class ChooserController : MonoBehaviour
{
    public DataSenderController dataSenderController; 
    public FadingController fadingController;
    public MapGeneratorController mapGeneratorController;
    Vector3 idealPosition = new Vector3();
    public float slideSpeed;
    float holdingTime_W;
    float holdingTime_S;
    public float gap;
    public bool isChoosingDifficulty;
    
    // Start is called before the first frame update
    void Start()
    {
        dataSenderController = FindObjectOfType<DataSenderController>();
        idealPosition = new Vector3(0, 0, 0);
        fadingController.Fade(true, "");
        isChoosingDifficulty = false;
        dataSenderController.songIndex = -1;
        dataSenderController.difficulty = -1;
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
        if(idealPosition.y <= -1*mapGeneratorController.sizeOfSongList*gap){ // 循環選譜bottom
            idealPosition = new Vector3(idealPosition.x, 0, idealPosition.z);
        }
        if(idealPosition.y >= gap){ // 循環選譜top
            idealPosition = new Vector3(idealPosition.x, -1*(mapGeneratorController.sizeOfSongList-1)*gap, idealPosition.z);
        }

        if(transform.position.y > idealPosition.y+0.05f){  // 滑動效果y
            transform.position -= new Vector3(
                0, 
                Math.Max( slideSpeed, Math.Abs(transform.position.y-idealPosition.y)*10 ) * Time.deltaTime, 
                0
            );
        }else if(transform.position.y < idealPosition.y-0.05f){
            transform.position += new Vector3(
                0, 
                Math.Max( slideSpeed, Math.Abs(idealPosition.y-transform.position.y)*10 ) * Time.deltaTime, 
                0
            );
        }else{
            transform.position = new Vector3(transform.position.x, idealPosition.y, transform.position.z);
        }

        if(transform.position.y == idealPosition.y && transform.position.x > idealPosition.x+0.005f){  // 滑動效果x
            transform.position -= new Vector3(
                Math.Max( slideSpeed, Math.Abs(transform.position.x-idealPosition.x)*20 ) * Time.deltaTime, 
                0, 
                0
            );
        }else if(transform.position.y == idealPosition.y && transform.position.x < idealPosition.x-0.005f){
            transform.position += new Vector3(
                Math.Max( slideSpeed, Math.Abs(idealPosition.y-transform.position.y)*20 ) * Time.deltaTime, 
                0, 
                0
            );
        }else if(transform.position.y == idealPosition.y){
            transform.position = new Vector3(transform.position.x, idealPosition.y, transform.position.z);
        }


        if (Input.GetKeyDown(KeyCode.Return)){ // 下一個
            dataSenderController.songIndex = (int)(idealPosition.y / -1.5f) ;
            dataSenderController.difficulty = (int)(idealPosition.x / 2.0f) ; 
            dataSenderController.FillQFunction();
            fadingController.Fade(false, "PlayScene");
        }

        if(Input.GetKeyDown(KeyCode.A) && idealPosition.x != 0){
            idealPosition -= new Vector3(2, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.D) && idealPosition.x != 4){
            idealPosition += new Vector3(2, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            fadingController.Fade(false, "SettingScene");
        }
    }
}
