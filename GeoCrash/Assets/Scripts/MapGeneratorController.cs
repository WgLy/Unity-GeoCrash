using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorController : MonoBehaviour
{
    
    public GameObject songTitlePrefeb;
    List<Song> songList = new List<Song>();
    public int sizeOfSongList;
    public List<GameObject> songTitlePrefebList = new List<GameObject>();
    public int currentSongIndex;
    public Vector3 idealPosition = new Vector3();

    float holdingTime_W;
    float holdingTime_S;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3.0f, 0);
        Song tmp = new Song();
        tmp.name = "ForTest";
        tmp.author = "ME";
        tmp.BPM = 180;
        tmp.difficulty = 0;
        songList.Add(tmp);
        for(int i=1;i<3;i++){
            tmp.difficulty = i;
            songList.Add(tmp);
        }

        for(int i=0;i<songList.Count;i++){
            GameObject currentSongTitle = Instantiate(songTitlePrefebList[i], transform.position, Quaternion.identity);
            currentSongTitle.transform.position = new Vector3(-4.84f, -1.5f*i, 0);
            currentSongTitle.transform.localScale = new Vector3(1.5f, 1.5f, 0);
            currentSongTitle.GetComponent<SongDetailUIController>().songIndex = i;
            currentSongTitle.GetComponent<SongDetailUIController>().gap = 1.5f;
        }
        sizeOfSongList = songList.Count;
        currentSongIndex = 0;
        idealPosition = new Vector3(-4.84f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            currentSongIndex--;
            if(currentSongIndex==-1){
                currentSongIndex = sizeOfSongList-1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){
            currentSongIndex++;
            if(currentSongIndex==sizeOfSongList){
                currentSongIndex = 0;
            }
        }

        if(Input.GetKey(KeyCode.W)){ // 紀錄W長按時間
            holdingTime_W += Time.deltaTime;  
        }else{
            holdingTime_W = 0; 
        }
        if(Input.GetKey(KeyCode.S)){ // 紀錄S長按時間 
            holdingTime_S += Time.deltaTime; 
        }else{
            holdingTime_S = 0; 
        }
        if(holdingTime_W >= 0.5f){ // 長按W快速上升
            currentSongIndex--;
            if(currentSongIndex==-1){
                currentSongIndex = sizeOfSongList-1;
            }
            holdingTime_W -= 0.05f;
        }
        if(holdingTime_S >= 0.5f){ // 長按S快速下降
            currentSongIndex++;
            if(currentSongIndex==sizeOfSongList){
                currentSongIndex = 0;
            }
            holdingTime_S -= 0.05f;
        }
        
        idealPosition = new Vector3(-4.84f, 1.5f*currentSongIndex, 0);
    }
}
