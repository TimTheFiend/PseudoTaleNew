using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : IState
{
    private Player player;
    private bool isMoving = false;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private Vector2 playerPosition;

    private enum MoveDir { W, A, S, D }
    private MoveDir playerDir;  // Used to store direction `Player` is facing after `Movement` stops.

    // Constructor
    public MovementState(Player player) {
        this.player = player;

        animator = player.GetComponent<Animator>();
        rigidbody = player.GetComponent<Rigidbody2D>();
    }

    #region Interface implementation
    public void Enter() {
        Debug.Log("ENTER: " + this.GetType().Name);
    }

    public void Exit() {
        Debug.Log("EXIT: " + this.GetType().Name);
    }

    public void Execute() {
        playerPosition = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovePlayer();
        UpdateAnimation();

        // Check for player input
        if (Input.GetKeyDown(KeyCode.Z) && player.canInteract) {
            Debug.Log("CAN INTERACT");
            player.state.ChangeState(new InteractState(player));
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            player.state.ChangeState(new MenuState(player));
        }

    }
    #endregion

    private void MovePlayer() {
        rigidbody.velocity = playerPosition * player.movementSpeed;
    }

    // Updates Animator variables
    private void UpdateAnimation() {
        if (playerPosition.x == 0 && playerPosition.y == 0) {
            isMoving = false;
        }
        else {
            if (playerPosition.x == 0) 
                playerDir = playerPosition.y > 0 ? MoveDir.W : MoveDir.S;
            else
                playerDir = playerPosition.x > 0 ? MoveDir.D : MoveDir.A;

            isMoving = true;
            animator.SetFloat("FacingDirection", (float)playerDir);
            animator.SetFloat("Vertical", playerPosition.y);
            animator.SetFloat("Horizontal", playerPosition.x);
        }

        animator.SetBool("IsMoving", isMoving);
    }

    private void UpdateFacingDirection() {
        animator.SetFloat("FacingDirection", (float)MoveDir.S);
    }
}
