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
        PlayerAnimator.SetFloat("ForwardMove", Input.GetAxis("Vertical"));
        PlayerAnimator.SetFloat("SideMove", Input.GetAxis("Horizontal"));
        PlayerAnimator.SetFloat("Attack", Input.GetAxis("Fire1"));
        //animation du saut
        PlayerAnimator.SetFloat("Jump", Input.GetAxis("Jump"));
    }
}


