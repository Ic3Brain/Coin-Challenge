using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour, IDamageable
{
    Vector3 inputDir;
    
    [SerializeField]
    float forwardSpeed = 10;
    
    [SerializeField]
    float strafeSpeed = 8;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Camera cam;

    [SerializeField]
    AudioClip jump;

    [SerializeField]
    AudioSource SFXAudioSource;

    float currentVelocity;
    float smoothTime = 0.05f;
    
    public CollectingMeat collectingMeat;
    public Vector3 localPosition;
    public Quaternion localRotation;
    
    [SerializeField]
    GameManager gameManager;
    
    [SerializeField]
    HealthManager healthManager;
    
    [SerializeField] LayerMask groundDetectionMask;

    public bool IsAlive
    {
        get 
        {
            return healthManager.health > 0;
        }
    } 

    bool isGrounded; 
    float jumpCooldown = 0.2f; 
    float lastJumpTime = 0f; 
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        localPosition = transform.localPosition;
        localRotation = transform.localRotation;
    }
    
    void Start()
    {
        PlayerHealthBar.Instance.UpdateBar(healthManager);
        
    }
    
    void FixedUpdate()
    {
        MovePlayer();
    }

    
    
    void Update()
    {
        inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputDir = inputDir.normalized;
        CheckGrounded();
        PlayerJump();
        PlayerFall();
    }

    //Au restart on vient chercher la position initial du joueur
    public void Respawn()
    {
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }

    
    //Mouvement du joueur et de la caméra
    void MovePlayer()
    {
        //forwardDir
        Vector3 forwardDir = transform.forward * inputDir.z;
        forwardDir.Normalize();
        forwardDir *= forwardSpeed;

        //strafeDir
        Vector3 strafeDir = Vector3.Cross(Vector3.up, transform.forward)* inputDir.x;
        strafeDir.Normalize();
        strafeDir *= strafeSpeed;

        Vector3 moveDir = forwardDir + strafeDir;
        
        rb.MovePosition(transform.position + (moveDir*Time.deltaTime));

        //RotatePlayerTowardCamDirection
        float targetRotation = cam.transform.eulerAngles.y;
        float playerAngleDamp = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0, playerAngleDamp, 0);
    }


    public void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && Time.time > lastJumpTime + jumpCooldown)
        {   
        // Joue l'effet sonore de saut
        SFXAudioSource.clip = jump;
        SFXAudioSource.Play();
        
        // Applique une force vers le haut pour faire sauter le joueur
        rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);

        // Met à jour le temps du dernier saut pour démarrer le délai
        lastJumpTime = Time.time;

        // Désactive temporairement la détection de sol pour éviter les détections prématurées
        isGrounded = false;
        }
    }

    public void CheckGrounded()
    {   
        // On lance le raycast d'un peu au-dessus de la position du joueur pour une meilleure détection
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hitInfo, 1.2f);

        // Dessine le raycast pour la visualisation
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * 1.2f, Color.red);
    }


    //On détecte la col avec des meat puis on les collecte
    public void OnTriggerEnter(Collider Col)
    {
        ICollectable iCollectable = Col.gameObject.GetComponent<ICollectable>();
        if(iCollectable == null)
        return;
        if(!iCollectable.isCollectable)
        return;
        iCollectable.OnCollected();
    }

    //On détecte la col avec un enemy puis on le détruit
    public void OnTriggerEnterEnemy(BoxCollider col)
    {
        if(col.gameObject.tag == "Enemy")
        {   
            Debug.Log("touché");
            Destroy(gameObject);
        }
    }

    //Update la barre de vie si damage
    public void OnDamage(float damage)
    {
        PlayerHealthBar.Instance.UpdateBar(healthManager);
    }


    //Si mort alors gameover
    public void OnKill()
    {   
        gameManager.OnGameOver();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void FreezeMovement()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    //Tue le joueur si en dessous de -4 y
    public void PlayerFall()
    {
        if(transform.position.y < -4)
        {   
            rb.constraints = RigidbodyConstraints.FreezeAll;
            gameManager.OnGameOver();
        }
        else
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }
}
