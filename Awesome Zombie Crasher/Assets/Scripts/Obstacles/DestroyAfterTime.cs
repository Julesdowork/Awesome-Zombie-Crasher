using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timer = 3f;

    void Start()
    {
        Invoke("Deactivate", timer);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
