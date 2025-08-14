using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Math 類別位於 System 命名空間

public class CharacterController : MonoBehaviour
{
    public GameObject perfectEffectorPrefeb;
    public GameObject goodEffectorPrefeb;
    public GameObject missEffectorPrefeb;
    public GameObject holdEffectorPrefeb;
    public GameObject currentHoldEffector;
    public GameObject wallPrefeb;
    public FadingController fadingController;
    public SpriteRenderer spriteRenderer;
    public Vector2 dir;
    public float moveSpeed;
    public int BPM;
    private Rigidbody2D rb;
    public WallMakerController wallMakerController;
    public DataSenderController dataSenderController;

    //分數計算
    public float score;
    public float max_score;
    public int perfect_num;
    public int good_num;
    public int miss_num;

    Queue<Note> notes = new Queue<Note>();
    Queue<Hold> holds = new Queue<Hold>();
    Queue<Note> turns = new Queue<Note>();
    Queue<Effect> effects = new Queue<Effect>();
    public float gameTime;

    public float perfectLimit;
    public float goodLimit;
    public AudioController audioController;
    public bool isPlayingMusic;
    public bool autoPlay;
    bool canCatchHold;
    public float deviation; // 單位為毫秒
    private bool moving;
    public int shape;

    
    // 改變形狀
    public Sprite squareSprite;
    public Sprite triangleSprite;
    public Sprite hexagonSprite;
    public BoxCollider2D squareCollider;
    public PolygonCollider2D triangleCollider;
    public PolygonCollider2D hexagonCollider;

    // 結算畫面
    public EndUIController endUIController;
    public ScoreController scoreController;

    // 特效使用
    public CameraController cameraController;
    public BgColorController bgColorController;

    // 暫停保存狀態
    public MovementStatus stopStatus;
    public bool stopping;
    public float stoppingTime;

    // wall shine
    public Color origionColor;

    // 暫停UI
    public StopUIController stopUIController_continuer;
    public StopUIController stopUIController_replaier;
    public StopUIController stopUIController_quiter;
    public StopUIController stopUIController_banner;
    public bool activeEscape;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dataSenderController = FindObjectOfType<DataSenderController>();
        notes = new Queue<Note>(dataSenderController.notes);
        holds = new Queue<Hold>(dataSenderController.holds);
        turns = new Queue<Note>(dataSenderController.notes);
        effects = new Queue<Effect>(dataSenderController.effects);
        BPM = dataSenderController.BPM;
        autoPlay = dataSenderController.autoPlay;
        shape = dataSenderController.initialShape;
        // 初始校正
        dir = dataSenderController.InitialStatus.dir;
        transform.position = dataSenderController.InitialStatus.locate;
        transform.rotation = dataSenderController.InitialStatus.angle;
        rb.angularVelocity = dataSenderController.InitialStatus.spin;
        rb.velocity = dataSenderController.InitialStatus.dir;
        moveSpeed = dataSenderController.InitialStatus.speed;
        // 初始變形
        ChangeShape(shape);

        // 設定分數
        score = 0;
        max_score = notes.Count+holds.Count-2;

        gameTime = 0.00000f-8*60.0f/BPM; //8
        
        isPlayingMusic = false;
        moving = false;
        
        // 暫停保存狀態
        stopStatus.dir = dir;
        stopStatus.locate = transform.position;
        stopStatus.angle = transform.rotation;
        stopStatus.spin = rb.angularVelocity;
        stopStatus.dir = rb.velocity;
        stopStatus.speed = moveSpeed;
        stoppingTime = 0;
        
        // 暫停UI
        activeEscape = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!stopping) gameTime += Time.fixedDeltaTime * dataSenderController.timeDegree; // 讓時間流動
        stoppingTime += Time.fixedDeltaTime;

        if (gameTime >= -4 * 60.0f / BPM && moving == false){
            transform.position = new Vector3(0, 0, 0); 
            rb.velocity = dir.normalized * moveSpeed;
            moving = true;
            fadingController.Fade(true, "");
        }else if(moving == false){
            rb.velocity = Vector2.zero; // 如果還沒到開始移動的時間，則速度為零
        }//transform.position += dir * moveSpeed * Time.fixedDeltaTime * ((gameTime>=-4*60/BPM)?1:0); // 移動


        if(gameTime >= deviation*0.001f+dataSenderController.songDeviation*0.001f && !isPlayingMusic){ // 播放音樂
            audioController.StartMusic();
            isPlayingMusic = true;
        }

        if(autoPlay){ // 自動演奏
            if(gameTime>=notes.Peek().t){
                GameObject newEffector = Instantiate(perfectEffectorPrefeb, transform.position, Quaternion.identity);
                notes.Dequeue();
                score += 1;
                audioController.PlayTapSound();
            }
        }

        if(gameTime>=turns.Peek().t){ // 校正位置 + change wall shine
            transform.position = wallMakerController.correction.Peek().locate;
            transform.rotation = wallMakerController.correction.Peek().angle;
            rb.angularVelocity = wallMakerController.correction.Peek().spin;
            rb.velocity = wallMakerController.correction.Peek().dir;
            turns.Dequeue();
            wallMakerController.correction.Dequeue();
            if(dataSenderController.isWallShine){
                wallMakerController.shineWall.Peek().GetComponent<SpriteRenderer>().color = origionColor;
                wallMakerController.shineWall.Dequeue();
            }
            

        }

/*
        if(gameTime>=effects.Peek().t){ // 播放特效
            if(effects.Peek().type == 1){

            }
            effects.Dequeue();
        }
*/
        if(Input.anyKeyDown && !autoPlay){   // 打擊判定
            if( Math.Abs(notes.Peek().t - gameTime) <= perfectLimit ){
                GameObject newEffector = Instantiate(perfectEffectorPrefeb, transform.position, Quaternion.identity);
                notes.Dequeue();
                score += 1;
                perfect_num++;
                audioController.PlayTapSound();
            }
            else if( Math.Abs(notes.Peek().t - gameTime) <= goodLimit){
                GameObject newEffector = Instantiate(goodEffectorPrefeb, transform.position, Quaternion.identity);
                notes.Dequeue();
                score += 0.5f;
                good_num++;
                audioController.PlayTapSound();
            }
        }
        if(gameTime > notes.Peek().t + goodLimit){
            GameObject newEffector = Instantiate(missEffectorPrefeb, transform.position, Quaternion.identity);
            miss_num++;
            notes.Dequeue();
        }

        if(gameTime >= holds.Peek().t_begin){ // 創造長條特效
            if(currentHoldEffector == null){
                currentHoldEffector = Instantiate(holdEffectorPrefeb, transform.position, Quaternion.identity);
            } 
            else currentHoldEffector.transform.position = transform.position;
        }

        if(gameTime < holds.Peek().t_begin){ // 長條判定
            canCatchHold = true;
        }
        else if(gameTime >= holds.Peek().t_begin && gameTime < holds.Peek().t_end){
            
            if( (!Input.anyKey || !canCatchHold) && !autoPlay ){
                canCatchHold = false;
                currentHoldEffector.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.8f);
            }else{
                currentHoldEffector.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 0.8f);
            }
        }
        if(gameTime >= holds.Peek().t_end){
            currentHoldEffector.GetComponent<HoldEffectorController>().blowing = true;
            currentHoldEffector = null;
            if(canCatchHold == true) score += 1;
            holds.Dequeue();
        }

        if(Input.GetKeyDown(KeyCode.Keypad1)){
            ChangeShape(1);
        }
        if(Input.GetKeyDown(KeyCode.Keypad2)){
            ChangeShape(2);
        }
        if(Input.GetKeyDown(KeyCode.Keypad3)){
            ChangeShape(3);
        }

        if(notes.Peek().type == 0 && holds.Peek().type == 0 && effects.Peek().type == 0){ // 遊玩結束
            Debug.Log("end");
            fadingController.Fade(false, "EndScene");
            audioController.StopAllSound();
            dataSenderController.finalScore = int.Parse(scoreController.myText.text);
            dataSenderController.perfect_num = perfect_num;
            dataSenderController.good_num = good_num;
            dataSenderController.miss_num = miss_num;
        }

        if(gameTime >= effects.Peek().t){ // 播放特效
            if(effects.Peek().type == 1){ // 1震動
                cameraController.ShakeCamera(effects.Peek().degree, effects.Peek().duriation);
            }
            if(effects.Peek().type == 2){ // 2變形
                ChangeShape((int)effects.Peek().degree);
            }
            if(effects.Peek().type == 3){ // 3縮放
                cameraController.ChangeCameraScale((int)effects.Peek().degree, (int)effects.Peek().speed);
            }
            if(effects.Peek().type == 4){ // 4變色
                bgColorController.ChangeColor(effects.Peek().degree, effects.Peek().duriation, effects.Peek().speed);
            }
            if(effects.Peek().type == 5){ // 5傾斜
                cameraController.TiltCamera(effects.Peek().degree, effects.Peek().speed, effects.Peek().duriation);
            }
            effects.Dequeue();
        }

        if(Input.GetKeyDown(KeyCode.Escape) || activeEscape){ // 暫停與啟動
            if(stopping == false && stoppingTime >= 0.1f){
                stopping = true;
                stopStatus.dir = dir;
                stopStatus.locate = transform.position;
                stopStatus.angle = transform.rotation;
                stopStatus.spin = rb.angularVelocity;
                stopStatus.dir = rb.velocity;
                stopStatus.speed = moveSpeed;
                rb.angularVelocity = 0.0f;
                rb.velocity = new Vector2(0, 0);
                stoppingTime = 0;
                stopUIController_banner.Show();
                stopUIController_continuer.Show();
                stopUIController_replaier.Show();
                stopUIController_quiter.Show();
                activeEscape = false;
            }else if(stopping == true && stoppingTime >= 0.1f){
                stopping = false;
                dir = stopStatus.dir;
                transform.position = stopStatus.locate;
                transform.rotation = stopStatus.angle;
                rb.angularVelocity = stopStatus.spin;
                rb.velocity = stopStatus.dir;
                moveSpeed = stopStatus.speed;
                stoppingTime = 0;
                stopUIController_banner.UnShow();
                stopUIController_continuer.UnShow();
                stopUIController_replaier.UnShow();
                stopUIController_quiter.UnShow();
                activeEscape = false;
            }
        }

        if(stopping==true && Input.GetKeyDown(KeyCode.Return)){
            if(stopUIController_banner.currentIndex == 0){
                fadingController.Fade(false, "MainScene");
            }
            if(stopUIController_banner.currentIndex == 1){
                fadingController.Fade(false, "PlayScene");
            }
            if(stopUIController_banner.currentIndex == 2){
                activeEscape = true;
            }
        }

        if(wallMakerController.shineWall.Count > 0 && wallMakerController.shineWall.Peek().GetComponent<SpriteRenderer>().color != new Color(1, 1, 0, 1) && dataSenderController.isWallShine){
            origionColor = wallMakerController.shineWall.Peek().GetComponent<SpriteRenderer>().color;
            wallMakerController.shineWall.Peek().GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
        }

    }

    public void ChangeShape(int targetShape){
        squareCollider.enabled = false;
        triangleCollider.enabled = false;
        hexagonCollider.enabled = false;
        if(targetShape == 1){
            spriteRenderer.sprite = squareSprite;
            squareCollider.enabled = true;
        }
        if(targetShape == 2){
            spriteRenderer.sprite = triangleSprite;
            triangleCollider.enabled = true;
        }
        if(targetShape == 3){
            spriteRenderer.sprite = hexagonSprite;
            hexagonCollider.enabled = true;
        }
        shape = targetShape;
    }
}
