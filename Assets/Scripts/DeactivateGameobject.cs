using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameobject : MonoBehaviour
{
    public float timer = 0;

    float timeReamaining;
    //SpriteRenderer spriteRenderer;

    private void Awake()
    {
        timeReamaining = timer;
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Debug.Log("Time remaining: "+timeReamaining);
        if (timeReamaining <= 0)
        {
            timeReamaining = timer;
            //Debug.Log("Timer: " + timer);
            gameObject.SetActive(false);

        }


        timeReamaining -= Time.deltaTime;
        

        
    }
}
