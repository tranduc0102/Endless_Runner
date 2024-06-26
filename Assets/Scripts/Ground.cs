using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ground : MonoBehaviour
{
    #region private value
    [SerializeField] private List<GameObject> listLevel = new List<GameObject>();
    [SerializeField] private Player _player;
    private Vector3 lastEndPoint;
    private GameManager GM;
    private GameObject obj;
    #endregion

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        lastEndPoint = new Vector3(-20f, -5.89f, 0f);
        for (int i = 0; i < 2; i++)
        {
            Spawn();
        }
    }

    private void Update()
    {
        MoveMap();
        if (Mathf.Abs(_player.transform.position.x-lastEndPoint.x)<=25f)
        {
            Spawn();
        }
    }

    private void MoveMap()
    {
        Transform obj = transform.Find("Holder");
        foreach (Transform child in obj)
        {
            if (child.name != "Warming" && child.name != "Arrow")
            {
                child.position += Vector3.left * GM.speed* Time.deltaTime;   
            }
        }
        lastEndPoint+=Vector3.left * GM.speed* Time.deltaTime;
    }

    private void Spawn()
    {
        GameObject nextLevel = listLevel[Random.Range(0, listLevel.Count)];
        GameObject spawnedLevel = Spawn(nextLevel,lastEndPoint);
        activeCoin(spawnedLevel.transform,"Coin");
        //activeCoin(spawnedLevel.transform,"Obstacles");
        lastEndPoint = spawnedLevel.transform.Find("EndPoint").position;
    }

    private GameObject Spawn(GameObject gameObject, Vector3 endPoint)
    {
        Vector3 position = new Vector3(endPoint.x, -2.98f, 0f);
        GameObject nextLevel = Pooling.Instance.Spawner(gameObject,position,Quaternion.identity);
        return nextLevel;
    }

    private void activeCoin(Transform obj, string name)
    {
        foreach (Transform child in obj)
        {
            if (child.name.Contains(name))
            {
                int x = Random.Range(0, 2);
                if (x == 0)
                {
                    child.gameObject.SetActive(false);
                    Debug.Log("coin");
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
