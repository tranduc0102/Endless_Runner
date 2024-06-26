using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector3.left*GameManager.Instances.speed*Time.deltaTime);
        if (transform.position.x <= -16.7f)
        {
            transform.position = new Vector3(28.33f,0.22f,0f);
        }
    }
}
