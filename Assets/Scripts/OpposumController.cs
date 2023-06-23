using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpposumController : MonoBehaviour
{
    private SpriteRenderer sr;
    private int direction = 1;
    public float startSpeed;
    private float speed;
    public LayerMask layerPlayer;
    public LayerMask layerGround;
    public float speedAmplifier;
    public bool pauseMovement;
    private BoxCollider2D bc2d;
    public float turnDistance;
    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed * direction;


        FlipSprite();
        if (Physics2D.Raycast(transform.position,new Vector2 (direction,0), turnDistance,layerGround))
        {
            Turn();
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(1, 0) * direction, 5f, layerPlayer))
        {
            speed = startSpeed * speedAmplifier;
        }
      
    }

    void FlipSprite()
    {
        if (direction == -1)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    void Turn()
    {
        if (direction == 1)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        speed = startSpeed;

    }

  
}
