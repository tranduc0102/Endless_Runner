using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private TextMeshProUGUI sumCoin;
    public AudioManager audio;

    private void Awake()
    {
        audio = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        //SaveGame.Load();
        sumCoin.text = "X"+GameManager.Instances.sumCoin.ToString();
        buttonPlay.onClick.AddListener(()=>playGame());
        buttonQuit.onClick.AddListener(()=>Quit());
    }

    // Update is called once per frame
    private void playGame()
    {
        GameManager.Instances.isPlay = true;
        SceneManager.LoadScene("GamePlay");
        audio.PlayMusicSource(audio.gamePlay);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
