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

    float currentVelocity;
    float smoothTime = 0.05f;
    public bool playerTouchTheGround = true;
    public CollectingMeat collectingMeat;
    public Vector3 localPosition;
    public Quaternion localRotation;
    
    [SerializeField]
    GameManager gameManager;
    
    [SerializeField]
    HealthManager healthManager;
    
    [SerializeField] LayerMask groundDetectionMask;
    
    



   
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
        RaycastHit hit;
        playerTouchTheGround = Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f, groundDetectionMask);
        MovePlayer();
    }

    
    
    void Update()
    {
        inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputDir = inputDir.normalized;
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

    //Saut du joueur
    void PlayerJump()
    {
        

        Debug.DrawRay(transform.position, transform.up * 10, Color.red);

        
        
        if(Input.GetButtonDown("Jump") && playerTouchTheGround)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            playerTouchTheGround = false;
        }

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

    public void OnDamage(float damage)
    {
        PlayerHealthBar.Instance.UpdateBar(healthManager);
    }

    public void OnKill()
    {
        Debug.Log("je suis mort la honte");
    }

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
