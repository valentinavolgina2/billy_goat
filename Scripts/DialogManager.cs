using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour{
	
	public Text dialogueText;
	public Animator animator;
    public PlayerCollision playerCollis;
    private PayMoney payMoney;

    public Dialog dialog_no_money;
    public Dialog dialog_has_money;
    public Dialog firstDialog_no_money;
    public Dialog firstDialog_has_money;

    public ButtonController button;

    public int fee;
    private bool enoughCoins;

    private Queue<string> sentences;

    public event Action DialogueComplete;
	
	void Start(){
		sentences = new Queue<string>();

    }
	
	public void StartDialogue(PayMoney payMoney)
	{
        //playerCollis.gameObject.SetActive(false);
        playerCollis.movement.enabled = false;

        this.payMoney = payMoney;

        animator.SetBool("isOpen",true);
		
		sentences.Clear();

        this.enoughCoins = true;
        if (this.fee > playerCollis.Coin)
        {
            //sentences.Enqueue("It costs " + fee + " coins to go through. You don't have enough coins to pay. Go and grap some!");

            if (payMoney.dialogStarted == false)
            {
                foreach (string sentence in this.firstDialog_no_money.sentences)
                    sentences.Enqueue(sentence);
                payMoney.dialogStarted = true;
            }
            else
            {
                foreach (string sentence in this.dialog_no_money.sentences)
                    sentences.Enqueue(sentence);
            }

			
			this.enoughCoins = false;
        }
        else 
        {
            if (payMoney.dialogStarted == false)
            {
                foreach (string sentence in this.firstDialog_has_money.sentences)
                    sentences.Enqueue(sentence);
                payMoney.dialogStarted = true;
            }
            else
            {
                foreach (string sentence in this.dialog_has_money.sentences)
                    sentences.Enqueue(sentence);
            }

        }
        DisplayNextSentence();


		
	}
	
	public void DisplayNextSentence(){
		
		if(sentences.Count == 0){

            if (enoughCoins)
                PayMoney();
            else
                CloseDialogue();


		}
		string sentence = sentences.Dequeue();
		
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		
		//dialogueText.text = sentence;

        changeButton(sentences.Count == 0);

    }
	
	
	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
		
	}

    public void changeButton(bool lastSentence)
    { 
        if(lastSentence)
			if(enoughCoins)
			{
				//PayMoney();
				//CloseDialogue();
				 button.transform.GetComponent<Text>().text = "Pay";
			}
			else
				button.transform.GetComponent<Text>().text = "Close the dialog";
        else
            button.transform.GetComponent<Text>().text = "Next";
    }

    public void PayMoney() 
    {
        //pay money
        playerCollis.Coin -= fee;

        //remove the collider
        this.payMoney.GetComponent<Collider2D>().enabled = false;

        //playerCollis.gameObject.SetActive(true);
        playerCollis.movement.enabled = true;

        //close dialog
        EndDialogue();



    }

    public void CloseDialogue()
    {
        //move the player a little bit so that it doesn't collide again
        playerCollis.gameObject.transform.position = new Vector3(playerCollis.gameObject.transform.position.x -0.2f, playerCollis.gameObject.transform.position.y, playerCollis.gameObject.transform.position.z);
       

        playerCollis.movement.enabled = true;
        //close dialog
        EndDialogue();


    }

    void EndDialogue(){
		
		animator.SetBool("isOpen",false);
		if (DialogueComplete != null)
        {
            DialogueComplete();
        }
	}
	
	
	
}
