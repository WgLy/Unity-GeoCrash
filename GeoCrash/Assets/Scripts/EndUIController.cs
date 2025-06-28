using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false); // 禁用整個 GameObject
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(){
        this.gameObject.SetActive(true); // 啟用整個 GameObject
    }
}
