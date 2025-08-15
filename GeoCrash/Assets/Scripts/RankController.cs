using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public NumbersListController numbersListController;
    public List<Sprite> chars = new List<Sprite>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(numbersListController.showNumber >= 10000000){
            spriteRenderer.sprite = chars[0];
        }else if(numbersListController.showNumber >= 9200000){
            spriteRenderer.sprite = chars[1];
        }else if(numbersListController.showNumber >= 8800000){
            spriteRenderer.sprite = chars[2];
        }else if(numbersListController.showNumber >= 7000000){
            spriteRenderer.sprite = chars[3];
        }else if(numbersListController.showNumber >= 6000000){
            spriteRenderer.sprite = chars[4];
        }else{
            spriteRenderer.sprite = chars[6];
        }
    }
}
