using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空間

public class EndRankController : MonoBehaviour
{
    public float score;
    public float max_score;
    public int totalscore;
    public TextMeshProUGUI myText;
    public CharacterController characterController;
    public RectTransform uiElementRectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        myText.text = "N";
    }

    // Update is called once per frame
    void Update()
    {
        score = characterController.score;
        max_score = characterController.max_score;
        totalscore = (int)(10000000*(score/max_score));
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
