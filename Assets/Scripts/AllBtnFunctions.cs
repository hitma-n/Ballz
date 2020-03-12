using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllBtnFunctions : MonoBehaviour {

    [SerializeField]
    Button restart;

    [SerializeField]
    Button home;

    //[SerializeField]
    //Button facebook;

    [SerializeField]
    Button pauseBtn;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    Button resume;

    [SerializeField]
    Button homeBtnPause;

    [SerializeField]
    Button restartBtnPP;

    [SerializeField]
    Button fastForward;

    //[SerializeField]
    //Button leaderStart;


    public static bool fastBall;
    public static float delta;
    private AudioSource temp;

    // Use this for initialization
    void Start () {
        delta = 0;
        restart.onClick.AddListener(restartFunctn);
        home.onClick.AddListener(homeFunctn);
        //facebook.onClick.AddListener(facebookFunctn);
        restartBtnPP.onClick.AddListener(restartFunctn);
        homeBtnPause.onClick.AddListener(homeFunctn);
        resume.onClick.AddListener(resumeFunctn);
        pauseBtn.onClick.AddListener(pauseFunctn);

        fastForward.onClick.AddListener(setVars);

        temp = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        //leaderStart.onClick.AddListener(PlayServices.OnShowLeaderBoard);

    }

    void FixedUpdate()
    {
        if (BallLauncher.fastForward)
        {
            delta += Time.deltaTime;
            if (delta > 8)
                fastForward.gameObject.SetActive(true);
        }

        if(BallLauncher.readyToShoot)
            fastForward.gameObject.SetActive(false);

        if(fastBall)
            fastForward.gameObject.SetActive(false);

    }

    void restartFunctn()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    

    void homeFunctn()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    void resumeFunctn()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void pauseFunctn()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        //scorePP.text = GameManager.score.ToString();
        //bestPP.text = PlayerPrefs.GetInt("highScore").ToString();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        

    }

    void setVars()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        fastBall = true;
        Debug.Log("Clicked");
        //StartCoroutine(switchOf());
        
    }

    IEnumerator switchOf()
    {
        yield return new WaitForSeconds(0.1f);
        fastForward.gameObject.SetActive(false);
    }

}
