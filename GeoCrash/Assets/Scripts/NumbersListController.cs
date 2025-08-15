using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersListController : MonoBehaviour
{
    public int showNumber;
    public List<NumberController> numbersList = new List<NumberController>();
    public DataSenderController dataSenderController;
    public MapGeneratorController mapGeneratorController;
    int tmp;
    // Start is called before the first frame update
    void Start()
    {
        
        dataSenderController = FindObjectOfType<DataSenderController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mapGeneratorController != null) showNumber = dataSenderController.songHighScoreList[mapGeneratorController.currentSongIndex];
        else showNumber = dataSenderController.finalScore;
        tmp = showNumber;
        for(int i=7;i>=0;i--){
            numbersList[i].numberIndex = tmp%10;
            tmp /= 10;
        }
    }
}
