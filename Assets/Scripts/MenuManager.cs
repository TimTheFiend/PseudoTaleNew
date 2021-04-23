using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance = null;

    private bool isMenuOpen = false;

    [Header("Default menu")]
    [SerializeField]
    private GameObject defaultMenu;  // The "main" menu that opens when not currently in `MenuState` 

    private GameObject currentMenuObject;   // The current `IMenu` gameobject
    private IMenu currentMenu;              // The current `IMenu` object


    private void Awake() {
        #region Singleton pattern
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        #endregion

        SetMenuToDefault();
    }

    // Activates the currently selected element in the current menu.
    public void ActivateCursor() {
        currentMenu.ActivateCursor();
    }

    // Sends the movement input to the current menu.
    public void MoveCursor(Vector2 cursor) {
        currentMenu.MoveCursor(cursor);
    }

    // Goes back a menu-layer, and returns true if the current menu is the first layer.
    public bool GoBack() {
        bool isDefault = currentMenuObject == defaultMenu;  // Since all menus inherit from IMenu, they all have different `CloseMenu` interactions.

        currentMenu.CloseMenu();

        return isDefault;
    }

    // Sets currentMenuObject and currentMenu, and calls its `OpenMenu` function
    public void ChangeMenu(GameObject newMenu) {
        currentMenuObject = newMenu;
        currentMenu = newMenu.GetComponent<IMenu>();

        currentMenu.OpenMenu();
    }

    public void ChangeMenuToDefault() {
        SetMenuToDefault();
    }

    // Is called from `PlayMainMenu` on `Open` and `Close`
    public void OpenDefaultMenu() {
        print(isMenuOpen);
        if (isMenuOpen) {
            DefaultExit();
        }
        else {
            DefaultEnter();
        }
    }

    #region Pause/Resume
    private void DefaultEnter() {
        currentMenu.OpenMenu();
        isMenuOpen = true;
        Time.timeScale = 0f;
    }

    private void DefaultExit() {
        isMenuOpen = false;
        Time.timeScale = 1f;
        currentMenu.CloseMenu();
    }
    #endregion

    // Resets the currentMenu to defaultMenu
    private void SetMenuToDefault() {
        currentMenuObject = defaultMenu;
        currentMenu = defaultMenu.GetComponent<IMenu>();
    }
}
