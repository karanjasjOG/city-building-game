using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseMenu : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        
    }
    public void ShowHideMenu()
    {
        
        bool isOpen = animator.GetBool("show");
        
        animator.SetBool("show", !isOpen);
    }
}
