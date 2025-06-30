using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Note{
    public float t;
    public int type;
};

[System.Serializable]
public struct Hold{
    public float t_begin,t_end;
    public int type;
};

[System.Serializable]
public struct MovementStatus{
    public Vector3 locate; // 位置
    public Quaternion angle; // 角度
    public float spin; // 角速度
    public Vector2 dir; // 移動方向
    public float speed; // 速度
};

[System.Serializable]
public struct Effect{
    public float t;
    public int type;
    public int degree;
};

[System.Serializable]
struct Song{
    public string name;
    public string author;
    public int BPM;
    public int difficulty;
};

public class GameDataStructure : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
