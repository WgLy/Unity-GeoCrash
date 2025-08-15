using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController : MonoBehaviour
{
    public DataSenderController dataSenderController;
    string recordCode; 
    string tmp;
    string codeList = "/*-+&*()_=";
    
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
            tmp = recordCode.Substring(0, 9);
            if(tmp == "GeoCrash:" && recordCode.Length == 39){
                recordCode = recordCode.Substring(9);
                for(int i=0;i<6;i++){
                    if(recordCode[i]==EnCode('1')){
                        dataSenderController.SettingIsOn[i] = true;
                    }else{
                        dataSenderController.SettingIsOn[i] = false;
                    }
                }
                for(int i=0;i<3;i++){
                    tmp = "";
                    for(int j=0;j<8;j++){
                        tmp += DeCode(recordCode.Substring(6+8*i, 8)[j]).ToString();
                    }
                    dataSenderController.songHighScoreList[i] = int.Parse(tmp);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.T)){
            recordCode = "GeoCrash:";
            for(int i=0;i<6;i++){
                if(dataSenderController.SettingIsOn[i] == true){
                    recordCode += EnCode('1');
                }else{
                    recordCode += EnCode('0');
                }
            }
            for(int i=0;i<3;i++){
                for(int j = 0;j<8-dataSenderController.songHighScoreList[i].ToString().Length;j++){
                    recordCode += EnCode('0');
                }
                for(int j = 0;j<dataSenderController.songHighScoreList[i].ToString().Length;j++){
                    recordCode += EnCode(dataSenderController.songHighScoreList[i].ToString()[j]);
                }
                
            }
            GUIUtility.systemCopyBuffer = recordCode;
        }
        
    }

    char EnCode(char a){
        return codeList[a-'0'];
    }

    int DeCode(char a){
        for(int i=0;i<10;i++){
            if(a == codeList[i]) return i;
        }
        return -1;
    }
}
