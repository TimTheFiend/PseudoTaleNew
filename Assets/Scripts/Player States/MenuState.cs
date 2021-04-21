using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : IState
{
    private Player player;
    private Vector2 menuPosition;
    private Vector2 oldMenuPosition;


    public MenuState(Player player) {
        this.player = player;
    }

    public void Enter() {
        MenuBehavior.instance.OpenMenu();
        Debug.Log("ENTER: " + this.GetType().Name);
    }

    public void Exit() {
        MenuBehavior.instance.OpenMenu();
        Debug.Log("EXIT: " + this.GetType().Name);
    }

    public void Execute() {
        menuPosition = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        MoveMenuSelector();

        if (Input.GetKeyDown(KeyCode.Z)) {
            ActivateSelectedOption();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            player.state.ChangeState(new MovementState(player));
        }
    }

    private void ActivateSelectedOption() {
        // todo
        MenuBehavior.instance.ActivateSelected();
    }

    private void MoveMenuSelector() {
        if (menuPosition.x == 0 && menuPosition.y == 0) {
            oldMenuPosition = menuPosition;
            return;
        }


        if (menuPosition != oldMenuPosition) {
            MenuBehavior.instance.NavigateMenu(menuPosition);
            oldMenuPosition = menuPosition;
        }
    }
}
