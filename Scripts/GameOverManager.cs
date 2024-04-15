using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{


    public PlayerCollision playerCollis;
    public NextLevelController levelController;

    private float m_JumpForce = 1400f;
    private float m_MovementSmoothing = 300f;
    private Vector3 m_Velocity = Vector3.zero;

    void Start()
    {


    }

    public void GameOver(NextLevelController levelController)
    {


        StartCoroutine(LiftPlayer(levelController));



    }

    IEnumerator LiftPlayer(NextLevelController levelController)
    {
        //playerCollis.gameObject.SetActive(false);
        playerCollis.movement.enabled = false;

        //remove the collider
        levelController.GetComponent<Collider2D>().enabled = false;

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(-1f, playerCollis.GetComponent<Rigidbody2D>().velocity.y);
        // And then smoothing it out and applying it to the character
        playerCollis.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(playerCollis.GetComponent<Rigidbody2D>().velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        playerCollis.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, m_JumpForce));

        //wait
        yield return new WaitForSeconds(1);


        SceneManager.LoadScene("NextLevel");

        //stop blinking and turn on collisions
        playerCollis.movement.enabled = true;


    }



}
