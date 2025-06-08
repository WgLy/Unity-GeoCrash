using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorController : MonoBehaviour
{
    struct Song{
        public string name;
        public string author;
        public int BPM;
        public int difficulty;
    };
    public GameObject songTitlePrefeb;
    List<Song> songList = new List<Song>();
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3.0f, 0);
        Song tmp = new Song();
        tmp.name = "ForTest";
        tmp.author = "ME";
        tmp.BPM = 180;
        tmp.difficulty = 1;
        songList.Add(tmp);

        for(int i=0;i<songList.Count;i++){
            GameObject currentSongTitle = Instantiate(songTitlePrefeb, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
