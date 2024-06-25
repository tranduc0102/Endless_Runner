using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("prefabs"))
        {
            Transform parentObject = other.gameObject.transform.parent;
            if (parentObject != null)
            {
                Pooling.Instance.Despawn(parentObject.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Arrow"))
        {
            Pooling.Instance.Despawn(other.gameObject);
        }
    }
}