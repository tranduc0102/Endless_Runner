using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Scorce;
    [SerializeField] private TextMeshProUGUI overScorce;
    [SerializeField] private TextMeshProUGUI overHightScorce;
    [SerializeField] private TextMeshProUGUI Coin;
    [SerializeField] private TextMeshProUGUI overCoin;
    [SerializeField] private TextMeshProUGUI Live;
    [SerializeField] private Button Pause;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Button Play;
    [SerializeField] private Button RePlay;
    [SerializeField] private Button Home;
    [SerializeField] private Button RePlay1;
    [SerializeField] private Button Home1;

    public AudioManager audio;
    

    private void Start()
    {
        Pause.onClick.AddListener(buttonPause);
        Play.onClick.AddListener(buttonPlay);
        RePlay.onClick.AddListener(buttonRePlay);
        Home.onClick.AddListener(buttonHome);
        RePlay1.onClick.AddListener(buttonRePlay);
        Home1.onClick.AddListener(buttonHome);
    }

    private void Update()
    {
        Scorce.text = GameManager.Instances.Scorce();
        overScorce.text = "Score: "+GameManager.Instances.Scorce();
        overHightScorce.text = "Hight Score: "+GameManager.Instances.hightScore.ToString();
        
        Coin.text = "x" + GameManager.Instances.Coin();
        overCoin.text = "+" + GameManager.Instances.Coin();
        Live.text = "x" + GameManager.Instances.live.ToString();
        if (GameManager.Instances.live == 0)
        {
            Invoke("GameOver",0.5f);
        }
    }

    private void buttonPause()
    {
        Time.timeScale = 0f;
        Panel.SetActive(true);
        GameManager.Instances.isPlay = false;
        Debug.Log("tam dung");
        SaveGame.Save();
        //SaveGame.Load();
    }

    private void buttonPlay()
    {
        Time.timeScale = 1f;
        Panel.SetActive(false);
        GameManager.Instances.isPlay = true;
        Debug.Log("choi tiep");
    }

    private void buttonRePlay()
    {
        Time.timeScale = 1f;
        GameManager.Instances.ResetGame();
        SceneManager.LoadScene("GamePlay");
        Debug.Log("load lai game");
    }

    private void buttonHome()
    {
        Time.timeScale = 1f;
        GameManager.Instances.ResetGame();
        GameManager.Instances.isPlay = false;
        Debug.Log("quay ve home");
        SceneManager.LoadScene("Before");
        audio = GameObject.Find("GameManager").GetComponent<AudioManager>();
        audio.PlayMusicSource(audio.musicLobby);
    }

    private void GameOver()
    {
        gameOver.gameObject.SetActive(true);
    }
   
}
