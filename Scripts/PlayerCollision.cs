using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    public static PlayerCollision instance;
    public PlayerMovement movement;

    public Vector3 respawnPoint;
    public Text healthText;
    public Text prizeText;

    [SerializeField]
    private AnimatorControllers animatorControllers;

    [SerializeField]
    private float resetTime;

    int prize = 1;

    //combat variables
    [SerializeField]
    private int healthMax = 5;

    
    public int Health {
        get { return health; }
        set { health = value; healthText.text = "Health: " + health.ToString(); } }
    private int health;

    public int Coin {
        get { return coin; }
        set { coin = value; prizeText.text = "Coin: " + coin.ToString(); } }
    private int coin;
    private float invisibleTimeAfterHurt = 2;

    [HideInInspector]
    public Collider2D[] myColls;

    private void Start()
    {
        instance = this;
        myColls = this.GetComponents<Collider2D>();
        Health = healthMax;
        Coin = 0;
       
    }

    private void Hurt(int damageValue = 1)
    {

        Health -= damageValue;
        Debug.Log("Health: " + health);
        if (Health <= 0)
        {
            animatorControllers.TriggerDead();
            StartCoroutine(Respawn());

           // Application.LoadLevel(Application.loadedLevel);
           // SceneManager.LoadScene("MyScene");

        }
        else
        {

            animatorControllers.TriggerHurt(invisibleTimeAfterHurt);
        }

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(resetTime);
        transform.position = new Vector3(respawnPoint.x + 0.8f, respawnPoint.y+5f, respawnPoint.z); ;

        Health = healthMax;
        Coin = 0;
        animatorControllers.ResetAnimation();
    }

    //runs when we hit another object
    void OnCollisionEnter2D(Collision2D collisionInfo)
     {

        var damage = collisionInfo.gameObject.GetComponent<Damage>();
        if (damage != null)
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            Hurt(damage.value);
        }

        //check if it is an enemy
        if (collisionInfo.gameObject.tag == "Enemy")
        {
            FindObjectOfType<AudioManager>().Play("HitEnemy");

            Hurt();


        }
        else if (collisionInfo.gameObject.tag == "Coin")
        {

            FindObjectOfType<AudioManager>().Play("HitCoin");

            Destroy(collisionInfo.gameObject);//prize will disappear!
            Coin += prize;
            if (Health > healthMax) Health = healthMax;
           

        }


        else if (collisionInfo.gameObject.tag == "Prize")
        {

            FindObjectOfType<AudioManager>().Play("HitPrize");

            Destroy(collisionInfo.gameObject);//prize will disappear!
            Coin += 5*prize;
            if (Health > healthMax) Health = healthMax;


        }
        else if (collisionInfo.gameObject.tag == "Moving")
        {
           

            transform.parent = collisionInfo.transform;


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moving")
        {
            transform.parent = null;
            Debug.Log("Leaving moving platform");

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
            
        }

    }




}
