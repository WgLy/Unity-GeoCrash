using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject effectorPrefeb;

    struct Note{
        public float t;
        public int type;
    };
    Queue<Note> notes = new Queue<Note>();
    public float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0.00000f;
        for(int i=0;i<100;i++){
            Note tmp = new Note();
            tmp.t = i*0.3333333f;
            tmp.type = 1;
            notes.Enqueue(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        // if(Input.anyKeyDown){
        //     GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity); // copy a new prefeb
        // }

        if(notes.Count > 0) if(gameTime >= notes.Peek().t){
            notes.Dequeue();
            GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity);
        }
    }
}
