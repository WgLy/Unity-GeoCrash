using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空間

public class ScoreController : MonoBehaviour
{
    public float score;
    public float max_score;
    public TextMeshProUGUI myText;
    public CharacterController characterController;
    public RectTransform uiElementRectTransform;


    // Start is called before the first frame update
    void Start()
    {
        uiElementRectTransform.anchoredPosition = new Vector2(-245, 166);
    }

    // Update is called once per frame
    void Update()
    {
        score = characterController.score;
        max_score = characterController.max_score;
        myText.text = ((int)(10000000*(score/max_score))).ToString();
    }

    public void Move(){
        uiElementRectTransform.anchoredPosition = new Vector2(-61, -101);
    }
}
