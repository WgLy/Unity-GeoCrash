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
        gameTime = 0.00000f;
        dir = new Vector3(1, 1, 0);
        for(int i=0;i<1000;i++){
            Note tmp = new Note();
            tmp.t = i*0.3333333f;
            tmp.type = i%4+1;
            notes.Enqueue(tmp);
            turns.Enqueue(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        transform.position += dir * moveSpeed * Time.deltaTime;
        // if(Input.anyKeyDown){
        //     GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity); // copy a new prefeb
        // }

        // if(notes.Count > 0) if(gameTime >= notes.Peek().t){
        //     notes.Dequeue();
        //     GameObject newEffector = Instantiate(effectorPrefeb, transform.position, Quaternion.identity);
        // }

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
            if(turns.Peek().type == 1){
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position-new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 2){
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0,0.6f,0), 
                    Quaternion.identity
                );
            }
            if(turns.Peek().type == 3){
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
            if(turns.Peek().type == 4){
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(-0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f)
                );
            }
        }
    }

    void SwitchDirction(int dirIndex){
        if(dirIndex == 1){
            dir = new Vector3(1, -1, 0);
        }else if(dirIndex == 2){
            dir = new Vector3(-1, -1, 0);
        }else if(dirIndex == 3){
            dir = new Vector3(1, -1, 0);
        }else if(dirIndex == 4){
            dir = new Vector3(-1, -1, 0);
        }
        
    }
}
