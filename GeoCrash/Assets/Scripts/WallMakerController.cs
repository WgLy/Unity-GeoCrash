using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Math 類別位於 System 命名空間

public class WallMakerController : MonoBehaviour
{
    public GameObject wallPrefeb;
    public GameObject tapPrefeb;
    public SpriteRenderer spriteRenderer;
    public Vector2 dir;
    public float moveSpeed;
    public int BPM;
    public GameObject holdPrefeb;
    private Rigidbody2D rb;
    private bool moving;
    public DataSenderController dataSenderController;

    Queue<Note> notes = new Queue<Note>();
    Queue<Hold> holds = new Queue<Hold>();
    Queue<Note> turns = new Queue<Note>();
    public Queue<MovementStatus> correction = new Queue<MovementStatus>(); 
    public float gameTime;
    MovementStatus temp;

    public int shape;
    // 改變形狀
    public Sprite squareSprite;
    public Sprite triangleSprite;
    public Sprite hexagonSprite;
    public BoxCollider2D squareCollider;
    public PolygonCollider2D triangleCollider;
    public PolygonCollider2D hexagonCollider;

    public GameObject randomPointPrefeb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dataSenderController = FindObjectOfType<DataSenderController>();
        notes = new Queue<Note>(dataSenderController.notes);
        holds = new Queue<Hold>(dataSenderController.holds);
        turns = new Queue<Note>(dataSenderController.notes);
        BPM = dataSenderController.BPM;
        shape = dataSenderController.initialShape;
        // 初始校正
        dir = dataSenderController.InitialStatus.dir;
        transform.position = dataSenderController.InitialStatus.locate;
        transform.rotation = dataSenderController.InitialStatus.angle;
        rb.angularVelocity = dataSenderController.InitialStatus.spin;
        rb.velocity = dataSenderController.InitialStatus.dir;
        moveSpeed = dataSenderController.InitialStatus.speed;

        // 初始變形
        ChangeShape(shape);
        gameTime = 0.00000f-4*60.0f/BPM;
        moving = false; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameTime += Time.fixedDeltaTime; // 讓時間流動

        //transform.position += dir * moveSpeed * Time.deltaTime * ((gameTime>=-4*60/BPM)?1:0);
        if (gameTime >= -4 * 60.0f / BPM && moving == false){
            transform.position = new Vector3(0, 0, 0); 
            rb.velocity = dir.normalized * moveSpeed;
            moving = true;
        }else if(moving == false){
            rb.velocity = Vector2.zero; // 如果還沒到開始移動的時間，則速度為零
        }
        
        if(gameTime >= turns.Peek().t){ // 造牆
            if(turns.Peek().type == 1){ //down
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position-new Vector3(0,0.6f,0), 
                    Quaternion.identity * transform.rotation
                );
            }
            if(turns.Peek().type == 2){ //up
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0,0.6f,0), 
                    Quaternion.identity * transform.rotation
                );
            }
            if(turns.Peek().type == 3){ //right
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f) * transform.rotation
                );
            }
            if(turns.Peek().type == 4){ //left
                GameObject newWallPrefeb = Instantiate(
                    wallPrefeb, 
                    transform.position+new Vector3(-0.6f,0,0), 
                    Quaternion.Euler(0f, 0f, 90f) * transform.rotation
                );
            }
            if(turns.Peek().type == 5){ //tap
                GameObject newTapPrefeb = Instantiate(
                    tapPrefeb, 
                    transform.position+new Vector3(0,0,0), 
                    Quaternion.identity
                );
            }
            rb.velocity = rb.velocity.normalized * moveSpeed;
            
            temp.locate = transform.position;
            temp.angle = transform.rotation;
            temp.spin = rb.angularVelocity;
            temp.dir = rb.velocity;
            correction.Enqueue(temp);
            turns.Dequeue();
        }


        if(gameTime >= holds.Peek().t_begin){ // 造長條
            GameObject newHoldPrefeb = Instantiate(
                holdPrefeb, 
                transform.position, 
                Quaternion.identity
            );
        }
        if(gameTime >= holds.Peek().t_end){
            holds.Dequeue();
        }

        if(moving && UnityEngine.Random.Range(1, 500) == 1){ // 造雜點
        
            Vector2 randomPos2D = UnityEngine.Random.insideUnitCircle * 5f;
            GameObject newPointShape = Instantiate(
                randomPointPrefeb,
                randomPos2D + new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity
            );
        }
    }

    public void ChangeShape(int targetShape){ // 變形
        squareCollider.enabled = false;
        triangleCollider.enabled = false;
        hexagonCollider.enabled = false;
        if(targetShape == 1){
            spriteRenderer.sprite = squareSprite;
            squareCollider.enabled = true;
        }
        if(targetShape == 2){
            spriteRenderer.sprite = triangleSprite;
            triangleCollider.enabled = true;
        }
        if(targetShape == 3){
            spriteRenderer.sprite = hexagonSprite;
            hexagonCollider.enabled = true;
        }
    }
}

