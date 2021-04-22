using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : IState
{
    private Player player;
    private Vector2 menuPosition;
    private Vector2 oldMenuPosition;

    // Constructor
    public MenuState(Player player) {
        this.player = player;
    }

    #region Interface implementation
    public void Enter() {
        MenuManager.instance.OpenDefaultMenu();
        Debug.Log("ENTER: " + this.GetType().Name);
    }

    public void Exit() {
        MenuManager.instance.OpenDefaultMenu();
        Debug.Log("EXIT: " + this.GetType().Name);
    }
    #endregion

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

    // Move Menu Cursor/Selector, as long as the input is new, this makes it so you don't fly up and down the menu.
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
