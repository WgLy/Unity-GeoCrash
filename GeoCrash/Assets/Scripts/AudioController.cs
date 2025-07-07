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
    // Start is called before the first frame update
    void Start()
    {
        dataSenderController = FindObjectOfType<DataSenderController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
