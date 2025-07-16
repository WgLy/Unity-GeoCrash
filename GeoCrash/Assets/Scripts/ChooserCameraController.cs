using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooserCameraController : MonoBehaviour
{
    public Transform chooser;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            3,
            chooser.position.y+3f,
            -10
        );
    }
}
