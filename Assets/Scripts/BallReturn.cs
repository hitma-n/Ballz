using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour {

    private BallLauncher bl;

    void Awake()
    {
        bl = FindObjectOfType<BallLauncher>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.001f;
        col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 10;
        col.gameObject.SetActive(false);
        bl.returnBall();
        
    }

}
