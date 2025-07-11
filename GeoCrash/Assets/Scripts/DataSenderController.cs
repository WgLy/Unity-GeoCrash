using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSenderController : MonoBehaviour
{
    // 處理單例
    public static DataSenderController Instance { get; private set; }


    // 基礎數值
    public int songIndex;
    public int difficulty;
    public int BPM;
    public Queue<Note> notes = new Queue<Note>();
    public Queue<Hold> holds = new Queue<Hold>();
    public Queue<Effect> effects = new Queue<Effect>();
    public MovementStatus InitialStatus;
    public bool autoPlay;
    public int initialShape;
    public int songDeviation;

    //結束用
    public int finalScore;
    
    // 設定用
    public bool[] SettingIsOn = new bool[6];
    public bool isWallShine;
    public bool isLineShine;

    void Awake()
    {
        if (Instance != null && Instance != this) // by Gemini
        {
            // 銷毀新建立的這個物件，確保只有一個實例
            Debug.LogWarning("Duplicate DataSenderController found, destroying this one.");
            Destroy(this.gameObject);
        }
        else
        {
            // 如果 Instance 不存在，或者它就是自己，則將自己設為唯一的實例
            Instance = this;
            // 確保這個物件在場景切換時不會被銷毀
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        autoPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        autoPlay = SettingIsOn[0];
        isWallShine = SettingIsOn[2];
        isLineShine = SettingIsOn[4];

    }

    public void FillQFunction(){
        if(songIndex == 0){
            FillTheQueue();
        }
        if(songIndex == 1){
            FillTheQueue_NaughyCute();
        }
        if(songIndex == 2){
            FillTheQueue_NCS();
        }
    }

    /*
    特效一覽
    震動：
    AddEffect(<小節>, <第幾拍>, 1, <程度>, <持續時間(秒)>, 0.0f); // 震動
    變形：
    AddEffect(<小節>, <第幾拍>, 2, <形狀>, 0.0f, 0.0f); // 變形
        <形狀>
        1:正方形
        2:三角形
        3:正六邊形
    縮放：
    AddEffect(<小節>, <第幾拍>, 3, <縮放大小>, 0.0f, <速度>); // 縮放
    變色：
    AddEffect(<小節>, <第幾拍>, 4, <r>, <g>, <b>); // 變色
    傾斜：
    AddEffect(<小節>, <第幾拍>, 5, <傾斜目標角度>, <是否回彈>, <速度>); // 傾斜
        <是否回彈>
        0:不回彈
        1:自動回彈
    */

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
        songDeviation = -100;
        notes.Clear();
        holds.Clear();
        effects.Clear();

        for (int i=0;i<1000;i++)
        {
            //AddEffect(0, i*2f, 1, 5, 0.1f, 0.0f);
        }

        //        2
        //    4       3 
        //        1
        AddNote(0, 0.0f, 2);
        AddNote(0, 1.5f, 1);
        AddNote(0, 3.0f, 2);
        AddNote(0, 3.5f, 1);
        AddNote(1, 0.0f, 2);
        AddNote(1, 1.5f, 1);
        AddNote(1, 3.0f, 2);
        AddNote(1, 3.5f, 1);
        AddNote(2, 0.0f, 2);
        AddNote(2, 1.5f, 1);
        AddNote(2, 3.0f, 2);
        AddNote(2, 3.5f, 1);
        AddNote(2, 6.0f, 2);
        AddNote(2, 6.5f, 1);
        AddNote(2, 7.0f, 2);
        AddNote(4, 0.0f, 1);
        AddNote(4, 1.5f, 2);
        AddNote(4, 3.0f, 1);
        AddNote(4, 4.0f, 2);
        AddNote(4, 5.0f, 1);
        AddNote(4, 5.5f, 2);
        AddNote(4, 6.5f, 1);
        AddNote(4, 7.5f, 2);
        AddNote(6, 0.0f, 1);
        AddNote(6, 2.0f, 2);
        AddNote(6, 3.0f, 1);
        AddNote(6, 4.0f, 2);
        AddNote(6, 6.0f, 1);
        AddNote(6, 6.5f, 2);
        AddNote(6, 7.0f, 1);
        AddNote(6, 7.5f, 3);

        AddNote(8, 0.0f, 4);
        AddNote(10, 0.0f, 3);
        AddNote(12, 0.0f, 2);
        AddNote(14, 0.0f, 1);
        AddNote(14, 2.0f, 2);
        AddNote(14, 4.0f, 1);
        AddNote(14, 4.5f, 2);
        AddNote(14, 5.0f, 1);
        AddNote(14, 5.5f, 2);
        AddNote(14, 6.0f, 1);
        AddNote(14, 6.5f, 2);
        AddNote(14, 7.0f, 1);
        AddNote(14, 7.5f, 2);

        // 前奏B
        AddNote(16, 0.0f, 1);
        AddNote(16, 1.0f, 2);
        AddHold(16, 2.0f, 3.0f, 1);
        AddNote(16, 3.5f, 1);
        AddNote(16, 4.5f, 2);
        AddNote(16, 5.0f, 1);
        AddNote(16, 5.5f, 2);
        AddNote(16, 6.0f, 1);
        AddNote(16, 6.5f, 2);
        AddNote(16, 7.0f, 1);
        AddNote(16, 7.5f, 2);

        AddNote(18, 0.0f, 1);
        AddNote(18, 1.0f, 2);
        AddHold(18, 2.0f, 3.0f, 1);
        AddNote(18, 3.5f, 1);
        AddNote(18, 4.5f, 2);
        AddNote(18, 5.0f, 1);
        AddNote(18, 5.5f, 2);
        AddNote(18, 6.0f, 1);
        AddNote(18, 6.5f, 2);
        AddNote(18, 7.0f, 1);
        AddNote(18, 7.5f, 2);

        AddNote(20, 0.0f, 1);
        AddNote(20, 1.0f, 2);
        AddHold(20, 2.0f, 3.0f, 1);
        AddNote(20, 3.5f, 1);
        AddNote(20, 4.5f, 2);
        AddNote(20, 5.0f, 1);
        AddNote(20, 5.5f, 2);
        AddNote(20, 6.0f, 1);
        AddNote(20, 6.5f, 2);
        AddNote(20, 7.0f, 1);
        AddNote(20, 7.5f, 4);

        AddNote(22, 0.0f, 3);
        AddNote(22, 0.5f, 4);
        AddNote(22, 1.5f, 3);
        AddNote(22, 2.0f, 4);
        AddNote(22, 3.0f, 3);
        AddNote(22, 3.5f, 4);
        AddNote(22, 4.5f, 3);
        AddNote(22, 5.0f, 4);
        AddHold(22, 6.0f, 7.0f, 1);

        // 主歌A
        AddNote(24, 0.0f, 3);
        AddNote(24, 1.0f, 2);
        AddNote(24, 3.0f, 1);
        AddNote(24, 4.5f, 4);
        AddNote(24, 5.0f, 3);
        AddNote(24, 5.5f, 2);
        AddNote(24, 6.0f, 1);
        AddNote(24, 7.0f, 4);

        AddNote(26, 0.0f, 3);
        AddNote(26, 1.0f, 2);
        AddNote(26, 3.0f, 1);
        AddNote(26, 4.5f, 4);
        AddNote(26, 5.0f, 3);
        AddNote(26, 5.5f, 2);
        AddNote(26, 6.0f, 1);
        AddNote(26, 7.0f, 4);

        AddNote(28, 0.0f, 3);
        AddNote(28, 1.0f, 2);
        AddNote(28, 3.0f, 1);
        AddNote(28, 4.5f, 4);
        AddNote(28, 5.0f, 3);
        AddNote(28, 5.5f, 2);
        AddNote(28, 6.0f, 1);
        AddNote(28, 7.0f, 4);

        AddNote(30, 0.0f, 3);
        AddNote(30, 3.0f, 2);
        AddNote(30, 4.0f, 1);
        AddNote(30, 6.0f, 2);

        //主歌A-2
        AddNote(32, 0.0f, 4);
        AddNote(32, 1.0f, 3);
        AddNote(32, 3.0f, 1);
        AddNote(32, 4.5f, 4);
        AddNote(32, 5.0f, 3);
        AddNote(32, 5.5f, 2);
        AddNote(32, 6.0f, 1);
        AddNote(32, 7.0f, 2);

        AddNote(34, 0.0f, 4);
        AddNote(34, 1.0f, 3);
        AddNote(34, 4.0f, 1);
        AddNote(34, 5.0f, 2);
        AddNote(34, 6.0f, 1);
        AddNote(34, 7.0f, 2);

        AddNote(36, 0.0f, 4);
        AddNote(36, 1.5f, 3);
        AddNote(36, 3.0f, 1);
        AddNote(36, 4.5f, 4);
        AddNote(36, 5.0f, 3);
        AddNote(36, 5.5f, 2);
        AddNote(36, 6.0f, 1);
        AddNote(36, 7.0f, 2);

        AddNote(38, 0.0f, 1);

        // 主歌B
        AddNote(38, 6.0f, 2);
        AddNote(38, 7.0f, 1);
        AddHold(40, 0.0f, 6.0f, 1);
        AddNote(40, 0.0f, 2);
        AddNote(40, 2.0f, 1);
        AddNote(40, 4.0f, 2);
        AddNote(40, 6.0f, 1);
        AddHold(42, 0.0f, 6.0f, 1);
        AddNote(42, 4.0f, 5);
        AddNote(42, 6.0f, 5);
        AddHold(44, 0.0f, 6.0f, 1);
        AddNote(44, 0.0f, 2);
        AddNote(44, 2.0f, 1);
        AddNote(44, 4.0f, 2);
        AddNote(44, 6.0f, 1);
        AddHold(46, 0.0f, 6.0f, 1);
        AddNote(46, 4.0f, 5);
        AddNote(46, 6.0f, 5);

        //主歌B-2
        AddNote(46, 7.0f, 5);

        AddNote(48, 0.0f, 2);
        AddNote(48, 1.0f, 5);
        AddNote(48, 1.5f, 1);
        AddNote(48, 3.0f, 5);

        AddNote(48, 4.0f, 2);
        AddNote(48, 5.0f, 5);
        AddNote(48, 5.5f, 1);
        AddNote(48, 7.0f, 5);

        AddNote(50, 0.0f, 2);
        AddNote(50, 1.0f, 5);
        AddNote(50, 1.5f, 1);
        AddNote(50, 3.0f, 5);
        
        AddNote(50, 4.0f, 2);
        
        AddNote(50, 6.0f, 1);
        AddNote(50, 7.0f, 2);

        AddNote(52, 0.0f, 1);
        AddNote(52, 1.5f, 2);
        AddNote(52, 3.0f, 4);
        AddNote(52, 4.0f, 3);
        AddNote(52, 5.5f, 1);
        AddNote(52, 7.0f, 2);
        AddNote(54, 0.0f, 1);

        //副歌-1
        AddNote(54, 6.0f, 2);
        AddNote(54, 6.5f, 1);
        AddNote(54, 7.0f, 2);
        AddNote(54, 7.5f, 1);
        AddNote(56, 0.0f, 2);
        AddNote(56, 1.5f, 4);
        AddNote(56, 3.0f, 3);
        AddNote(56, 3.5f, 1);
        AddNote(56, 4.0f, 4);
        AddNote(56, 5.5f, 2);
        AddNote(56, 7.0f, 3);
        AddNote(56, 7.5f, 1);
        AddNote(58, 0.0f, 4);
        AddNote(58, 1.0f, 2);
        AddNote(58, 2.0f, 5);
        AddNote(58, 3.0f, 3);
        AddNote(58, 4.0f, 4);
        AddNote(58, 4.5f, 3);
        AddNote(58, 5.5f, 5);
        //-1 ,-1

        AddNote(58, 6.5f, 5);
        AddNote(58, 7.0f, 5);
        AddNote(58, 7.5f, 5);
        AddNote(60, 0.0f, 5);
        AddNote(60, 1.5f, 4);
        AddNote(60, 3.0f, 3);
        AddNote(60, 4.0f, 4);
        AddNote(60, 5.0f, 3);
        AddNote(60, 5.5f, 1);
        AddNote(60, 6.5f, 2);
        AddNote(60, 7.5f, 5);
        AddNote(62, 0.0f, 1);
        AddNote(62, 1.0f, 5);
        AddNote(62, 2.0f, 2);
        AddNote(62, 3.0f, 5);
        AddNote(62, 4.0f, 1);
        //(-1, 1)

        AddNote(62, 6.0f, 4);
        AddNote(62, 6.5f, 3);
        AddNote(62, 7.0f, 2);
        AddNote(62, 7.5f, 1);
        AddNote(64, 0.0f, 2);
        AddNote(64, 1.5f, 1);
        AddNote(64, 3.0f, 2);
        AddNote(64, 3.5f, 1);
        AddNote(64, 4.0f, 2);
        AddNote(64, 5.5f, 1);
        AddNote(64, 7.0f, 2);
        AddNote(64, 7.5f, 1);
        AddNote(66, 0.0f, 2);
        AddNote(66, 1.5f, 1);
        AddNote(66, 3.0f, 2);
        AddNote(66, 3.5f, 1);
        // (-1, 1)

        AddNote(66, 6.0f, 2);
        AddNote(66, 6.5f, 4);
        AddNote(66, 7.0f, 1);
        AddNote(68, 0.0f, 2);
        AddNote(68, 1.5f, 3);
        AddNote(68, 3.0f, 1);
        AddNote(68, 4.0f, 2);
        AddNote(68, 5.0f, 1);
        AddNote(68, 5.5f, 2);
        AddNote(68, 7.0f, 5);
        AddNote(68, 7.5f, 1);
        AddNote(70, 0.0f, 5);
        AddNote(70, 2.0f, 2);
        AddNote(70, 3.0f, 5);
        AddNote(70, 4.0f, 1);
        // (-1, 1)

        //副歌-2
        AddNote(72 -2, 6.0f, 2);
        AddNote(72 -2, 6.5f, 1);
        AddNote(72 -2, 7.0f, 2);
        AddNote(72 -2, 7.5f, 1);
        AddNote(74 -2, 0.0f, 2);
        AddNote(74 -2, 1.5f, 4);
        AddNote(74 -2, 3.0f, 3);
        AddNote(74 -2, 3.5f, 1);
        AddNote(74 -2, 4.0f, 4);
        AddNote(74 -2, 5.5f, 2);
        AddNote(74 -2, 7.0f, 3);
        AddNote(74 -2, 7.5f, 1);
        AddNote(76 -2, 0.0f, 4);
        AddNote(76 -2, 1.0f, 2);
        AddNote(76 -2, 2.0f, 5);
        AddNote(76 -2, 3.0f, 3);
        AddNote(76 -2, 4.0f, 4);
        AddNote(76 -2, 4.5f, 3);
        AddNote(76 -2, 5.5f, 5);
        //-1 ,-1 

        AddNote(76 -2, 6.5f, 5);
        AddNote(76 -2, 7.0f, 5);
        AddNote(76 -2, 7.5f, 5);
        AddNote(78 -2, 0.0f, 5);
        AddNote(78 -2, 1.5f, 4);
        AddNote(78 -2, 3.0f, 3);
        AddNote(78 -2, 4.0f, 4);
        AddNote(78 -2, 5.0f, 3);
        AddNote(78 -2, 5.5f, 1);
        AddNote(78 -2, 6.5f, 2);
        AddNote(78 -2, 7.5f, 5);
        AddNote(80 -2, 0.0f, 1);
        AddNote(80 -2, 1.0f, 5);
        AddNote(80 -2, 2.0f, 2);
        AddNote(80 -2, 3.0f, 5);
        AddNote(80 -2, 4.0f, 1);
        //(-1, 1)

        AddNote(80 -2, 6.0f, 4);
        AddNote(80 -2, 6.5f, 3);
        AddNote(80 -2, 7.0f, 2);
        AddNote(80 -2, 7.5f, 1);
        AddNote(82 -2, 0.0f, 2);
        AddNote(82 -2, 1.5f, 1);
        AddNote(82 -2, 3.0f, 2);
        AddNote(82 -2, 3.5f, 1);
        AddNote(82 -2, 4.0f, 2);
        AddNote(82 -2, 5.5f, 1);
        AddNote(82 -2, 7.0f, 2);
        AddNote(82 -2, 7.5f, 1);
        AddNote(84 -2, 0.0f, 2);
        AddNote(84 -2, 1.5f, 1);
        AddNote(84 -2, 3.0f, 2);
        AddNote(84 -2, 3.5f, 1);
        // (-1, 1)

        AddNote(84 -2, 6.0f, 2);
        AddNote(84 -2, 6.5f, 4);
        AddNote(84 -2, 7.0f, 1);
        AddNote(86 -2, 0.0f, 2);
        AddNote(86 -2, 1.5f, 3);
        AddNote(86 -2, 3.0f, 1);
        AddNote(86 -2, 4.0f, 2);
        AddNote(86 -2, 5.0f, 1);
        AddNote(86 -2, 5.5f, 2);
        AddNote(86 -2, 7.0f, 5);
        AddNote(86 -2, 7.5f, 4);
        AddNote(88 -2, 0.0f, 5);
        AddNote(88 -2, 2.0f, 1);
        AddNote(88 -2, 3.0f, 5);
        AddNote(88 -2, 4.0f, 2);
        // (1, -1)


        AddNote(0, 40000f, 0);
        AddHold(0, 40000.0f, 50000.5f, 0);
        AddEffect(0, 40000, 0, 1, 1, 0.0f);
    }

    void FillTheQueue2(){
        songIndex = 5;
        difficulty = 1;
        BPM = 180;
        InitialStatus.locate = new Vector3(0, 0, 0);
        InitialStatus.angle = Quaternion.identity;
        InitialStatus.spin = 0.0f;
        InitialStatus.dir = new Vector2(1, -1);
        InitialStatus.speed = 13;
        initialShape = 3;
        songDeviation = 0;
        notes.Clear();
        holds.Clear();
        effects.Clear();
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

    void FillTheQueue_NaughyCute(){
        songIndex = 1;
        difficulty = 1;
        BPM = 103;
        InitialStatus.locate = new Vector3(0, 0, 0);
        InitialStatus.angle = Quaternion.identity;
        InitialStatus.spin = 0.0f;
        InitialStatus.dir = new Vector2(1, 1);
        InitialStatus.speed = 13;
        initialShape = 1;
        songDeviation = -500; // 問題：測試系統測出-200左右的值
        notes.Clear();
        holds.Clear();
        effects.Clear();

        AddEffect(0, 0, 3, 6, 0.0f, 5);//縮放
        AddEffect(0, 0, 5, 20, 0, 2); // 傾斜
        AddEffect(0, 0, 4, 0, 0, 1);
        AddEffect(1, 0, 3, 5, 0.0f, 5);//縮放
        AddEffect(1, 0, 5, -20, 0, 4); // 傾斜
        AddEffect(1, 0, 4, 0, 0, 0);
        AddEffect(2, 0, 3, 6, 0.0f, 5);//縮放
        AddEffect(2, 0, 5, 20, 0, 4); // 傾斜
        AddEffect(2, 0, 4, 0, 0, 1);
        AddEffect(3, 0, 3, 5, 0.0f, 5);//縮放
        AddEffect(3, 0, 5, 0, 0, 2); // 傾斜
        AddEffect(3, 0, 4, 0, 0, 0);

        AddHold(4, 0.0f, 1.0f, 1);
        AddNote(4, 2.00f, 2);
        AddNote(4, 2.25f, 1);
        AddNote(4, 2.50f, 2);
        AddNote(4, 2.75f, 3);
        AddNote(4, 3.00f, 4);
        AddNote(4, 3.25f, 3);
        AddNote(4, 3.50f, 1);
        AddNote(4, 3.75f, 2);

        AddHold(5, 0.0f, 1.0f, 1);
        AddNote(5, 2.00f, 4);
        AddNote(5, 2.25f, 3);
        AddNote(5, 2.50f, 4);
        AddNote(5, 2.75f, 1);
        AddNote(5, 3.00f, 2);
        AddNote(5, 3.25f, 1);
        AddNote(5, 3.50f, 2);
        AddNote(5, 3.75f, 1);

        AddHold(6, 0.0f, 1.0f, 1);
        AddNote(6, 2.00f, 2);
        AddNote(6, 2.25f, 1);
        AddNote(6, 2.50f, 2);
        AddNote(6, 2.75f, 3);
        AddNote(6, 3.00f, 4);
        AddNote(6, 3.25f, 3);
        AddNote(6, 3.50f, 1);
        AddNote(6, 3.75f, 2);

        AddHold(7, 0.0f, 1.0f, 1);
        AddNote(7, 2.00f, 4);
        AddNote(7, 2.25f, 3);
        AddNote(7, 2.50f, 4);
        AddNote(7, 2.75f, 1);
        AddNote(7, 3.00f, 2);
        AddNote(7, 3.25f, 1);
        AddNote(7, 3.50f, 2);
        AddNote(7, 3.75f, 1);

        AddNote(8, 0.00f, 3);
        AddNote(8, 0.50f, 4);
        AddNote(8, 1.00f, 2);
        AddNote(8, 1.50f, 1);
        AddNote(8, 2.00f, 2);
        AddNote(8, 2.50f, 1);
        AddNote(8, 3.00f, 2);
        AddNote(8, 3.25f, 1);
        AddNote(8, 3.75f, 2);

        AddEffect(9, 0, 4, 0, 0, 1); // 變色
        AddHold(9, 0.0f, 0.5f, 1);
        AddHold(9, 1.0f, 1.5f, 1);
        AddHold(9, 2.0f, 2.5f, 1);
        AddHold(9, 3.0f, 3.5f, 1);
        AddEffect(9, 3.5f, 4, 0, 0, 0); // 變色
        AddNote(9, 3.5f, 3);
        AddNote(9, 3.75f, 4);
        
        AddNote(10, 0.00f, 3);
        AddNote(10, 0.50f, 4);
        AddNote(10, 1.00f, 1);
        AddNote(10, 1.50f, 2);
        AddNote(10, 2.00f, 1);
        AddNote(10, 2.50f, 2);
        AddNote(10, 3.00f, 1);
        AddNote(10, 3.25f, 2);
        AddNote(10, 3.75f, 1);

        AddEffect(11, 0, 4, 0, 0, 1); // 變色
        AddHold(11, 0.0f, 0.5f, 1);
        AddHold(11, 1.0f, 1.5f, 1);
        AddHold(11, 2.0f, 2.5f, 1);
        AddHold(11, 3.0f, 3.5f, 1);
        AddEffect(11, 3.5f, 4, 0, 0, 0); // 變色
        //(1, 1)

        AddNote(12, 0.00f, 3);
        AddNote(12, 0.50f, 4);
        AddNote(12, 1.00f, 2);
        AddNote(12, 1.50f, 1);
        AddNote(12, 2.00f, 2);
        AddNote(12, 2.50f, 1);
        AddNote(12, 3.00f, 2);
        AddNote(12, 3.25f, 1);
        AddNote(12, 3.75f, 2);

        AddEffect(13, 0, 4, 0, 0, 1); // 變色
        AddHold(13, 0.0f, 0.5f, 1);
        AddHold(13, 1.0f, 1.5f, 1);
        AddHold(13, 2.0f, 2.50f, 1);
        AddHold(13, 2.75f, 3.5f, 1);
        AddEffect(13, 3.5f, 4, 0, 0, 0); // 變色
        AddNote(13, 3.5f, 3);
        AddNote(13, 3.75f, 4);
        
        AddNote(14, 0.00f, 3);
        AddNote(14, 0.50f, 4);
        AddNote(14, 1.00f, 1);
        AddNote(14, 1.50f, 2);
        AddNote(14, 2.00f, 1);
        AddNote(14, 2.50f, 2);
        AddNote(14, 3.00f, 1);
        AddNote(14, 3.25f, 2);
        AddNote(14, 3.75f, 1);

        AddEffect(15, 0, 4, 0, 0, 1); // 變色
        AddHold(15, 0.0f, 0.5f, 1);
        AddHold(15, 1.0f, 1.5f, 1);
        AddHold(15, 2.0f, 2.5f, 1);
        AddHold(15, 3.0f, 3.5f, 1);
        AddEffect(15, 3.5f, 4, 0, 0, 0); // 變色
        //(1, 1)

        AddNote(16, 0.00f, 3);
        AddNote(16, 0.50f, 4);
        AddNote(16, 1.00f, 2);
        AddNote(16, 1.50f, 1);
        AddNote(16, 2.00f, 2);
        AddNote(16, 2.50f, 1);
        AddNote(16, 3.00f, 2);
        AddNote(16, 3.25f, 1);
        AddNote(16, 3.75f, 2);

        AddNote(17, 0.50f, 3);
        AddNote(17, 1.00f, 4);
        AddNote(17, 1.50f, 3);
        AddHold(17, 2.0f, 3.0f, 1);
        AddEffect(17, 2.0f, 5, 40, 1, 5); // 傾斜
        
        AddNote(18, 0.00f, 5);
        AddNote(18, 0.50f, 4);
        AddNote(18, 1.00f, 1);
        AddNote(18, 1.50f, 2);
        AddNote(18, 2.00f, 1);
        AddNote(18, 2.50f, 2);
        AddNote(18, 3.00f, 1);
        AddNote(18, 3.25f, 2);
        AddNote(18, 3.75f, 1);

        AddNote(19, 0.50f, 2);
        AddNote(19, 1.00f, 1);
        AddNote(19, 1.50f, 5);
        AddHold(19, 2.0f, 3.0f, 1);
        AddEffect(19, 2.0f, 5, -40, 1, 5); // 傾斜
        //(1, 1)

        AddNote(20, 0.00f, 3);
        AddNote(20, 0.50f, 4);
        AddNote(20, 1.00f, 2);
        AddNote(20, 1.50f, 1);
        AddNote(20, 2.00f, 2);
        AddNote(20, 2.50f, 1);
        AddNote(20, 3.00f, 2);
        AddNote(20, 3.25f, 1);
        AddNote(20, 3.75f, 2);

        AddEffect(21, 0.0f, 3, 8, 0.0f, 1); // 縮放
        AddHold(21, 0.0f, 0.25f, 1);
        AddHold(21, 0.5f, 0.75f, 1);
        AddHold(21, 1.0f, 1.25f, 1);
        AddHold(21, 1.5f, 1.75f, 1);
        AddHold(21, 2.0f, 2.25f, 1);
        AddHold(21, 2.5f, 2.75f, 1);
        AddEffect(21, 3.5f, 3, 5, 0.0f, 5); // 縮放
        AddNote(21, 3.5f, 3);
        AddNote(21, 3.75f, 4);
        
        AddNote(22, 0.00f, 3);
        AddNote(22, 0.50f, 4);
        AddNote(22, 1.00f, 1);
        AddNote(22, 1.50f, 2);
        AddNote(22, 2.00f, 1);
        AddNote(22, 2.50f, 2);
        AddNote(22, 3.00f, 1);
        AddNote(22, 3.25f, 2);
        AddNote(22, 3.75f, 1);

        AddHold(23, 0.0f, 3.5f, 1);
        AddNote(23, 1.00f, 5);
        AddNote(23, 2.00f, 5);
        AddNote(23, 3.00f, 5);
        //(1, 1) HOld and points, enter center

        AddNote(24, 0.00f, 2);
        AddNote(24, 0.00f, 5);
        AddNote(24, 1.00f, 1);
        AddNote(24, 1.00f, 5);
        AddNote(24, 2.00f, 2);
        AddNote(24, 2.00f, 5);
        AddNote(24, 3.00f, 1);
        AddNote(24, 3.00f, 5);

        AddNote(25, 0.00f, 2);
        AddNote(25, 0.00f, 5);

        AddNote(26, 0.00f, 1);
        AddNote(26, 0.00f, 5);
        AddNote(26, 1.00f, 2);
        AddNote(26, 1.00f, 5);
        AddNote(26, 2.00f, 1);
        AddNote(26, 2.00f, 5);
        AddNote(26, 3.00f, 2);
        AddNote(26, 3.00f, 5);

        AddNote(27, 0.00f, 1);
        AddNote(27, 0.00f, 5);

        AddNote(28, 0.00f, 2);
        AddNote(28, 0.50f, 1);
        AddNote(28, 0.50f, 5);
        AddNote(28, 1.00f, 2);
        AddNote(28, 1.50f, 1);
        AddNote(28, 1.50f, 5);
        AddNote(28, 2.00f, 2);
        AddNote(28, 2.50f, 1);
        AddNote(28, 2.50f, 5);
        AddNote(28, 3.00f, 2);
        AddNote(28, 3.50f, 1);
        AddNote(28, 3.50f, 5);

        AddNote(29, 0.00f, 2);

        AddNote(29, 0.50f, 1);
        AddNote(29, 0.75f, 2);
        AddNote(29, 1.00f, 1);
        AddNote(29, 1.50f, 2);
        AddNote(29, 1.75f, 1);
        AddNote(29, 2.00f, 2);
        AddNote(29, 2.50f, 1);
        AddNote(29, 2.75f, 2);
        AddNote(29, 3.00f, 1);
        AddNote(29, 3.50f, 2);
        AddNote(29, 3.75f, 1);

        AddNote(30, 0.00f, 2);
        AddNote(30, 0.50f, 1);
        AddNote(30, 0.50f, 5);
        AddNote(30, 1.00f, 2);
        AddNote(30, 1.50f, 1);
        AddNote(30, 1.50f, 5);
        AddNote(30, 2.00f, 2);
        AddNote(30, 2.50f, 1);
        AddNote(30, 2.50f, 5);
        AddNote(30, 3.00f, 2);
        AddNote(30, 3.50f, 1);
        AddNote(30, 3.50f, 5);

        AddNote(31, 0.00f, 2);

        AddNote(31, 0.50f, 1);
        AddNote(31, 0.75f, 2);
        AddNote(31, 1.00f, 1);
        AddNote(31, 1.50f, 2);
        AddNote(31, 1.75f, 1);
        AddNote(31, 2.00f, 2);
        AddNote(31, 2.50f, 1);
        AddNote(31, 2.75f, 2);
        AddNote(31, 3.00f, 1);
        AddNote(31, 3.50f, 2);
        AddNote(31, 3.75f, 1);
        //(1, 1) pass center

        AddNote(32, 0.00f, 3);
        AddNote(32, 0.50f, 4);
        AddNote(32, 1.00f, 3);
        AddNote(32, 1.50f, 4);
        AddNote(32, 2.00f, 3);
        AddNote(32, 2.50f, 4);
        AddNote(32, 3.00f, 3);
        AddNote(32, 3.25f, 4);
        AddNote(32, 3.75f, 3);

        AddEffect(33, 0, 4, 0, 0, 1); // 變色
        AddHold(33, 0.0f, 0.5f, 1);
        AddNote(33, 1.0f, 5);
        AddNote(33, 1.5f, 5);
        AddHold(33, 2.0f, 2.5f, 1);
        AddNote(33, 3.0f, 5);
        AddNote(33, 3.5f, 5);
        AddEffect(33, 3.5f, 4, 0, 0, 0); // 變色
        
        AddNote(34, 0.00f, 4);
        AddNote(34, 0.50f, 2);
        AddNote(34, 1.00f, 1);
        AddNote(34, 1.50f, 2);
        AddNote(34, 2.00f, 1);
        AddNote(34, 2.50f, 2);
        AddNote(34, 3.00f, 1);
        AddNote(34, 3.25f, 2);
        AddNote(34, 3.75f, 1);

        AddEffect(35, 0, 4, 0, 0, 1); // 變色
        AddEffect(35, 0.0f, 1, 5, 2.04f, 0.0f); // 震動
        AddHold(35, 0.0f, 3.5f, 1);
        AddEffect(35, 3.5f, 4, 0, 0, 0); // 變色
        AddNote(35, 3.5f, 3);
        AddNote(35, 3.75f, 4);
        //(1, 1)

        AddNote(36, 0.00f, 3);
        AddNote(36, 0.50f, 4);
        AddNote(36, 1.00f, 2);
        AddNote(36, 1.50f, 1);
        AddNote(36, 2.00f, 2);
        AddNote(36, 2.50f, 1);
        AddNote(36, 3.00f, 2);
        AddNote(36, 3.25f, 1);
        AddNote(36, 3.75f, 2);

        AddEffect(37, 0.0f, 3, 8, 0.0f, 1); // 縮放
        AddHold(37, 0.0f, 0.25f, 1);
        AddHold(37, 0.5f, 0.75f, 1);
        AddHold(37, 1.0f, 1.25f, 1);
        AddHold(37, 1.5f, 1.75f, 1);
        AddHold(37, 2.0f, 2.25f, 1);
        AddHold(37, 2.5f, 2.75f, 1);
        AddEffect(37, 3.5f, 3, 5, 0.0f, 5); // 縮放
        
        AddNote(38, 0.00f, 3);
        AddNote(38, 0.50f, 4);
        AddNote(38, 1.00f, 1);
        AddNote(38, 1.50f, 2);
        AddNote(38, 2.00f, 1);
        AddNote(38, 2.50f, 2);
        AddNote(38, 3.00f, 1);
        AddNote(38, 3.25f, 2);
        AddNote(38, 3.75f, 1);

        AddHold(39, 0.0f, 3.5f, 1);
        AddNote(39, 1.00f, 5);
        AddNote(39, 2.00f, 5);
        AddNote(39, 3.00f, 5);
        //(1, 1)

        AddNote(40, 0.00f, 2);
        AddNote(40, 0.50f, 1);
        AddNote(40, 1.00f, 3);
        AddNote(40, 1.50f, 4);
        AddNote(40, 2.00f, 2);
        AddNote(40, 2.50f, 1);
        AddNote(40, 3.00f, 3);
        AddNote(40, 3.25f, 4);
        AddNote(40, 3.75f, 2);

        AddNote(41, 0.50f, 3);
        AddNote(41, 1.00f, 4);
        AddNote(41, 1.50f, 3);
        AddHold(41, 2.0f, 3.0f, 1);
        AddEffect(41, 2.0f, 5, 30, 1, 5); // 傾斜
        //(-1, 1)

        AddNote(42, 0.00f, 4);
        AddNote(42, 0.50f, 1);
        AddNote(42, 1.00f, 5);
        AddNote(42, 1.50f, 2);
        AddNote(42, 2.00f, 3);
        AddNote(42, 2.50f, 4);
        AddNote(42, 3.00f, 5);
        AddNote(42, 3.25f, 3);
        AddNote(42, 3.75f, 1);

        AddNote(43, 0.50f, 2);
        AddNote(43, 1.00f, 1);
        AddNote(43, 1.50f, 5);
        AddHold(43, 2.0f, 3.0f, 1);
        AddEffect(43, 2.0f, 5, -30, 1, 5); // 傾斜
        //(-1, 1)

        AddNote(44, 0.00f, 4);
        AddNote(44, 0.50f, 3);
        AddNote(44, 1.00f, 2);
        AddNote(44, 1.50f, 1);
        AddNote(44, 2.00f, 2);
        AddNote(44, 2.50f, 1);
        AddNote(44, 3.00f, 2);
        AddNote(44, 3.25f, 1);
        AddNote(44, 3.75f, 2);

        AddEffect(45, 0.0f, 3, 8, 0.0f, 1); // 縮放
        AddHold(45, 0.0f, 0.25f, 1);
        AddHold(45, 0.5f, 0.75f, 1);
        AddHold(45, 1.0f, 1.25f, 1);
        AddHold(45, 1.5f, 1.75f, 1);
        AddHold(45, 2.0f, 2.25f, 1);
        AddHold(45, 2.5f, 2.75f, 1);
        AddEffect(45, 3.5f, 3, 5, 0.0f, 5); // 縮放
        AddNote(45, 3.5f, 5);
        AddNote(45, 3.75f, 4);
        
        AddNote(46, 0.00f, 3);
        AddNote(46, 0.50f, 4);
        AddNote(46, 1.00f, 1);
        AddNote(46, 1.50f, 2);
        AddNote(46, 2.00f, 1);
        AddNote(46, 2.50f, 2);
        AddNote(46, 3.00f, 1);
        AddNote(46, 3.25f, 2);
        AddNote(46, 3.75f, 1);

        AddHold(47, 0.0f, 3.5f, 1);
        
        
        AddNote(0, 40000f, 0);
        AddHold(0, 40000.0f, 50000.5f, 0);
        AddEffect(0, 40000, 0, 1, 1, 0.0f);
    }

    void FillTheQueue_NCS(){
        songIndex = 2;
        difficulty = 1;
        BPM = 128;
        InitialStatus.locate = new Vector3(0, 0, 0);
        InitialStatus.angle = Quaternion.identity;
        InitialStatus.spin = 0.0f;
        InitialStatus.dir = new Vector2(1, 1);
        InitialStatus.speed = 13;
        initialShape = 1;
        songDeviation = -300; 
        notes.Clear();
        holds.Clear();
        effects.Clear();

        AddEffect(0, 0.0f, 4, 0, 1, 0); // 變色
        AddEffect(0, 0.0f, 3, 5, 0.0f, 100); // 縮放
        AddEffect(0, 0.0f+0.1f, 3, 7, 0.0f, 5); // 縮放

        AddEffect(0, 2.0f, 4, 1, 0, 0); // 變色
        AddEffect(0, 2.0f, 3, 5, 0.0f, 100); // 縮放
        AddEffect(0, 2.0f+0.1f, 3, 7, 0.0f, 5); // 縮放

        AddEffect(1, 0.0f, 4, 0, 0, 1); // 變色
        AddEffect(1, 0.0f, 3, 5, 0.0f, 100); // 縮放
        AddEffect(1, 0.0f+0.1f, 3, 7, 0.0f, 5); // 縮放

        AddEffect(1, 2.0f, 4, 0, 0, 0); // 變色
        AddEffect(1, 2.0f, 3, 5, 0.0f, 100); // 縮放
        AddEffect(1, 2.0f+0.1f, 3, 7, 0.0f, 5); // 縮放

        
        AddNote(2, 0.0f, 5);
        AddNote(2, 0.5f, 5);
        AddNote(2, 1.0f, 5);
        AddNote(2, 1.5f, 5);
        AddNote(2, 2.0f, 5);
        AddNote(2, 2.5f, 5);
        AddNote(2, 3.0f, 5);
        AddNote(2, 3.5f, 5);
        AddNote(3, 0.0f, 5);
        AddNote(3, 0.5f, 5);
        AddNote(3, 1.0f, 5);
        AddNote(3, 1.5f, 5);
        AddNote(3, 2.0f, 5);
        AddNote(3, 2.5f, 5);
        AddNote(3, 3.0f, 5);
        AddNote(3, 3.5f, 5);

        AddNote(0, 40000f, 1);
        AddHold(0, 40000.0f, 50000.5f, 0);
        AddEffect(0, 40000, 0, 1, 1, 0.0f);
    }

    void AddNote(int p, float t, int type){  // 加入音符函式
        Note tmp = new Note();
        tmp.t = (t+p*4)*60.0f/BPM;
        tmp.type = type;
        notes.Enqueue(tmp);
    }

    void AddHold(int p, float t_b, float t_e, int type){  // 加入長條函式
        Hold tmp = new Hold();
        tmp.t_begin = (t_b+p*4)*60.0f/BPM;
        tmp.t_end = (t_e+p*4)*60.0f/BPM;
        tmp.type = type;
        holds.Enqueue(tmp);
    }

    void AddEffect(int p, float t, int type, float degree, float duriation, float speed){  // 加入特效函式
        Effect tmp = new Effect();
        tmp.t = (t+p*4)*60.0f/BPM;
        tmp.type = type;
        tmp.degree = degree;
        tmp.duriation = duriation;
        tmp.speed = speed;
        effects.Enqueue(tmp);
    }
}
