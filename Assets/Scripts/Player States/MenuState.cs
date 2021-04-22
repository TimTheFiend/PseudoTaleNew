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
        MenuManager.instance.OpenDefaultMenu();
        Debug.Log("ENTER: " + this.GetType().Name);
    }

    public void Exit() {
        MenuManager.instance.OpenDefaultMenu();
        Debug.Log("EXIT: " + this.GetType().Name);
    }

    public void Execute() {
        menuPosition = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        MoveMenuCursor();

        // `Presses` the `Button`
        if (Input.GetKeyDown(KeyCode.Z)) {
            MenuManager.instance.ActivateCursor();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            if (MenuManager.instance.GoBack()) {
                player.state.ChangeState(new MovementState(player));
            }
        }
    }

    private void MoveMenuCursor() {
        if (menuPosition.x == 0 && menuPosition.y == 0) {
            oldMenuPosition = menuPosition;
            return;
        }

        if (menuPosition != oldMenuPosition) {
            MenuManager.instance.MoveCursor(menuPosition);
            //MenuBehavior.instance.NavigateMenu(menuPosition);
            oldMenuPosition = menuPosition;
        }
    }
}
