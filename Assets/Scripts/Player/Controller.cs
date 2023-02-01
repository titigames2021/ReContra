using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private PlayerInput _input;

    private Vector2 move_input_data;
    private bool jump_input;
    private void Awake()
    {

        _input = new PlayerInput();


        _input.GeneralMovement.Move.performed += move_performed =>
        {

            move_input_data = move_performed.ReadValue<Vector2>();
            Debug.Log("Guapisimo");
        };


        _input.GeneralMovement.Move.canceled += move_performed =>
        {

            move_input_data = move_performed.ReadValue<Vector2>();
            Debug.Log("Guapisimo");
        };






        _input.GeneralMovement.Jump.started += jump_performed =>
        {

            jump_input = jump_performed.ReadValueAsButton();

        };



        _input.GeneralMovement.Jump.canceled += jump_performed =>
        {

            jump_input = jump_performed.ReadValueAsButton();

        };






    }
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(move_input_data.x, 0, move_input_data.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jump_input && groundedPlayer)
        {
            playerVelocity.y = jumpHeight * -1.0f * gravityValue;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
