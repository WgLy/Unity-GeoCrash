using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 載入其他場景

public class FadingController : MonoBehaviour
{
    public float fadeDuration = 1.0f; // 透明度變化持續時間，單位秒
    public Image image;
    Color tempColor;
    bool fading;
    bool inverseFading;
    public float initialA;
    public string nextLoadScene;

    //for test
    public DataSenderController dataSenderController;

    // Start is called before the first frame update
    void Start()
    {
        dataSenderController = FindObjectOfType<DataSenderController>();
        tempColor = image.color;
        tempColor.a = initialA;
        image.color = tempColor;
        fading = false;
        inverseFading = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* for test
        if(Input.GetKeyDown(KeyCode.F)){
            Fade(fadeDuration, false);
        }
        if(Input.GetKeyDown(KeyCode.G)){
            Fade(fadeDuration, true);
        }
        */
        //Debug.Log(inverseFading.ToString());

        if(fading){
            tempColor = image.color;
            tempColor.a += Time.deltaTime / fadeDuration;
            image.color = tempColor;
            if(image.color.a>=1.0f){
                fading = false;
                tempColor = image.color;
                tempColor.a = 1.0f;
                image.color = tempColor;
                if(nextLoadScene != ""){
                    SceneManager.LoadScene(nextLoadScene);
                    nextLoadScene = "";
                }
            }
            
        }
        if(inverseFading){
            tempColor = image.color;
            tempColor.a -= Time.deltaTime / fadeDuration;
            image.color = tempColor;
            if(image.color.a<=0.0f){
                inverseFading = false;
                tempColor = image.color;
                tempColor.a = 0.0f;
                image.color = tempColor;
                if(nextLoadScene != ""){
                    SceneManager.LoadScene(nextLoadScene);
                    nextLoadScene = "";
                }
            }
        }
    }

    public void Fade(bool isInverse, string nLS){
        if(!isInverse){
            nextLoadScene = nLS;
            fading = true;
            inverseFading = false;
        }else{
            nextLoadScene = nLS;
            fading = false;
            inverseFading = true;
        }
    }
}
