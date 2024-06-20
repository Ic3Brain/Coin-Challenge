using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player_Controller : MonoBehaviour
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


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        MovePlayer();
    }
    
    void Update()
    {
        inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputDir = inputDir.normalized;
        PlayerJump();
        
        /*RaycastHit hit;

        Debug.DrawRay(transform.position, transform.up * 10, Color.red);

        if (Physics.Raycast(transform.position, transform.up, out hit, 10))
        {
            Debug.Log("le raycast touche un objet !");
        }*/
        
    }

    
    
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

    void PlayerJump()
    {
        if(Input.GetButtonDown("Jump") && playerTouchTheGround)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            playerTouchTheGround = false;
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        playerTouchTheGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        playerTouchTheGround = false;
    }

    public void OnTriggerEnter(Collider Col)
    {
        ICollectable iCollectable = Col.gameObject.GetComponent<ICollectable>();
        if(iCollectable == null)
        return;
        iCollectable.OnCollected();
    }
}
