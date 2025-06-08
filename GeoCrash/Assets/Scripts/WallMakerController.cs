using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Math 類別位於 System 命名空間

public class WallMakerController : MonoBehaviour
{
    public GameObject wallPrefeb;
    public SpriteRenderer spriteRenderer;
    public float velocity;
    public Vector3 dir;
    public int moveSpeed;
    public int BPM;
    public GameObject holdPrefeb;

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

    void Start()
    {
        gameTime = 0.00000f-4*60/BPM;
        dir = new Vector3(1, 1, 0);
//            2
//        4       3
//            1
        AddNote(0, 0.0f, 1);
        AddNote(0, 1.5f, 2);
        AddNote(0, 3.0f, 1);
        AddNote(0, 3.5f, 2);
        AddNote(0, 4.0f, 1);
        AddNote(0, 5.5f, 2);
        AddNote(0, 7.0f, 1);
        AddNote(0, 7.5f, 2);
        AddNote(1, 0.0f, 1);
        AddNote(1, 1.5f, 2);
        AddNote(1, 3.0f, 1);
        AddNote(1, 3.5f, 2);
        AddNote(1, 6.0f, 1);
        AddNote(1, 6.5f, 2);
        AddNote(1, 7.0f, 1);
        AddNote(2, 0.0f, 2);
        AddNote(2, 1.5f, 1);
        AddNote(2, 3.0f, 2);
        AddNote(2, 4.0f, 1);
        AddNote(2, 5.0f, 2);
        AddNote(2, 5.5f, 1);
        AddNote(2, 6.5f, 2);
        AddNote(2, 7.5f, 1);
        AddNote(3, 0.0f, 2);
        AddNote(3, 2.0f, 1);
        AddNote(3, 3.0f, 2);
        AddNote(3, 4.0f, 1);
        AddNote(3, 6.0f, 2);
        AddNote(3, 6.5f, 1);
        AddNote(3, 7.0f, 2);
        AddNote(3, 7.5f, 1);

        AddNote(4, 0.0f, 3);
        AddNote(5, 0.0f, 4);
        AddNote(6, 0.0f, 3);
        AddNote(7, 0.0f, 2);
        AddNote(7, 2.0f, 1);
        AddNote(7, 4.0f, 2);
        AddNote(7, 4.5f, 1);
        AddNote(7, 5.0f, 2);
        AddNote(7, 5.5f, 1);
        AddNote(7, 6.0f, 2);
        AddNote(7, 6.5f, 1);
        AddNote(7, 7.0f, 2);
        AddNote(7, 7.5f, 1);

        // 前奏B
        AddNote(8, 0.0f, 2);
        AddNote(8, 1.0f, 1);
        AddHold(8, 2.0f, 3.0f, 1);
        AddNote(8, 3.5f, 2);
        AddNote(8, 4.5f, 1);
        AddNote(8, 5.0f, 2);
        AddNote(8, 5.5f, 1);
        AddNote(8, 6.0f, 2);
        AddNote(8, 6.5f, 1);
        AddNote(8, 7.0f, 2);
        AddNote(8, 7.5f, 1);

        AddNote(9, 0.0f, 2);
        AddNote(9, 1.0f, 1);
        AddHold(9, 2.0f, 3.0f, 1);
        AddNote(9, 3.5f, 2);
        AddNote(9, 4.5f, 1);
        AddNote(9, 5.0f, 2);
        AddNote(9, 5.5f, 1);
        AddNote(9, 6.0f, 2);
        AddNote(9, 6.5f, 1);
        AddNote(9, 7.0f, 2);
        AddNote(9, 7.5f, 1);

        AddNote(10, 0.0f, 2);
        AddNote(10, 1.0f, 1);
        AddHold(10, 2.0f, 3.0f, 1);
        AddNote(10, 3.5f, 2);
        AddNote(10, 4.5f, 1);
        AddNote(10, 5.0f, 2);
        AddNote(10, 5.5f, 1);
        AddNote(10, 6.0f, 2);
        AddNote(10, 6.5f, 1);
        AddNote(10, 7.0f, 2);
        AddNote(10, 7.5f, 1);

        AddNote(11, 0.0f, 4);
        AddNote(11, 0.5f, 3);
        AddNote(11, 1.5f, 4);
        AddNote(11, 2.0f, 3);
        AddNote(11, 3.0f, 4);
        AddNote(11, 3.5f, 3);
        AddNote(11, 4.5f, 4);
        AddNote(11, 5.0f, 3);
        AddHold(11, 6.0f, 7.0f, 1);

        AddNote(0, 4000f, 1);
        AddHold(0, 4000.0f, 5000.5f, 1);
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
        gameTime += Time.deltaTime;

        transform.position += dir * moveSpeed * Time.deltaTime * ((gameTime>=-4*60/BPM)?1:0);
        
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


        if(gameTime >= holds.Peek().t_begin){
            GameObject newHoldPrefeb = Instantiate(
                holdPrefeb, 
                transform.position, 
                Quaternion.identity
            );
        }
        if(gameTime >= holds.Peek().t_end){
            holds.Dequeue();
        }
    }

    void SwitchDirction(int dirIndex){
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

    void AddNote(int p,float t,int type){
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

