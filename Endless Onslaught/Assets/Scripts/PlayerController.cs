using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("----- Components -----")]
    // Reference to the CharacterController component used for player movement
    [SerializeField] CharacterController characterController;
    #endregion

    #region Player Attributes
    [Header("----- Attributes -----")]
    // Player movement speed
    [Range(1, 20)][SerializeField] int playerSpeed;

    // Sprint multiplier to increase movement speed
    [Range(2, 5)][SerializeField] int sprintMod;

    // Maximum number of jumps allowed before the player must land
    [Range(1, 3)][SerializeField] int jumpMax;

    // Speed applied when the player jumps
    [Range(8, 25)][SerializeField] int jumpSpeed;

    // Gravity applied to the player when they are in the air
    [Range(15, 75)][SerializeField] float gravity;
    #endregion

    #region Private Variables
    // Vector to store player's movement direction
    Vector3 movePlayer;

    // Vector to store player's current velocity (for jumping and gravity)
    Vector3 playerVelocity;

    // Counter to keep track of the number of jumps
    int jumpCount;

    // Boolean to determine if the player is sprinting
    bool isSprinting;
    #endregion

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(); // Handle all player movement input and mechanics
    }

    // Handles player movement including walking, running, and jumping
    void PlayerMovement()
    {
        // Check if the player is grounded
        if (characterController.isGrounded)
        {
            // Reset jump count and vertical velocity when grounded
            jumpCount = 0;
            playerVelocity = Vector3.zero; // Reset vertical velocity
        }

        // Handle sprinting input
        Sprint();

        // Get movement input from the player and calculate movement direction
        movePlayer = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        characterController.Move(playerSpeed * Time.deltaTime * movePlayer);

        // Handle jumping input
        Jump();

        // Apply gravity to the player
        playerVelocity.y -= gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    // Handles sprinting logic
    void Sprint()
    {
        // If the sprint button is pressed down, increase the player's speed
        if (Input.GetButtonDown("Sprint"))
        {
            playerSpeed *= sprintMod;
            isSprinting = true;
        }

        // If the sprint button is released, return the player's speed to normal
        else if (Input.GetButtonUp("Sprint"))
        {
            playerSpeed /= sprintMod;
            isSprinting = false;
        }
    }

    // Handles jumping logic
    public void Jump()
    {
        // If the jump button is pressed and the player has jumps left, make the player jump
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++; // Increment jump count
            playerVelocity.y = jumpSpeed; // Apply jump speed to vertical velocity
        }
    }
}
