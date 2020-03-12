using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Block size was previously 36

public class Block : MonoBehaviour {

    [SerializeField]
    GameObject particlePrefab;

    SpriteRenderer sr;
    TextMeshPro tmp;
    int hitRemain;
    float distanceBetweenBlocks = 0.7f;

    // Use this for initialization
    void Awake () {
        hitRemain = 0;
        sr = GetComponent<SpriteRenderer>();
        tmp = GetComponentInChildren<TextMeshPro>();
        //updateVisualState();
	}

    void Update()
    {

        if (transform.position.y <= -3.79f)
        {
            if(GameManager.gameStarted)
                Handheld.Vibrate();
            GameManager.gameStarted = false;
            
        }
    }
    

    void updateVisualState()
    {
        tmp.SetText(hitRemain.ToString());

        if (hitRemain > 0 && hitRemain <= 5)
        {
            sr.color = new Color32(251, 216, 40, 255);
        }

        else if (hitRemain > 5 && hitRemain <= 10)
        {
            sr.color = Color32.Lerp(new Color32(251, 216, 40, 255), new Color32(153, 227, 50, 255), hitRemain / 10f); //yellow to Green

        }
        else if (hitRemain > 10 && hitRemain <= 20)
            sr.color = Color32.Lerp(new Color32(237, 31, 91, 255), new Color32(153, 227, 50, 255), hitRemain / 10f); //Red to green
        else if (hitRemain > 20 && hitRemain <= 30)
            sr.color = new Color32(237, 31, 91, 255); //Red
        else if (hitRemain > 30 && hitRemain <= 40)
            sr.color = Color32.Lerp(new Color32(195, 34, 130, 255), new Color32(237, 31, 91, 255), hitRemain / 10f); //Pink to Red
        else if (hitRemain > 40 && hitRemain <= 50)
            sr.color = Color32.Lerp(new Color32(19, 116, 185, 255), new Color32(195, 34, 130, 255), hitRemain / 10f); //Blue to Pink
        else if (hitRemain > 50)
            sr.color = new Color32(19, 116, 185, 255);


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        hitRemain--;
        if (hitRemain > 0)
        {
            //gameObject.GetComponent<Animator>().Play("block");
            updateVisualState();
        }
        else
        {
           
            GameManager.score += 1;
            
            //Setting Up the particle system;
            ParticleSystem part = BoxSpwanner.particles.Pop();
            part.gameObject.SetActive(true);
            part.gameObject.transform.position = transform.position;
            part.Play();

            transform.SetParent(GameObject.Find("BlocksInactive").transform);
            BoxSpwanner.blockStack.Push(gameObject.GetComponent<Block>()); 
            StartCoroutine(destroyObj());

        }
    }


    IEnumerator destroyObj()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
        
    }

    internal void setHit(int hits)
    {
        hitRemain = hits;
        updateVisualState();
    }
}
