using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float speed = 5f; // 可以調整速度
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1, 1).normalized * speed; // 初始向右上移動
    }

    // 不需要 FixedUpdate 來持續設置速度
    // 讓物理引擎處理反彈
}