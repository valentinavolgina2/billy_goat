using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;
    float myHeight;


    // Start is called before the first frame update
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        myHeight = this.GetComponent<SpriteRenderer>().bounds.extents.y;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - (myTrans.right) * myWidth;// + (Vector2.up)*myHeight;

        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos,lineCastPos+(Vector2.down)*0.5f,enemyMask);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2()*0.02f, enemyMask);

        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot; 
        }

        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;


    }
}
