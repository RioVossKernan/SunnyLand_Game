using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public GameObject projectilePrebab;
    public float shootRate;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectiles",2,shootRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProjectiles()
    {
        if (!isDead)
        {
            Instantiate(projectilePrebab, gameObject.transform);
        }
    }
    
}
