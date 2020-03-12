using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour {

    [SerializeField]
    Ball ballPrefab;

    [SerializeField]
    DottedLine dl;

    [SerializeField]
    GameObject ballParent;

    [SerializeField]
    CircleCollider2D circle;

    [SerializeField]
    Transform cameraPos;

    [SerializeField]
    GameObject hand;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private List<Ball> spwanBallList = new List<Ball>();
    private BoxSpwanner boxSpwanner;
    private int ballReady;
    private float randomPosX;
    private float circleRad;
    private float posLeft,posRight;

    public static int numOfBallToBeCreated;
    public static bool fastForward;
    public static bool readyToShoot;


    Camera cam;
    float height;
    float width;

    void Awake()
    {
        boxSpwanner = FindObjectOfType<BoxSpwanner>();
        readyToShoot = true;
        numOfBallToBeCreated = 0;
        createBallz();
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        circleRad = circle.radius;
        posLeft = cameraPos.position.x - width / 2 + circleRad;
        posRight = cameraPos.position.x + width / 2 - circleRad;
    }


    

    public void returnBall()
    {
        ballReady++;
        if (ballReady == spwanBallList.Count)
        {
            //ballReady += numOfBallToBeCreated;
            if (numOfBallToBeCreated > 0)
            {
                for (int i = 0; i < numOfBallToBeCreated; i++)
                    createBallz();

                numOfBallToBeCreated = 0;
            }
            boxSpwanner.spwanBlocks();


            randomPosX = UnityEngine.Random.Range(posLeft, posRight);
            Vector3 pos = transform.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(randomPosX, pos.y, pos.z), 100 * Time.deltaTime);


        }


    }

    void createBallz()
    {
        var ball = Instantiate(ballPrefab);
        ball.transform.parent = ballParent.transform;
        ball.gameObject.SetActive(false);
        spwanBallList.Add(ball);
        ballReady++;
    }

    // Update is called once per frame
    void Update () {

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back*(-10);

        //Checking is game started or not.
        if (GameManager.gameStarted)
        {
            if (Input.GetMouseButtonDown(0) && readyToShoot)
            {
                startDrag(worldPosition);

            }
            else if (Input.GetMouseButton(0) && readyToShoot)
            {
                continueDrag(worldPosition);
            }
            else if (Input.GetMouseButtonUp(0) && readyToShoot)
            {
                endDrag();
                readyToShoot = false;

            }
        }



    }


    void FixedUpdate()
    {
        if (ballReady == spwanBallList.Count)
        {
            readyToShoot = true;
            fastForward = false;
            AllBtnFunctions.fastBall = false;
            AllBtnFunctions.delta = 0;
        }

    }

    //This is called when user releases the launcher
    private void endDrag()
    {
        if (endPosition - startPosition != Vector3.zero)
        {
            StartCoroutine(LaunchBall());
            fastForward = true;
        }
        
    }

    private IEnumerator LaunchBall()
    {
        Vector3 direction = endPosition - startPosition;
        direction.Normalize();
        ballReady = 0;

        foreach (var ball in spwanBallList)
        {
            
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);
            yield return new WaitForSeconds(0.04f); // 0.04f
            
        }

        

    }

    private void continueDrag(Vector3 worldPosition)
    {
        if (hand.activeSelf)
        {
            hand.SetActive(false);
        }
        endPosition = worldPosition;
        Vector3 direction = endPosition - startPosition;

        direction *= 1.5f;

        dl.DrawDottedLine(transform.position, transform.position - direction);



    }

    private void startDrag(Vector3 worldPosition)
    {
        
        startPosition = worldPosition;
        
    }
}
