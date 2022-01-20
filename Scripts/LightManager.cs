using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class LightManager : MonoBehaviour
{
    public int powerUp;
    private bool hasCollide = false;

    [SerializeField] Light2D lightObject = null;
    [SerializeField] GameObject endTrigger;
    GameObject playerLightObject;
    GameObject newPlayerLightObject;
    GameObject enemyObj;
    GameObject gemObject;
    GameObject globalLight;
    GameObject audioSource;

    public void enableEndTrigger()
    {
        endTrigger = GameObject.Find("LevelEndTrigger");
        endTrigger.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void collectPowerUp(Collision2D collision1)
    {

        if (hasCollide == false)
        {
            hasCollide = true;
            powerUp += 1;
            lightObject.enabled = true;
            Debug.Log("PowerUp: " + powerUp);

            audioSource = GameObject.Find("GemSound");
            audioSource.GetComponent<AudioSource>().Play();

            Destroy(collision1.gameObject);
            enableEndTrigger();

            //if (endTrigger.activeInHierarchy == false)
            //{
            //    endTrigger.SetActive(true);
            //}
        }
    }

    public void collectScore(Collision2D collision1)
    {
        if(hasCollide == false)
        {
            hasCollide = true;
            playerLightObject = GameObject.FindWithTag("GuideLight1");
            if (playerLightObject != null)
            {
                if (!playerLightObject.GetComponent<Light2D>().enabled)
                {
                    playerLightObject.GetComponent<Light2D>().enabled = true;
                }
                else
                {
                    newPlayerLightObject = GameObject.FindWithTag("GuideLight2");
                    if(newPlayerLightObject != null)
                    {
                        newPlayerLightObject.GetComponent<Light2D>().enabled = true;
                    }
                }
            }
            audioSource = GameObject.Find("PearlSound");
            audioSource.GetComponent<AudioSource>().Play();
            Destroy(collision1.gameObject);
        }
    }

    public void enablePlayerLight(Collision2D collision1)
    {
        if (hasCollide == false)
        {
            hasCollide = true;
            playerLightObject = GameObject.FindWithTag("Emit_Light");
            if(playerLightObject != null)
            {
                playerLightObject.GetComponent<Light2D>().enabled = true;
            }
            audioSource = GameObject.Find("BluePearlSound");
            audioSource.GetComponent<AudioSource>().Play();
            Destroy(collision1.gameObject);
        }
    }

    public void enableKillerLight1(Collision2D collision)
    {
        if (hasCollide == false)
        {
            hasCollide = true;
            playerLightObject = GameObject.FindWithTag("KillerLight1");
            if (playerLightObject != null)
            {
                playerLightObject.GetComponent<Light2D>().enabled = true;
            }
            audioSource = GameObject.Find("PearlSound");
            audioSource.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
        }
    }

    public void enableKillerLight2(Collision2D collision)
    {
        if (hasCollide == false)
        {
            hasCollide = true;
            playerLightObject = GameObject.FindWithTag("KillerLight2");
            if (playerLightObject != null)
            {
                playerLightObject.GetComponent<Light2D>().enabled = true;
            }
            audioSource = GameObject.Find("PearlSound");
            audioSource.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);

            enemyObj = GameObject.FindWithTag("Enemy");
            if(enemyObj != null)
            {
                audioSource = GameObject.Find("EnemyKillSound");
                audioSource.GetComponent<AudioSource>().Play();
                audioSource = GameObject.Find("BossSound");
                audioSource.GetComponent<AudioSource>().Stop();
                DestroyEnemy(enemyObj);
            }
        }
    }

    public void DestroyEnemy(GameObject enemyObj)
    {
        Destroy(enemyObj.gameObject);
        gemObject = GameObject.FindWithTag("Collectible");
        gemObject.GetComponent<SpriteRenderer>().enabled = true;
        gemObject.GetComponent<BoxCollider2D>().enabled = true;

        gemObject = GameObject.FindWithTag("GemLight");
        gemObject.GetComponent<Light2D>().enabled = true;

        globalLight = GameObject.FindWithTag("GlobalLight");
        globalLight.GetComponent<Light2D>().intensity = 1.45f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        hasCollide = false;
    }
}
