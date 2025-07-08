using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    
    public AudioSource audioSource;
    public AudioSource NaughyCuteSource;
    public AudioSource tapSoundSource;

    // 暫停保存狀態
    public bool stopping;
    public float stoppingTime;

    // Start is called before the first frame update
    void Start()
    {
        dataSenderController = FindObjectOfType<DataSenderController>();
        stopping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        stoppingTime += Time.fixedDeltaTime;
        if(Input.GetKeyDown(KeyCode.Escape)){ // 暫停與啟動
            if(stopping == false && stoppingTime >= 0.1f){
                stopping = true;
                if (audioSource.isPlaying) audioSource.Pause();
                if (NaughyCuteSource.isPlaying) NaughyCuteSource.Pause();
                stoppingTime = 0;
            }else if(stopping == true && stoppingTime >= 0.1f){
                stopping = false;
                if (!audioSource.isPlaying) audioSource.UnPause();
                if (!NaughyCuteSource.isPlaying) NaughyCuteSource.UnPause();
                stoppingTime = 0;
            }
        }
    }

    public void StartMusic(){
        if(dataSenderController.songIndex == 0) audioSource.Play();
        if(dataSenderController.songIndex == 1) NaughyCuteSource.Play();
    }

    public void PlayTapSound(){
        tapSoundSource.Play();
    }

    public void StopAllSound(){
        audioSource.Stop();
        NaughyCuteSource.Stop();
    }
}
