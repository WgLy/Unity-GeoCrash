using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> numberPicture = new List<Sprite>();
    public int numberIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = numberPicture[numberIndex];
    }
}
