using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
    public GameObject opossumPrefab;
    private float startX;
    public float flightRange;
    public int spawnRate;
    public float speed;
    private bool faceLeft = true;
    public SpriteRenderer sr;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        sr = GetComponent<SpriteRenderer>();

        InvokeRepeating("SpawnPrefab", 2, spawnRate);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > flightRange + startX || transform.position.x < startX - flightRange)
        {
            faceLeft = !faceLeft;
        }
        if (!isDead)
        {
            if (faceLeft)
            {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            }
        }


        FlipSprite();
    }
    void SpawnPrefab()
    {
        Instantiate(opossumPrefab, transform.position + new Vector3(0,-1,0), new Quaternion(0, 0, 0, 0)) ;
    }
    void FlipSprite()
    {
        if (!faceLeft)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }   
}
