using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("-----ComponentSerializationService-----")]
    [SerializeField] CharacterController characterController;
    #endregion

    #region Player Attributes
    [Header("-----Attributes-----")]
    [Range(1, 20)][SerializeField] int playerSpeed;
    [Range(2, 5)][SerializeField] int sprintMod;
    [Range(1, 3)][SerializeField] int jumpMax;
    [Range(8, 25)][SerializeField] int jumpSpeed;
    [Range(15, 75)][SerializeField] float gravity;
    #endregion


    Vector3 movePlayer;
    Vector3 playerVelocity;

    int jumpCount;

    bool isSprinting;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if(characterController.isGrounded)
        {
            jumpCount = 0;
            playerVelocity = Vector3.zero;
        }

        Sprint();

        movePlayer = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        characterController.Move(playerSpeed * Time.deltaTime * movePlayer);

        Jump();

        characterController.Move(playerVelocity * Time.deltaTime);
        playerVelocity.y -= gravity * Time.deltaTime;
    } 
    
    void Sprint()
    {
        if(Input.GetButtonDown("Sprint"))
        {
            playerSpeed *= sprintMod;
            isSprinting = true;
        }

        else if(Input.GetButtonDown("Sprint"))
        {
            playerSpeed /= sprintMod;
            isSprinting = false;
        }
    }

    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            playerVelocity.y = jumpSpeed;
        }
    }
}
