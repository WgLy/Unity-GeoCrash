using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Math 類別位於 System 命名空間

public class CharacterController : MonoBehaviour
{
    public GameObject perfectEffectorPrefeb;
    public GameObject goodEffectorPrefeb;
    public GameObject missEffectorPrefeb;
    public GameObject wallPrefeb;
    public SpriteRenderer spriteRenderer;
    public float velocity;
    public Vector3 dir;
    public int moveSpeed;
    public int BPM;

    struct Note{
        public float t;
        public int type;
    };
    Queue<Note> notes = new Queue<Note>();
    Queue<Note> turns = new Queue<Note>();
    public float gameTime;
    // Start is called before the first frame update
    public float perfectLimit;
    public float goodLimit;

    void Start()
    {
        gameTime = 0.00000f-8*60/BPM;
        dir = new Vector3(1, 1, 0);
//            2
//        4       3
//            1
        AddNote(0, 0.0f, 1);
        AddNote(0, 1.5f, 2);
        AddNote(0, 3.0f, 1);
        AddNote(0, 3.5f, 2);
        AddNote(0, 4.0f, 1);
        AddNote(0, 5.5f, 2);
        AddNote(0, 7.0f, 1);
        AddNote(0, 7.5f, 2);
        AddNote(1, 0.0f, 1);
        AddNote(1, 1.5f, 2);
        AddNote(1, 3.0f, 1);
        AddNote(1, 3.5f, 2);
        AddNote(1, 6.0f, 1);
        AddNote(1, 6.5f, 2);
        AddNote(1, 7.0f, 1);
        AddNote(2, 0.0f, 2);
        AddNote(2, 1.5f, 1);
        AddNote(2, 3.0f, 2);
        AddNote(2, 4.0f, 1);
        AddNote(2, 5.0f, 2);
        AddNote(2, 5.5f, 1);
        AddNote(2, 6.5f, 2);
        AddNote(2, 7.5f, 1);
        AddNote(3, 0.0f, 2);
        AddNote(3, 2.0f, 1);
        AddNote(3, 3.0f, 2);
        AddNote(3, 4.0f, 1);
        AddNote(3, 6.0f, 2);
        AddNote(3, 6.5f, 1);
        AddNote(3, 7.0f, 2);
        AddNote(3, 7.5f, 1);

        AddNote(4, 0.0f, 3);
        AddNote(5, 0.0f, 4);
        AddNote(6, 0.0f, 3);
        AddNote(7, 0.0f, 2);
        AddNote(7, 2.0f, 1);
        AddNote(7, 4.0f, 2);
        AddNote(7, 4.5f, 1);
        AddNote(7, 5.0f, 2);
        AddNote(7, 5.5f, 1);
        AddNote(7, 6.0f, 2);
        AddNote(7, 6.5f, 1);
        AddNote(7, 7.0f, 2);
        AddNote(7, 7.5f, 1);


        // for(int i=0;i<1000;i++){
        //     Note tmp = new Note();
        //     tmp.t = i*60.0f/BPM;
        //     tmp.type = i%4+1;
        //     notes.Enqueue(tmp);
        //     turns.Enqueue(tmp);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        transform.position += dir * moveSpeed * Time.deltaTime * ((gameTime>=-4*60/BPM)?1:0);
        // if(Input.anyKeyDown){
        //     GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity); // copy a new prefeb
        // }

        // if(notes.Count > 0) if(gameTime >= notes.Peek().t){
        //     notes.Dequeue();
        //     GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity);
        // }

    /*  wall maker function
        if(gameTime >= turns.Peek().t){
            turns.Dequeue();
            SwitchDirction(turns.Peek().type);
            if(turns.Peek().type == 1 || Input.GetKeyDown(KeyCode.A)){ //down
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position-new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 2 || Input.GetKeyDown(KeyCode.S)){ //up
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 3 || Input.GetKeyDown(KeyCode.D)){ //right
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
            if(turns.Peek().type == 4 || Input.GetKeyDown(KeyCode.F)){ //left
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(-0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
        }
        */

        if(Input.anyKeyDown){
            if( Math.Abs(notes.Peek().t - gameTime) <= perfectLimit ){
                notes.Dequeue();
                GameObject newEffector = Instantiate(perfectEffectorPrefeb, transform.position, Quaternion.identity);
            }else if( Math.Abs(notes.Peek().t - gameTime) <= goodLimit ){
                notes.Dequeue();
                GameObject newEffector = Instantiate(goodEffectorPrefeb, transform.position, Quaternion.identity);
            }
        }
        if(gameTime > notes.Peek().t + goodLimit){
            notes.Dequeue();
            GameObject newEffector = Instantiate(missEffectorPrefeb, transform.position, Quaternion.identity);
        }
        

        
        if(gameTime >= turns.Peek().t){
            turns.Dequeue();
            SwitchDirction(turns.Peek().type);
            if(turns.Peek().type == 1 || Input.GetKeyDown(KeyCode.A)){ //down
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position-new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 2 || Input.GetKeyDown(KeyCode.S)){ //up
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 3 || Input.GetKeyDown(KeyCode.D)){ //right
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
            if(turns.Peek().type == 4 || Input.GetKeyDown(KeyCode.F)){ //left
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(-0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
        }
    }

    void SwitchDirction(int dirIndex){
        if(dirIndex == 1){ //down
            if(dir.y==1) Debug.Log("ERROR");
            dir.y *= -1;
        }else if(dirIndex == 2){ //up
            if(dir.y==-1) Debug.Log("ERROR");
            dir.y *= -1;
        }else if(dirIndex == 3){ //right
            if(dir.x==-1) Debug.Log("ERROR");
            dir.x *= -1;
        }else if(dirIndex == 4){ //left
            if(dir.x==1) Debug.Log("ERROR");
            dir.x *= -1;
        }
        
    }

    void AddNote(int p,float t,int type){
        Note tmp = new Note();
        tmp.t = (t+p*8)*60.0f/BPM;
        tmp.type = type;
        notes.Enqueue(tmp);
        turns.Enqueue(tmp);
    }
}
