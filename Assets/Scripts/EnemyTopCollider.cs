using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTopCollider : MonoBehaviour
{
    public GameObject parent;
    public GameObject explosionPrefab;
    public PlayerController playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Player"))
        {
            Instantiate(explosionPrefab,transform.position, new Quaternion(0,0,0,0)) ;
            Destroy(parent);
            playerController.bounce();
        }
    }
}
