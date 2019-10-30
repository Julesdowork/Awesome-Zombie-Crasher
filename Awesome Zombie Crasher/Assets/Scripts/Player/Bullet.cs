using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DeactivateGameObject();
        }
    }

    public void Move(float speed)
    {
        rb.AddForce(transform.forward.normalized * speed);
        Invoke("DeactivateGameObject", 5f);
    }

    private void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
