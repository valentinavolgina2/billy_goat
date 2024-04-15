using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayMoney : MonoBehaviour
{
    [HideInInspector]
    public PayMoney instance;
    public bool dialogStarted = false;

    [SerializeField]
    TrollAnimationController mController;

    private DialogManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dialogManager = FindObjectOfType<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            //this.gameObject..isStatic = true;

            FindObjectOfType<AudioManager>().Play("TrollDialog");

            mController.SetSpeaking();
            dialogManager.StartDialogue(instance);
            dialogManager.DialogueComplete += OnDialogueComplete;
            //collisionInfo.gameObject.GetComponent<Collider2D>().enabled = false;

        }

    }

    void OnDialogueComplete()
    {
        mController.ResetSpeaking();
        dialogManager.DialogueComplete -= OnDialogueComplete;
    }

    private void OnDisable()
    {
        
    }
}
