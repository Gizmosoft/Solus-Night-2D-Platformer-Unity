using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectibles : MonoBehaviour
{
    public int powerUp;

    public void OnTriggerEnter(Collider collision1)
    {
        if(collision1.tag == "Collectible")
        {
            powerUp += 1;
            Destroy(collision1.gameObject);
        }   
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("PowerUp: " + powerUp); 
    }
}
