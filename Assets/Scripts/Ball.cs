using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb;
    private float speed;

    AudioSource temp;

    void Awake()
    {
        speed = 10;
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (AllBtnFunctions.fastBall)
        {
            speed = 40;
            rb.gravityScale = 0.8f;
        }
        else if(!AllBtnFunctions.fastBall){
            speed = 10;
            rb.gravityScale = 0.001f;
        }

        rb.velocity = rb.velocity.normalized * speed;
        	
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "pp")
        {
            temp = GameObject.Find("PowerSound").GetComponent<AudioSource>();
            if (PlayerPrefs.GetInt("hasSound") == 0)
                temp.Play();
            BallLauncher.numOfBallToBeCreated += 1;

        }
        else if (col.gameObject.tag == "block")
        {
            
            temp = GameObject.Find("BubbleSound").GetComponent<AudioSource>();
            if (PlayerPrefs.GetInt("hasSound") == 0)
                temp.Play();

        }
        
    }
}
