using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空間

public class NoteDetailsController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    public TextMeshProUGUI myText;
    
    // Start is called before the first frame update
    void Start()
    {
        dataSenderController = FindObjectOfType<DataSenderController>();
        myText.text = (dataSenderController.perfect_num).ToString()+"-"+(dataSenderController.good_num).ToString()+"-"+(dataSenderController.miss_num).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
