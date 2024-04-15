using UnityEngine;
using System.Collections;

public class TrollAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpeaking()
    {
        animator.SetTrigger("TrollSpeak");
    }
    public void ResetSpeaking()
    {
        animator.ResetTrigger("TrollSpeak");
    }
}
