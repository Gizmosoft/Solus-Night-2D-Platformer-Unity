using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public LightManager lightManager;
    GameObject audioSource;
    AudioSource audSource;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        audSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (horizontalMove != 0)
        {
            if (!audSource.isPlaying)
            {
                playWalkingSound();
            }
            //else
            //    audSource.Stop();
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));  // Abs used to keep Speed always positive

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void pauseGame() 
    {
        audioSource = GameObject.Find("DieSound");
        audioSource.GetComponent<AudioSource>().Play();
        StartCoroutine(GamePauser(1.7f));
    }
    public IEnumerator GamePauser(float pauseTime)
    {
        Debug.Log("Inside PauseGame()");
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Debug.Log("Done with my pause");
        PauseEnded();
    }

    public void PauseEnded()
    {
        restart();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Collectible"))
        {
            lightManager.collectPowerUp(collision);
        }

        if (collision.gameObject.tag.Equals("Pearl"))
        {
            lightManager.collectScore(collision);
        }

        if (collision.gameObject.tag.Equals("Player_Light"))
        {
            lightManager.enablePlayerLight(collision);
        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            pauseGame();
        }

        if (collision.gameObject.tag.Equals("LightPearl"))
        {
            lightManager.enableKillerLight1(collision);
        }

        if (collision.gameObject.tag.Equals("LightPearl2"))
        {
            lightManager.enableKillerLight2(collision);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void playWalkingSound()
    {
        audSource.Play();
    }
    void FixedUpdate()
    {
        // Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}
