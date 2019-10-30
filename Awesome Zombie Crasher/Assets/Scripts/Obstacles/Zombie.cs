using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject bloodFXPrefab;

    private float speed;
    private bool isAlive;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;
        speed = Random.Range(1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            rb.velocity = new Vector3(0, 0, -speed);
        }

        if (transform.position.y < -10f)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(bloodFXPrefab, transform.position, Quaternion.identity);
            Invoke("Deactivate", 3f);
            // INCREASE SCORE
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;

        rb.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle");

        transform.rotation = Quaternion.Euler(90f, 0, 0);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
