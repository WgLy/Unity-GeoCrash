using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空間

public class EndScoreController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    public TextMeshProUGUI myText;
    public FadingController fadingController;
    bool havebeenFade;
    // Start is called before the first frame update
    void Start()
    {
        
        dataSenderController = FindObjectOfType<DataSenderController>();
        myText.text = dataSenderController.finalScore.ToString();
        havebeenFade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!havebeenFade){
            havebeenFade = true;
            fadingController.Fade(true, "");
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            fadingController.Fade(false, "MainScene");
        }
    }
}
