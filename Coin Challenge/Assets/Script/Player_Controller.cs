using UnityEngine;


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

    public bool playerTouchTheGround = false; 
    
    public CollectingMeat collectingMeat;
    public Vector3 localPosition;
    public Quaternion localRotation;
    
    [SerializeField]
    GameManager gameManager;
    
    [SerializeField]
    HealthManager healthManager;
    
    [SerializeField] LayerMask groundDetectionMask;

    [SerializeField]
    Animator animator;

    public GameObject player;

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
        CheckGrounded();
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


    void PlayerJump()
 {


     Debug.DrawRay(transform.position, transform.up * 10, Color.red);

    if(Input.GetButtonDown("Jump") && playerTouchTheGround)
     {
         SFXAudioSource.clip = jump;
         SFXAudioSource.Play();
         rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
         playerTouchTheGround = false;
     }

 }

    public void CheckGrounded()
    {   
        RaycastHit hit;
        playerTouchTheGround = Physics.Raycast(transform.position + new Vector3(0, 0.15f, 0), Vector3.down, out hit, 0.3f, groundDetectionMask);
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
        player.SetActive(false);
        gameManager.OnGameOver();
    }

    public void FreezeMovement()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnFreezeMovement()
    {
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;	
    }

    //Tue le joueur si en dessous de -4 y
    public void PlayerFall()
    {
        if(transform.position.y < -4)
        {   
            player.SetActive(false);
            gameManager.OnGameOver();
        }
    }

    public void DisableJumpComponents()
    {
        animator.enabled = false;
        
        SFXAudioSource.mute = true;
    }

    // Réactiver animation et son
    public void EnableJumpComponents()
    {
        animator.enabled = true;
    
        SFXAudioSource.mute = false;
    }

    public void DisablePlayerCollider()
    {
        player.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void EnablePlayerCollider()
    {
        player.GetComponent<CapsuleCollider>().enabled = true;
    }
}
