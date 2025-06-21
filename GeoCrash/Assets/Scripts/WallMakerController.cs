using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Math 類別位於 System 命名空間

public class WallMakerController : MonoBehaviour
{
    public GameObject wallPrefeb;
    public GameObject tapPrefeb;
    public SpriteRenderer spriteRenderer;
    public Vector2 dir;
    public float moveSpeed;
    public int BPM;
    public GameObject holdPrefeb;
    private Rigidbody2D rb;
    private bool moving;
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
    public Queue<Vector3> locate = new Queue<Vector3>();
    public Queue<Quaternion> angle = new Queue<Quaternion>();
    public Queue<float> spin = new Queue<float>();
    public Queue<Vector2> speed = new Queue<Vector2>();
    public float gameTime;

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
        AddNote(19, 6.0f, 2);
        AddNote(19, 7.0f, 1);
        AddHold(20, 0.0f, 6.0f, 1);
        AddNote(20, 0.0f, 2);
        AddNote(20, 2.0f, 1);
        AddNote(20, 4.0f, 2);
        AddNote(20, 6.0f, 1);
        AddHold(21, 0.0f, 6.0f, 1);
        AddNote(21, 4.0f, 5);
        AddNote(21, 6.0f, 5);
        AddHold(22, 0.0f, 6.0f, 1);
        AddNote(22, 0.0f, 2);
        AddNote(22, 2.0f, 1);
        AddNote(22, 4.0f, 2);
        AddNote(22, 6.0f, 1);
        AddHold(23, 0.0f, 6.0f, 1);
        AddNote(23, 4.0f, 5);
        AddNote(23, 6.0f, 5);

        //主歌B-2
        AddNote(23, 7.0f, 5);
        AddNote(24, 0.0f, 2);
        AddNote(24, 1.0f, 5);
        AddNote(24, 1.5f, 1);
        AddNote(24, 3.0f, 5);
        AddNote(24, 4.0f, 2);
        AddNote(24, 5.0f, 5);
        AddNote(24, 5.5f, 1);
        AddNote(24, 7.0f, 5);
        AddNote(25, 0.0f, 2);
        AddNote(25, 1.0f, 5);
        AddNote(25, 1.5f, 1);
        AddNote(25, 3.0f, 5);
        AddNote(25, 4.0f, 2);
        AddNote(25, 6.0f, 1);
        AddNote(25, 7.0f, 2);
        AddNote(26, 0.0f, 1);
        AddNote(26, 1.5f, 4);
        AddNote(26, 3.0f, 3);
        AddNote(26, 4.0f, 4);
        AddNote(26, 5.5f, 3);
        AddNote(26, 7.0f, 2);
        AddNote(27, 0.0f, 1);

        //副歌
        AddNote(27, 6.0f, 2);
        AddNote(27, 6.5f, 1);
        AddNote(27, 7.0f, 2);
        AddNote(27, 7.5f, 1);
        AddNote(28, 0.0f, 2);
        AddNote(28, 1.5f, 4);
        AddNote(28, 3.0f, 3);
        AddNote(28, 3.5f, 1);
        AddNote(28, 4.0f, 4);
        AddNote(28, 5.5f, 2);
        AddNote(28, 7.0f, 3);
        AddNote(28, 7.5f, 1);
        AddNote(29, 0.0f, 4);
        AddNote(29, 1.5f, 2);
        AddNote(29, 3.0f, 1);
        AddNote(29, 3.5f, 2);


        AddNote(0, 4000f, 1);
        AddHold(0, 4000.0f, 5000.5f, 1);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameTime = 0.00000f-4*60f/BPM;
        dir = new Vector2(1.0f, 1.0f);
        FillTheQueue();
        // for(int i=0;i<1000;i++){
        //     Note tmp = new Note();
        //     tmp.t = i*60.0f/BPM;
        //     tmp.type = i%4+1;
        //     notes.Enqueue(tmp);
        //     turns.Enqueue(tmp);
        // }
        moving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameTime += Time.fixedDeltaTime;

        //transform.position += dir * moveSpeed * Time.deltaTime * ((gameTime>=-4*60/BPM)?1:0);
        if (gameTime >= -4 * 60f / BPM && moving == false){
            transform.position = new Vector3(0, 0, 0); 
            rb.velocity = dir.normalized * moveSpeed;
            moving = true;
        }else if(moving == false){
            rb.velocity = Vector2.zero; // 如果還沒到開始移動的時間，則速度為零
        }
        
        if(gameTime >= turns.Peek().t){
            
            //SwitchDirction(turns.Peek().type);
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
            if(turns.Peek().type == 5){ //tap
                GameObject newTapPrefeb = Instantiate(
                    tapPrefeb, 
                    transform.position+new Vector3(0,0,0), 
                    Quaternion.identity
                );
            }
            rb.velocity = rb.velocity.normalized * moveSpeed;
            locate.Enqueue(transform.position);
            angle.Enqueue(transform.rotation);
            spin.Enqueue(rb.angularVelocity);
            speed.Enqueue(rb.velocity);
            turns.Dequeue();
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

