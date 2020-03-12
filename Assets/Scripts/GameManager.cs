using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text highScoreText;

    [SerializeField]
    GameObject gameOver;

    [SerializeField]
    Text gpScore;

    [SerializeField]
    Text gpHighScore;

    [SerializeField]
    Transform camPos;

    [SerializeField]
    Transform[] walls;

    private int highScore;
    Camera cam;
    float height;
    float width;
    private float yPos, zPos;

    public static int score;
    public static bool gameStarted;




    // Use this for initialization
    void Start () {
        score = 0;
        highScore = PlayerPrefs.GetInt("highScore");
        gameStarted = true;
        highScoreText.text = highScore.ToString();

        //For Camera
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        yPos = walls[0].position.y;
        zPos = walls[0].position.z;

        walls[0].position = new Vector3(camPos.position.x - width/2,yPos,zPos);
        walls[1].position = new Vector3(camPos.position.x + width / 2, yPos, zPos);

    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString();

        if (!gameStarted)
        {
            StartCoroutine(gameOverApp());
        }

	}

    IEnumerator gameOverApp()
    {
        yield return new WaitForSeconds(0.8f);
        gameOver.SetActive(true);
        gpScore.text = score.ToString();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            gpHighScore.text = score.ToString();
        }
        else {
            gpHighScore.text = highScore.ToString();
        }
    }

}
