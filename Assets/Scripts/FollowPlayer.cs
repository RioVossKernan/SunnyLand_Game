using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followObject;
    public Transform objectTransform;
    // Start is called before the first frame update
    void Start()
    {
        followObject = GameObject.Find("Player");
        objectTransform = followObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectTransform.position.x > 0)
        {
            transform.position = new Vector3(objectTransform.position.x, transform.position.y, transform.position.z);
        }
    }
}
