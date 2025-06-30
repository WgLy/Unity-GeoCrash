using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorController : MonoBehaviour
{
    
    public GameObject songTitlePrefeb;
    List<Song> songList = new List<Song>();
    public int sizeOfSongList;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hi");
        transform.position = new Vector3(0, -3.0f, 0);
        Song tmp = new Song();
        tmp.name = "ForTest";
        tmp.author = "ME";
        tmp.BPM = 180;
        tmp.difficulty = 0;
        songList.Add(tmp);
        for(int i=1;i<10;i++){
            tmp.difficulty = i;
            songList.Add(tmp);
        }

        for(int i=0;i<songList.Count;i++){
            GameObject currentSongTitle = Instantiate(songTitlePrefeb, transform.position, Quaternion.identity);
            currentSongTitle.transform.position = new Vector3(-3, -1.5f*i, 0);
            Debug.Log("place new title " + i.ToString());
        }
        sizeOfSongList = songList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
