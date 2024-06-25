using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacles : MonoBehaviour
{
   #region private value
   [SerializeField]private GameObject warming;
   [SerializeField]private GameObject arrow;
   private float timeSpawn;
   private float coolDown;
   private Vector2 PositionArrow;
   private GameObject currentWarming;
   private bool warmingSpawned = false;
   private GameManager GM;
   #endregion

   private void Awake()
   {
      GM = FindObjectOfType<GameManager>();
      timeSpawn = Random.Range(10, 20f);
   }

   private void Update()
   {
      timeSpawn -= Time.deltaTime;

      if (timeSpawn <= 0 && !warmingSpawned)
      {
         SpawnWarming();
      }

      if (coolDown > 0)
      {
         coolDown -= Time.deltaTime;
         if (coolDown <= 0 && warmingSpawned)
         {
            Pooling.Instance.Despawn(currentWarming);
            SpawnArrow();
            warmingSpawned = false;
         }
      }
   }

   private void SpawnWarming()
   {
      if (timeSpawn <= 0)
      {
         Vector2 position = new Vector2(10.5f, Random.Range(-2.3f, 6.9f));
         PositionArrow = position;
         currentWarming = Pooling.Instance.Spawner(warming, position, Quaternion.identity);
         coolDown = Random.Range(1f,6f);
         warmingSpawned = true;
      }
   }

   private void SpawnArrow()
   {
      GameObject newArrow = Pooling.Instance.Spawner(arrow,PositionArrow,Quaternion.identity);
      newArrow.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
      newArrow.GetComponent<Rigidbody2D>().velocity = Vector2.left * (GM.speed+7f); 
      timeSpawn = Random.Range(10, 20f);
   }
}
