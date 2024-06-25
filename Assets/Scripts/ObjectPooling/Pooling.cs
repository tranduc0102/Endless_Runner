using System.Collections.Generic;
using UnityEngine;

public class Pooling : Spawn
{
    private static Pooling instance;

    public static Pooling Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Pooling>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}