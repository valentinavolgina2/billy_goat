using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private AnimatorControllers animatorControllers;

    [SerializeField]
    private Rigidbody2D mRigidbody;

    public CharacterController2D controller;
    float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    //bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal")); to show in consol
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            FindObjectOfType<AudioManager>().Play("Jump");
        }

       /* if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            
        }
        else if (Input.GetButtonUp("Crouch"))
        {
           
            crouch = false;
        }*/


    }

    void FixedUpdate()
    {
        if (jump)
        {
            animatorControllers.TriggerJump();
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump); //false - no jump, no crouch
        if (mRigidbody.velocity.y <= 0 && !jump)
        {
            animatorControllers.ResetJump();
        }
        jump = false;
    }


}
