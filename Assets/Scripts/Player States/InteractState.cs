using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : IState
{
    private Player player;
    private InteractableEntity entity;

    public InteractState(Player player) {
        this.player = player;
        entity = player.GetEntity();
    }

    public void Enter() {
        // Show UI or something
        entity.StartInteraction();
        Debug.Log("ENTER: " + this.GetType().Name);
    }

    public void Exit() {
        entity.EndInteraction();
    }
    public void Execute() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            // Advance interaction.
            AdvanceInteraction();
        }

        //DEBUG: to cancel out
        if (Input.GetKeyDown(KeyCode.X)) {
            player.state.ChangeState(new MovementState(player));
        }
    }

    private void AdvanceInteraction() {
        if (!DialogueManager.instance.AdvanceDialogue()) {
            player.state.ChangeState(new MovementState(player));
        }
    }
}
