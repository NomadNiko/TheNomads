using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour {
    private Animator animator;
    private int isRunningHash;
    private int isWalkingHash;
    private int isAttackingHash;

    private int comboNumHash;
    
    private PlayerInput input;
    private Vector2 currentMovement;
    private bool movementPressed;
    private bool runPressed;
    private bool isAttacking;

    private int comboNum = 0;
    private float lastAttack = 0.0f;
    private float comboAllowance = 2f;
    public float moveSpeed = 15f;
    private float runMultiplier = 1.5f;
    private float attackTime = 1f;

    public SpeedBuff speedBuffScript;
    


    [SerializeField] private GameObject hitbox;
    private HitBoxAttack hitboxScript;
    private Rigidbody rb;

    private void Awake() {
        // Initialize input and rigidbody
        input = new PlayerInput();
        rb = GetComponent<Rigidbody>();

        // Initialize hitbox script so we can pass it values
        hitboxScript = hitbox.GetComponent<HitBoxAttack>();

        HandleInput();
    }

    private void Start() {
        // Initialize animator and hash values for animation states
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash = Animator.StringToHash("isAttacking");
        comboNumHash = Animator.StringToHash("comboNum");
    }

    private void FixedUpdate() {
        // Handle character rotation and movement every physics update
        HandleRotation();
        HandleMovement();
    }

    private void HandleInput() {

        // Movement input handling
        input.CharacterControls.Movement.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        input.CharacterControls.Movement.canceled += ctx => {
            currentMovement = Vector2.zero;
            movementPressed = false;
        };

        // Run input handling
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();

        // Attack input handling
        input.CharacterControls.Attack_A.performed += ctx => StartAttack();
    }

    private void HandleRotation() {
        // Rotate the character to face the direction of movement
        Vector3 originalMovement = new Vector3(currentMovement.x, 0, currentMovement.y);
        Vector3 rotatedMovement = Quaternion.Euler(0, 45, 0) * originalMovement;
        Vector3 positionToLookAt = transform.position + rotatedMovement;
        transform.LookAt(positionToLookAt);
    }

    private void HandleMovement() {
        // Get current animation states
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        // Rotate the movement vector to match the character's orientation
        Vector3 rotatedMovement = Quaternion.Euler(0, 45, 0) * new Vector3(currentMovement.x, 0, currentMovement.y);

        // Normalize the movement vector to prevent increased moveSpeed when moving diagonally
        rotatedMovement.Normalize();

        float movementMagnitude = rotatedMovement.magnitude;
        float movementSpeed = movementMagnitude * moveSpeed * (runPressed ? runMultiplier : 1f);

        // Check if the character is attacking
        if (isAttacking) {
            // Stop movement when attacking
            movementSpeed = 0f;
        }

        // Set animation states based on movement conditions
        if (movementMagnitude > 0 && !isWalking) {
            animator.SetBool(isWalkingHash, true);
        }

        if (movementMagnitude == 0 && isWalking) {
            // If not moving and currently walking, transition to idle
            animator.SetBool(isWalkingHash, false);
        }

        if (movementMagnitude > 0 && (runPressed) && !isRunning) {
            animator.SetBool(isRunningHash, true);
        }

        if ((movementMagnitude == 0 || (!runPressed)) && isRunning) {
            // If not moving or not pressing run, transition to walk
            animator.SetBool(isRunningHash, false);
        }

        // Move Player
        rb.AddForce(rotatedMovement * movementSpeed, ForceMode.VelocityChange);
    }

    private void StartAttack() {
        if (!isAttacking) {
            // Trigger the attack animation
            animator.SetBool(isAttackingHash, true);

            // Set the character to be attacking
            isAttacking = true;

            //Check if the player has attacked fast enough to get a combo, if not reset
            if (Time.time - lastAttack > comboAllowance){
                comboNum = 0;
            }
            lastAttack = Time.time;

            // Activate Hitbox
            hitbox.SetActive(true);
            // Tell the hitbox which attack is being used
            hitboxScript.comboNum = comboNum;

            // Iterate combo number and trigger proper animation
            animator.SetInteger(comboNumHash, comboNum);
            comboNum = (comboNum + 1) % 3;
            
            // Call EndAttack after the expected duration of the attack animation
            Invoke("EndAttack", attackTime);
        }
    }

    private void EndAttack() {
        // Reset the attacking state
        isAttacking = false;

        //Disable Hitbox
        hitbox.SetActive(false);

        // Reset the isAttacking parameter in the animator to false
        animator.SetBool(isAttackingHash, false);
    }

    private void OnEnable() {
        // Enable character controls on script enable
        input.CharacterControls.Enable();
    }

    private void OnDisable() {
        // Disable character controls on script disable
        input.CharacterControls.Disable();
    }
}
