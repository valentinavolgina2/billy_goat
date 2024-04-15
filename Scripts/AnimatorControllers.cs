using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllers : MonoBehaviour
{
    public bool isHurting;
    public bool isDead;

    [SerializeField]
    private Animator myAnim;
   

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAnimation()
    {
        myAnim.ResetTrigger("isDead");
        myAnim.ResetTrigger("isHurting");
        myAnim.ResetTrigger("isJumping");
    }

    public void TriggerDead()
    {
        myAnim.SetTrigger("isDead");
    }

    public void TriggerJump()
    {
        myAnim.SetTrigger("isJumping");
    }

    public void ResetJump()
    {
        myAnim.ResetTrigger("isJumping");
    }

    public void TriggerHurt(float hurtTime)
    {
        StartCoroutine(HurtBlinker(hurtTime));
        myAnim.SetTrigger("isHurting");
    }

    IEnumerator HurtBlinker(float hurtTime)
    {
        //ignor collisions between player and enemy for a while
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer,true);

        //start blinking animation
        myAnim.SetLayerWeight(1, 1);

        //wait for invisibility to end
        yield return new WaitForSeconds(hurtTime);

        //stop blinking and turn on collisions
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer,false);
        myAnim.SetLayerWeight(1, 0);
        myAnim.ResetTrigger("isHurting");
        Debug.Log("--------");

    }

    ~AnimatorControllers()
    {
        Debug.Log("Destructor");
    }


}
