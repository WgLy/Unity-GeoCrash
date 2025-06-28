using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSenderController : MonoBehaviour
{
    public int songIndex;
    public int difficulty;
    public int BPM;
    public Queue<Note> notes = new Queue<Note>();
    public Queue<Hold> holds = new Queue<Hold>();
    public Queue<Effect> effects = new Queue<Effect>();
    public MovementStatus InitialStatus;
    public bool autoPlay;
    public int initialShape;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        autoPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillQFunction(){
        if(Input.GetKeyDown(KeyCode.Return)){
            if(songIndex == 0){
                FillTheQueue();
            }
            if(songIndex == 1){
                FillTheQueue2();
            }
        }
    }

    void FillTheQueue(){
        songIndex = 0;
        difficulty = 1;
        BPM = 180;
        InitialStatus.locate = new Vector3(0, 0, 0);
        InitialStatus.angle = Quaternion.identity;
        InitialStatus.spin = 0.0f;
        InitialStatus.dir = new Vector2(1, 1);
        InitialStatus.speed = 13;
        initialShape = 1;
        notes.Clear();
        holds.Clear();
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
        AddNote(26, 3.0f, 2);
        AddNote(26, 4.0f, 3);
        AddNote(26, 5.5f, 1);
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

        AddNote(0, 40000f, 0);
        AddHold(0, 40000.0f, 50000.5f, 0);
    }

    void FillTheQueue2(){
        songIndex = 1;
        difficulty = 1;
        BPM = 180;
        InitialStatus.locate = new Vector3(0, 0, 0);
        InitialStatus.angle = Quaternion.identity;
        InitialStatus.spin = 0.0f;
        InitialStatus.dir = new Vector2(1, -1);
        InitialStatus.speed = 13;
        initialShape = 3;
        notes.Clear();
        holds.Clear();
        //        2
        //    4       3 
        //        1
        AddHold(0, 1.0f, 2.0f, 1);
        AddNote(0, 3.5f, 5);
        AddNote(0, 4.0f, 5);
        AddNote(0, 5.5f, 5);
        AddNote(0, 7.0f, 5);
        AddNote(0, 7.5f, 5);
        AddNote(1, 0.0f, 5);
        AddNote(1, 1.5f, 5);
        AddNote(1, 3.0f, 5);
        AddNote(1, 3.5f, 5);
        AddNote(1, 6.0f, 5);
        AddNote(1, 6.5f, 5);
        AddNote(1, 7.0f, 5);
        AddNote(2, 0.0f, 5);
        AddNote(2, 1.5f, 5);
        AddNote(2, 3.0f, 5);
        AddNote(2, 4.0f, 5);
        AddNote(2, 5.0f, 5);
        AddNote(2, 5.5f, 5);
        AddNote(2, 6.5f, 5);
        AddNote(2, 7.5f, 5);
        AddNote(3, 0.0f, 5);
        AddNote(3, 2.0f, 5);
        AddNote(3, 3.0f, 5);
        AddNote(3, 4.0f, 5);
        AddNote(3, 6.0f, 5);
        AddNote(3, 6.5f, 5);
        AddNote(3, 7.0f, 5);
        AddNote(3, 7.5f, 5);

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
        AddNote(26, 3.0f, 2);
        AddNote(26, 4.0f, 3);
        AddNote(26, 5.5f, 1);
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

    void AddNote(int p,float t,int type){  // 加入音符函式
        Note tmp = new Note();
        tmp.t = (t+p*8)*60.0f/BPM;
        tmp.type = type;
        notes.Enqueue(tmp);
    }

    void AddHold(int p,float t_b,float t_e,int type){  // 加入長條函式
        Hold tmp = new Hold();
        tmp.t_begin = (t_b+p*8)*60.0f/BPM;
        tmp.t_end = (t_e+p*8)*60.0f/BPM;
        tmp.type = type;
        holds.Enqueue(tmp);
    }
}
