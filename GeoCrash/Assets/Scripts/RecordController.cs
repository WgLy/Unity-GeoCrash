using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    string recordCode;
    // Start is called before the first frame update
    void Start()
    {
        
        dataSenderController = FindObjectOfType<DataSenderController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            recordCode = GUIUtility.systemCopyBuffer;
            Debug.Log(recordCode);
            if(recordCode.Substring(0, 9) = "GeoCrash:"){
                recordCode = recordCode.Substring(9);
            }
        }
        if(Input.GetKeyDown(KeyCode.T)){
            recordCode = "GeoCrash:";

            GUIUtility.systemCopyBuffer = recordCode;
        }
        
    }
}
