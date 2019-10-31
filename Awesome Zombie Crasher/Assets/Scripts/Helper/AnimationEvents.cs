using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    PlayerController playerController;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    public void ResetShooting()
    {
        playerController.canShoot = true;
        anim.Play("Idle");
    }

    public void CameraStartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
