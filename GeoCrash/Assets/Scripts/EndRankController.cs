using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空間

public class EndRankController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    public int totalscore;
    public TextMeshProUGUI myText;
    public RectTransform uiElementRectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        myText.text = "N";
        dataSenderController = FindObjectOfType<DataSenderController>();
        totalscore = dataSenderController.finalScore;
        if(dataSenderController.finalScore > dataSenderController.songHighScoreList[dataSenderController.songIndex] && !dataSenderController.autoPlay){
            dataSenderController.songHighScoreList[dataSenderController.songIndex] = dataSenderController.finalScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(totalscore >= 10000000){
            myText.text = "P";
        }else if(totalscore >= 9200000){
            myText.text = "S";
        }else if(totalscore >= 8800000){
            myText.text = "A";
        }else if(totalscore >= 7000000){
            myText.text = "B";
        }else if(totalscore >= 6000000){
            myText.text = "C";
        }else{
            myText.text = "F";
        }
    }
}
