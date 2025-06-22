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
