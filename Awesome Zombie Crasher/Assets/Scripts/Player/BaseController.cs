using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Vector3 speed;
    public float xSpeed = 8f, zSpeed = 15f;
    public float accelerateForce = 15f, decelerationForce = -10f;
    public float lowSoundPitch, normalSoundPitch, highSoundPitch;
    public AudioClip engineOnSound, engineOffSound;

    protected float rotationSpeed = 10f;
    protected float maxAngle = 10f;

    private bool isSlow;

    AudioSource soundManager;

    void Awake()
    {
        speed = new Vector3(0, 0, zSpeed);
        soundManager = GetComponent<AudioSource>();
    }

    protected void MoveLeft()
    {
        speed = new Vector3(-xSpeed / 2f, 0f, speed.z);  // divide by 2 so we don't go too fast
    }

    protected void MoveRight()
    {
        speed = new Vector3(xSpeed / 2f, 0f, speed.z);  // divide by 2 so we don't go too fast
    }

    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal()
    {
        if (isSlow)
        {
            isSlow = false;

            soundManager.Stop();
            soundManager.clip = engineOnSound;
            soundManager.volume = 0.3f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, zSpeed);
    }

    protected void MoveSlow()
    {
        if (!isSlow)
        {
            isSlow = true;

            soundManager.Stop();
            soundManager.clip = engineOffSound;
            soundManager.volume = 0.5f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, decelerationForce);
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0f, accelerateForce);
    }
}
