using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    public SpriteRenderer spriteRenderer;
    public Sprite on;
    public Sprite off;
    // Start is called before the first frame update
    void Start()
    {
        
        dataSenderController = FindObjectOfType<DataSenderController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dataSenderController.isChallenge == true){
            spriteRenderer.sprite = on;
        }else{
            spriteRenderer.sprite = off;
        }
        if(Input.GetKeyDown(KeyCode.K)){
            dataSenderController.isChallenge = !dataSenderController.isChallenge;
        }
    }
}
