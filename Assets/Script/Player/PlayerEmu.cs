//using Photon.Pun;
using System.Threading;
using UnityEngine;


public class PlayerEmu : MonoBehaviour
    {
    // Start is called before the first frame update
    [Header("Movement")]
    public float movementSpeed;
    public float Run;
    public float horizontal;

    public float vertical;

    public CharacterController characterController;
    //public FixedJoystick FixedJoystick;

   /* [Header("Jump")]
    public float jumpForce = 5.0f; 

    public float GroundDistance;
    public float gravity = 9.8f;
    public Vector3 verticalVelocity;
public LayerMask Glayer;
    */
    

       [Header("Mouse")]
    public Camera playerCamera;
    public float mouseSensitivity = 2.0f;
    public float verticalRotation, minVerticalAngle, maxVerticalAngle;
    public float mouseX, mouseY;

    /*
    public Vector2 touchStartPosition;
    public Vector2 currentTouchPosition;
    public bool isTouching = false;
    public RectTransform joystickPanel;

    public GameObject localPlayerPanel;
      */  
    [Header("Raycast")]
    public float shootCooldown = 0.5f;



[Header("Animation")]

    public Animator Animator;
      
    public float targetSpeed,BlendSpeed;

    //public float VH;
    
    //public float VV;




    [Header("Gravity")]

    public bool isGrounded;
    public float Gravity = 9.8f;
    public float sphereRadius = 0.3f;
    public float sphereCastDistance = 0.2f;
    //  public GameObject  playercam;   




    private enum PlayerState
    {
        Idle,
        Walking,Falling,

        Running,
        Jumping,
        Shooting
    }

    private PlayerState currentState = PlayerState.Idle;
    private float shootTimer = 0f;

    private void Start()
    {
        //characterController = GetComponent<CharacterController>();
        //playerCamera = GetComponentInChildren<Camera>();
   //     playercam.SetActive(true);
       // cam = GameObject.FindWithTag("MainCamera");

      Cursor.lockState = CursorLockMode.Locked;
       
      Cursor.visible = false; 

     //   cam.SetActive(false);

     
    }

    private void Update()
    {
        //  if(pv.IsMine)
        //   {

        ApplyGravity();
       
        //DrawGizmos();
        HandleMouseLook();

        //       OnEnablePlayer(); 

        switch (currentState)
        {
            case PlayerState.Idle:
                HandleIdleState();
                break;

            case PlayerState.Walking:
                HandleWalkingState();
                break;
            case PlayerState.Shooting:
                HandleShootingState();
                break;
            case PlayerState.Running:
                HandleRunningState();

                break;

        }
                Check();
    /// 
   }






    
    void ApplyGravity()
    {
        // SphereCast to check if the character is grounded
         isGrounded = Physics.SphereCast(
            transform.position,
            sphereRadius,
            Vector3.down,
            out RaycastHit hit,
            sphereCastDistance
        );

        if (!isGrounded)
        {
            // Apply gravity when the character is grounded

            Vector3 gravityVector = Vector3.down * Gravity * Time.deltaTime;
            characterController.Move(gravityVector);
        }
    }
    void OnDrawGizmos()
    {
        // Draw a sphere to visualize the ground check
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * sphereCastDistance, sphereRadius);
    }
   
   
        

    
    void Check()

    {

        if(currentState==PlayerState.Running)
        {
          targetSpeed = 6;
            BlendSpeed = 0.2f;
        }else if(currentState == PlayerState.Walking)
        {
            targetSpeed = 2;

            BlendSpeed = 0.1f;
        }
        else if(currentState == PlayerState.Idle)
        {
            targetSpeed = 0;
        }

      

    }

    private void UpdateAnimatorParameters(float VH, float VV)
    {
        VH *= targetSpeed;
        VV *= targetSpeed;

        Animator.SetFloat("H", VH, BlendSpeed, Time.deltaTime);
        Animator.SetFloat("V", VV, BlendSpeed, Time.deltaTime);

        // Synchronize animation parameters over the network
      
    }

   

    private void HandleMouseLook()  
    {
         mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
         mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
     
        // Rotate the player around the Y-axis based on mouse input
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically with limits
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // Apply the new rotation to the camera
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

    }

    private void HandleIdleState()
    {
       
        if (Input.GetButtonDown("Jump"))  
        {
        
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            currentState = PlayerState.Shooting;
        }
     

        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(   Input.GetAxis("Vertical"))> 0.1f)
        {
            currentState = PlayerState.Walking;
        }
       

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            
                currentState = PlayerState.Running;
              
        }
        Debug.Log("idle");

     UpdateAnimatorParameters(0,0);
    }

    private void HandleWalkingState()
    {
        // Handle player movement  
        /*
                horizontal = Input.GetAxis("Horizontal");;
          vertical= Input.GetAxis("Vertical");;
        */
        horizontal = Input.GetAxis("Horizontal");
        vertical= Input.GetAxis("Vertical");


       Vector3 movement = transform.TransformDirection(new Vector3(horizontal, 0, vertical)) * movementSpeed;
        characterController.Move(movement * Time.deltaTime);

        // Handle transitioning to other states
        if (Input.GetButtonDown("Jump"))
        {
            //     currentState = PlayerState.Jumping;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            currentState = PlayerState.Shooting;
        }
        else if (Mathf.Abs(horizontal) < 0.1f && Mathf.Abs(vertical) < 0.1f)
        {
            currentState = PlayerState.Idle;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            currentState = PlayerState.Running;
        }
       
        UpdateAnimatorParameters(horizontal,vertical);


         
        Debug.Log("Moving");
    }
    void HandleRunningState()
    {

       
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // vertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.TransformDirection(new Vector3(horizontal, 0, vertical)) * Run;
        characterController.Move(movement * Time.deltaTime);

        UpdateAnimatorParameters(horizontal, vertical);




         if (Input.GetKeyUp(KeyCode.LeftShift))
          {
              currentState = PlayerState.Walking;
          }

        else if (Mathf.Abs(horizontal) < 0.1f && Mathf.Abs(vertical) < 0.1f)
        {
            currentState = PlayerState.Idle;
        }
        Debug.Log("Running");
     

   
    }

   

    private void HandleShootingState()
    {
        // Handle shooting logic
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootCooldown)
        {
            // Your shooting logic goes here
           // Debug.Log("Shooting!");

            shootTimer = 0f;
            currentState = PlayerState.Idle;
        }
    }

}
