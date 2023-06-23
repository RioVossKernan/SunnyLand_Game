using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public float direction;
    public float distanceMax;
    private float distanceTraveled = 0;
    private CircleCollider2D circleCollider;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * direction, 0, 0) * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;

        if(distanceTraveled > distanceMax)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (collision.CompareTag("Player"))
        {
            playerController.isDead = true;
        }
    }
}
