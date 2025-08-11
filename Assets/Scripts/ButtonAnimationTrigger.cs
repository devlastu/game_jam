using System;
using UnityEngine;

public class ButtonAnimationTrigger : MonoBehaviour {
    public Animator animator;
    public string triggerName = "RestartAnimation";

    public void PlayAnimation(){
        if (animator != null){
            animator.SetTrigger(triggerName);
            Debug.Log("Animation triggered");
        }
    }
}