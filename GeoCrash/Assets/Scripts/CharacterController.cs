using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Math 類別位於 System 命名空間

public class CharacterController : MonoBehaviour
{
    public GameObject perfectEffectorPrefeb;
    public GameObject goodEffectorPrefeb;
    public GameObject missEffectorPrefeb;
    public GameObject wallPrefeb;
    public SpriteRenderer spriteRenderer;
    public float velocity;
    public Vector3 dir;
    public int moveSpeed;
    public int BPM;

    struct Note{
        public float t;
        public int type;
    };
    struct Hold{
        public float t_begin,t_end;
        public int type;
    };
    Queue<Note> notes = new Queue<Note>();
    Queue<Hold> holds = new Queue<Hold>();
    Queue<Note> turns = new Queue<Note>();
    public float gameTime;
    // Start is called before the first frame update
    public float perfectLimit;
    public float goodLimit;
    public AudioSource audioSource;
    public bool isPlayingMusic;
    public bool autoPlay;
    bool canCatchHold;
    public float deviation; // 單位為毫秒

    void FillTheQueue(){
        //        2
        //    4       3 
        //        1
        AddNote(0, 0.0f, 2);
        AddNote(0, 1.5f, 1);
        AddNote(0, 3.0f, 2);
        AddNote(0, 3.5f, 1);
        AddNote(0, 4.0f, 2);
        AddNote(0, 5.5f, 1);
        AddNote(0, 7.0f, 2);
        AddNote(0, 7.5f, 1);
        AddNote(1, 0.0f, 2);
        AddNote(1, 1.5f, 1);
        AddNote(1, 3.0f, 2);
        AddNote(1, 3.5f, 1);
        AddNote(1, 6.0f, 2);
        AddNote(1, 6.5f, 1);
        AddNote(1, 7.0f, 2);
        AddNote(2, 0.0f, 1);
        AddNote(2, 1.5f, 2);
        AddNote(2, 3.0f, 1);
        AddNote(2, 4.0f, 2);
        AddNote(2, 5.0f, 1);
        AddNote(2, 5.5f, 2);
        AddNote(2, 6.5f, 1);
        AddNote(2, 7.5f, 2);
        AddNote(3, 0.0f, 1);
        AddNote(3, 2.0f, 2);
        AddNote(3, 3.0f, 1);
        AddNote(3, 4.0f, 2);
        AddNote(3, 6.0f, 1);
        AddNote(3, 6.5f, 2);
        AddNote(3, 7.0f, 1);
        AddNote(3, 7.5f, 3);

        AddNote(4, 0.0f, 4);
        AddNote(5, 0.0f, 3);
        AddNote(6, 0.0f, 2);
        AddNote(7, 0.0f, 1);
        AddNote(7, 2.0f, 2);
        AddNote(7, 4.0f, 1);
        AddNote(7, 4.5f, 2);
        AddNote(7, 5.0f, 1);
        AddNote(7, 5.5f, 2);
        AddNote(7, 6.0f, 1);
        AddNote(7, 6.5f, 2);
        AddNote(7, 7.0f, 1);
        AddNote(7, 7.5f, 2);

        // 前奏B
        AddNote(8, 0.0f, 1);
        AddNote(8, 1.0f, 2);
        AddHold(8, 2.0f, 3.0f, 1);
        AddNote(8, 3.5f, 1);
        AddNote(8, 4.5f, 2);
        AddNote(8, 5.0f, 1);
        AddNote(8, 5.5f, 2);
        AddNote(8, 6.0f, 1);
        AddNote(8, 6.5f, 2);
        AddNote(8, 7.0f, 1);
        AddNote(8, 7.5f, 2);

        AddNote(9, 0.0f, 1);
        AddNote(9, 1.0f, 2);
        AddHold(9, 2.0f, 3.0f, 1);
        AddNote(9, 3.5f, 1);
        AddNote(9, 4.5f, 2);
        AddNote(9, 5.0f, 1);
        AddNote(9, 5.5f, 2);
        AddNote(9, 6.0f, 1);
        AddNote(9, 6.5f, 2);
        AddNote(9, 7.0f, 1);
        AddNote(9, 7.5f, 2);

        AddNote(10, 0.0f, 1);
        AddNote(10, 1.0f, 2);
        AddHold(10, 2.0f, 3.0f, 1);
        AddNote(10, 3.5f, 1);
        AddNote(10, 4.5f, 2);
        AddNote(10, 5.0f, 1);
        AddNote(10, 5.5f, 2);
        AddNote(10, 6.0f, 1);
        AddNote(10, 6.5f, 2);
        AddNote(10, 7.0f, 1);
        AddNote(10, 7.5f, 4);

        AddNote(11, 0.0f, 3);
        AddNote(11, 0.5f, 4);
        AddNote(11, 1.5f, 3);
        AddNote(11, 2.0f, 4);
        AddNote(11, 3.0f, 3);
        AddNote(11, 3.5f, 4);
        AddNote(11, 4.5f, 3);
        AddNote(11, 5.0f, 4);
        AddHold(11, 6.0f, 7.0f, 1);

        // 主歌A
        AddNote(12, 0.0f, 3);
        AddNote(12, 1.0f, 2);
        AddNote(12, 3.0f, 1);
        AddNote(12, 4.5f, 4);
        AddNote(12, 5.0f, 3);
        AddNote(12, 5.5f, 2);
        AddNote(12, 6.0f, 1);
        AddNote(12, 7.0f, 4);

        AddNote(13, 0.0f, 3);
        AddNote(13, 1.0f, 2);
        AddNote(13, 3.0f, 1);
        AddNote(13, 4.5f, 4);
        AddNote(13, 5.0f, 3);
        AddNote(13, 5.5f, 2);
        AddNote(13, 6.0f, 1);
        AddNote(13, 7.0f, 4);

        AddNote(14, 0.0f, 3);
        AddNote(14, 1.0f, 2);
        AddNote(14, 3.0f, 1);
        AddNote(14, 4.5f, 4);
        AddNote(14, 5.0f, 3);
        AddNote(14, 5.5f, 2);
        AddNote(14, 6.0f, 1);
        AddNote(14, 7.0f, 4);

        AddNote(15, 0.0f, 3);
        AddNote(15, 3.0f, 2);
        AddNote(15, 4.0f, 1);
        AddNote(15, 6.0f, 2);

        //主歌A-2
        AddNote(16, 0.0f, 4);
        AddNote(16, 1.0f, 3);
        AddNote(16, 3.0f, 1);
        AddNote(16, 4.5f, 4);
        AddNote(16, 5.0f, 3);
        AddNote(16, 5.5f, 2);
        AddNote(16, 6.0f, 1);
        AddNote(16, 7.0f, 2);

        AddNote(17, 0.0f, 4);
        AddNote(17, 1.0f, 3);
        AddNote(17, 4.0f, 1);
        AddNote(17, 5.0f, 2);
        AddNote(17, 6.0f, 1);
        AddNote(17, 7.0f, 2);

        AddNote(18, 0.0f, 4);
        AddNote(18, 1.5f, 3);
        AddNote(18, 3.0f, 1);
        AddNote(18, 4.5f, 4);
        AddNote(18, 5.0f, 3);
        AddNote(18, 5.5f, 2);
        AddNote(18, 6.0f, 1);
        AddNote(18, 7.0f, 2);

        AddNote(19, 0.0f, 1);

        // 主歌B

        AddNote(0, 4000f, 1);
        AddHold(0, 4000.0f, 5000.5f, 1);
    }

    void Start()
    {
        gameTime = 0.00000f-8*60/BPM;
        dir = new Vector3(1, 1, 0);
        isPlayingMusic = false;
        
        FillTheQueue();
        
        // for(int i=0;i<1000;i++){
        //     Note tmp = new Note();
        //     tmp.t = i*60.0f/BPM;
        //     tmp.type = i%4+1;
        //     notes.Enqueue(tmp);
        //     turns.Enqueue(tmp);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime; // 讓時間流動

        transform.position += dir * moveSpeed * Time.deltaTime * ((gameTime>=-4*60/BPM)?1:0); // 移動

        if(gameTime >= deviation*0.001 && !isPlayingMusic){
            audioSource.Play();
            isPlayingMusic = true;
        }
        // if(Input.anyKeyDown){
        //     GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity); // copy a new prefeb
        // }

        // if(notes.Count > 0) if(gameTime >= notes.Peek().t){
        //     notes.Dequeue();
        //     GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity);
        // }

    /*  wall maker function
        if(gameTime >= turns.Peek().t){
            turns.Dequeue();
            SwitchDirction(turns.Peek().type);
            if(turns.Peek().type == 1 || Input.GetKeyDown(KeyCode.A)){ //down
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position-new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 2 || Input.GetKeyDown(KeyCode.S)){ //up
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 3 || Input.GetKeyDown(KeyCode.D)){ //right
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
            if(turns.Peek().type == 4 || Input.GetKeyDown(KeyCode.F)){ //left
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(-0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
        }
        */
        if(autoPlay){ // 自動演奏
            if(gameTime>=notes.Peek().t){
                GameObject newEffector = Instantiate(perfectEffectorPrefeb, transform.position, Quaternion.identity);
                notes.Dequeue();
            }
        }


        if(Input.anyKeyDown || !autoPlay){   // 打擊判定
            if( Math.Abs(notes.Peek().t - gameTime) <= perfectLimit ){
                GameObject newEffector = Instantiate(perfectEffectorPrefeb, transform.position, Quaternion.identity);
                notes.Dequeue();
            }else if( Math.Abs(notes.Peek().t - gameTime) <= goodLimit){
                GameObject newEffector = Instantiate(goodEffectorPrefeb, transform.position, Quaternion.identity);
                notes.Dequeue();
            }
        }
        if(gameTime > notes.Peek().t + goodLimit){
            GameObject newEffector = Instantiate(missEffectorPrefeb, transform.position, Quaternion.identity);
            notes.Dequeue();
        }

        if(gameTime < holds.Peek().t_begin){ // 長條判定
            canCatchHold = true;
        }
        else if(gameTime >= holds.Peek().t_begin+goodLimit && gameTime < holds.Peek().t_end-goodLimit){
            if( (!Input.anyKey || !canCatchHold) && !autoPlay ){
                canCatchHold = false;
                GameObject newEffector = Instantiate(missEffectorPrefeb, transform.position, Quaternion.identity);
                newEffector.GetComponent<EffectorController>().blowSpeed = 2;
                newEffector.GetComponent<EffectorController>().fadeSpeed = 2;
            }else{
                GameObject newEffector = Instantiate(perfectEffectorPrefeb, transform.position, Quaternion.identity);
                newEffector.GetComponent<EffectorController>().blowSpeed = 2;
                newEffector.GetComponent<EffectorController>().fadeSpeed = 2;
            }
        }else if(gameTime >= holds.Peek().t_end){
            holds.Dequeue();
        }
        
        if(gameTime >= turns.Peek().t){  // 碰撞轉向與製造牆壁
            SwitchDirction(turns.Peek().type); // 轉向
            turns.Dequeue();
            /*  // 造牆
            if(turns.Peek().type == 1){ //down
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position-new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 2){ //up
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 3){ //right
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
            if(turns.Peek().type == 4){ //left
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(-0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
            */
        }
    }

    void SwitchDirction(int dirIndex){  //轉向函式
        if(dirIndex == 1){ //down
            if(dir.y==1) Debug.Log("ERROR");
            dir.y *= -1;
        }else if(dirIndex == 2){ //up
            if(dir.y==-1) Debug.Log("ERROR");
            dir.y *= -1;
        }else if(dirIndex == 3){ //right
            if(dir.x==-1) Debug.Log("ERROR");
            dir.x *= -1;
        }else if(dirIndex == 4){ //left
            if(dir.x==1) Debug.Log("ERROR");
            dir.x *= -1;
        }
        
    }

    void AddNote(int p,float t,int type){  // 加入音符函式
        Note tmp = new Note();
        tmp.t = (t+p*8)*60.0f/BPM;
        tmp.type = type;
        notes.Enqueue(tmp);
        turns.Enqueue(tmp);
    }

    void AddHold(int p,float t_b,float t_e,int type){  // 加入長條函式
        Hold tmp = new Hold();
        tmp.t_begin = (t_b+p*8)*60.0f/BPM;
        tmp.t_end = (t_e+p*8)*60.0f/BPM;
        tmp.type = type;
        holds.Enqueue(tmp);
    }
}
