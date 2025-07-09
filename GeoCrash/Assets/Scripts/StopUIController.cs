using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopUIController : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image image;
    public int selfIndex;
    public bool initialShow;
    public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false); // 禁用整個 GameObject
        /*
        if(selfIndex == -1){
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
        if(selfIndex == 0){
            rectTransform.anchoredPosition = new Vector3(-2, 0, 0);
        }
        if(selfIndex == 1){
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
        if(selfIndex == 2){
            rectTransform.anchoredPosition = new Vector3(2, 0, 0);
        }
        */
        image.color = new Color(
            image.color.r,
            image.color.g,
            image.color.b,
            0.0f
        );
        initialShow = false;
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(initialShow == false){
            if(image.color.a > 0.7){
                image.color = new Color(
                    image.color.r,
                    image.color.g,
                    image.color.b,
                    0.7f
                );
                initialShow = true;
            }else{
                image.color += new Color(
                    0,
                    0,
                    0,
                    Time.deltaTime
                );
            }
        }else{
            if(selfIndex == currentIndex){
                image.color = new Color(
                    image.color.r,
                    image.color.g,
                    image.color.b,
                    1.0f
                );
            }else{
                image.color = new Color(
                    image.color.r,
                    image.color.g,
                    image.color.b,
                    0.7f
                );
            }
        }

        
        
        if(Input.GetKeyDown(KeyCode.A)){
            currentIndex-=1;
            if(currentIndex==-1) currentIndex = 2;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            currentIndex+=1;
            if(currentIndex==3) currentIndex = 0;
        }
    }

    public void Show(){
        this.gameObject.SetActive(true); // 啟用整個 GameObject
    }
    public void UnShow(){
        this.gameObject.SetActive(false); // 啟用整個 GameObject
    }
}
