using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class AnimPlayerController : MonoBehaviour
{
    Animator PlayerAnimator;
    
    
    void Awake() 
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //animation du saut
        PlayerAnimator.SetFloat("Jump", Input.GetAxis("Jump"));
        
        

        //animation pour avancer 
        if(Input.GetAxis("Vertical") > 0)
        {
            PlayerAnimator.SetBool("walk", true);
        }
        else if(Input.GetAxis("Vertical") == 0)
        {
            PlayerAnimator.SetBool("walk", false);
        }

        //animation reculer
        if(Input.GetAxis("Vertical") < 0)
        {
            PlayerAnimator.SetBool("BackWard", true);
        }
        else if(Input.GetAxis("Vertical") == 0)
        {
            PlayerAnimator.SetBool("BackWard", false);
        }

        //animation aller a droite
        if(Input.GetAxis("Horizontal") > 0)
        {
            PlayerAnimator.SetBool("StrafeRight", true);
        }
        else if(Input.GetAxis("Horizontal") == 0)
        {
            PlayerAnimator.SetBool("StrafeRight", false);
        }

        //animation aller a gauche
        if(Input.GetAxis("Horizontal") < 0)
        {
            PlayerAnimator.SetBool("StrafeLeft", true);
        }
        else if(Input.GetAxis("Horizontal") == 0)
        {
            PlayerAnimator.SetBool("StrafeLeft", false);
        }

        //animation courir en avant 
        if(Input.GetKey(KeyCode.LeftShift))
        {
            PlayerAnimator.SetBool("RunForward", true);
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            PlayerAnimator.SetBool("RunForward", false);
        }

        
    }
}


