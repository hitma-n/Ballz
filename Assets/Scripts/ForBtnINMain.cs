using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForBtnINMain : MonoBehaviour {

    [SerializeField]
    Button play;

    [SerializeField]
    Button rate;

    [SerializeField]
    Button fbBtn;

    [SerializeField]
    Button leaderBtn;

    [SerializeField]
    Button soundBtn;

    private AudioSource temp;
    private Image image;

    // Use this for initialization
    void Start () {
        fbBtn.onClick.AddListener(facebookFunctn);
        play.onClick.AddListener(playGame);
        leaderBtn.onClick.AddListener(leaderBtnFunction);
        soundBtn.onClick.AddListener(soundVars);
        rate.onClick.AddListener(rateGame);
        temp = GameObject.Find("ClickSound").GetComponent<AudioSource>();
       
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("hasSound") == 1)
        {
            image = soundBtn.gameObject.GetComponent<Image>();
            Color32 c = image.color;
            c.a = 87;
            image.color = c;

        }
        else
        {
            image = soundBtn.gameObject.GetComponent<Image>();
            Color32 c = image.color;
            c.a = 255;
            image.color = c;
        }
    }

    void playGame()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        SceneManager.LoadScene("SampleScene");
        
    }

    void rateGame()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.pineappstudios.ballz&hl=en");
        
    }

    void facebookFunctn()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        //WWW www = new WWW("fb://page/1291717037625415");
        //StartCoroutine(WaitForRequest(www));
        Application.OpenURL("fb://page/1291717037625415");

    }


    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("Sucess!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error + " Opening Safari...");
            //error. Open normal address
            Application.OpenURL("https://facebook.com/pineappgamess");

        }
    }

    void leaderBtnFunction()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();
        PlayServices.OnShowLeaderBoard();
        
    }

    void soundVars()
    {
        if (PlayerPrefs.GetInt("hasSound") == 0)
            temp.Play();

        if (PlayerPrefs.GetInt("hasSound") == 0)
        {
            PlayerPrefs.SetInt("hasSound", 1);
        }
        else {
            PlayerPrefs.SetInt("hasSound", 0);
        }
    }

}
