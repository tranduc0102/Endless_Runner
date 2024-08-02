using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   #region singleton

   public static GameManager Instances;

   private void Awake()
   {
      if (Instances == null) {Instances = this;}
      else
      {
         Destroy(gameObject);
      }
      DontDestroyOnLoad(gameObject);
   }

   #endregion

 

   public int score = 0;
   public int hightScore;
   public float speed;
   public int coin = 0;
   public int sumCoin;
   public bool isPlay = true;
   public int live = 2;
   private float timeSpeed = 40f;
   private void Start()
   {
      SaveGame.Load();
   }
   private void Update()
   {
      timeSpeed -= Time.deltaTime;
      if (isPlay)
      {
         score += 1;
         if (timeSpeed <= 0)
         {
            speed = speed + 0.5f;
            timeSpeed = 40f;
         }
      }
   }

   public string Scorce()
   {
      return (score).ToString();
   }

   public string Coin()
   {
      return (coin).ToString();
   }

   public void ResetGame()
   {
      score = 0;
      speed = 5f;
      coin = 0;
      live = 2;
      isPlay = true;
      SaveGame.Save();
   }
   
}
