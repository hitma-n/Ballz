using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpwanner : MonoBehaviour {

    [SerializeField]
    Block blockPrefab;

    [SerializeField]
    GameObject powerUpPrefab;

    [SerializeField]
    Transform blockSpare;

    [SerializeField]
    Transform blockActive;

    [SerializeField]
    Transform powerInactive;

    [SerializeField]
    ParticleSystem desParticle;

    private int playWidth = 7;
    private float distanceBetweenBlocks = 0.7f;
    private int rowsSpwaned;
    private List<GameObject> powerUps = new List<GameObject>();
    private Block tempobject;

    public static Stack<Block> blockStack;
    public static Stack<GameObject> powerStack;
    public static Stack<ParticleSystem> particles;

    void Update()
    {

        
    }

    void OnEnable()
    {
        blockStack = new Stack<Block>();
        powerStack = new Stack<GameObject>();
        particles = new Stack<ParticleSystem>();

        for (int i = 0; i < 150; i++)
        {
            tempobject = Instantiate(blockPrefab, transform.position, Quaternion.identity);
            tempobject.transform.SetParent(blockSpare);
            tempobject.gameObject.SetActive(false);
            blockStack.Push(tempobject);
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject temp = Instantiate(powerUpPrefab, transform.position,Quaternion.identity);
            temp.transform.SetParent(powerInactive.transform);
            temp.gameObject.SetActive(false);
            powerStack.Push(temp);
        }

        for (int i = 0; i < 100; i++)
        {
            ParticleSystem temp = Instantiate(desParticle, transform.position, Quaternion.identity);
            temp.gameObject.SetActive(false);
            particles.Push(temp);
        }

        for (int i = 0; i < 1; i++)
        {
            spwanBlocks();
        }

        

    }

    public void spwanBlocks()
    {
        AudioSource tempSound = GameObject.Find("PlingSound").GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("hasSound") == 0)
            tempSound.Play();

        for (int i = 0; i < blockActive.childCount; i++)
        {
            blockActive.GetChild(i).transform.position += Vector3.down * distanceBetweenBlocks;
        }
 

        foreach (GameObject obj in powerUps)
        {
            if (obj != null)
            {
                obj.transform.position += Vector3.down * distanceBetweenBlocks;
            }
        }

        for (int i=0;i<playWidth;i++)
        {

            if (UnityEngine.Random.Range(0, 100) <= 70)
            {
                if (UnityEngine.Random.Range(0, 100) <= 80)
                {
                    //var temp = Instantiate(blockPrefab, GetPos(i), Quaternion.identity);
                    var temp = blockStack.Pop();
                    temp.gameObject.transform.position = GetPos(i);
                    int hits = UnityEngine.Random.Range(1, 3) + rowsSpwaned;
                    temp.setHit(hits);
                    temp.transform.SetParent(blockActive);
                    temp.gameObject.SetActive(true);

                }
            }
            else {

                //This is for the extra ball powerup
                if(UnityEngine.Random.Range(0, 100) <= 40)
                {
                    GameObject temp = powerStack.Pop();
                    temp.transform.position = GetPos(i);
                    temp.transform.SetParent(blockActive.transform);
                    temp.SetActive(true);

                }
            }
           
        }

        rowsSpwaned++;

        
    }

    private Vector3 GetPos(int i)
    {
        Vector3 pos = transform.position;
        pos += Vector3.right * i * distanceBetweenBlocks;
        return pos;

    }
}
