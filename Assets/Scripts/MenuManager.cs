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
    private GameObject defaultMenu;

    private GameObject currentMenuObject;
    private IMenu currentMenu;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        SetMenuToDefault();
    }

    public void ActivateCursor() {
        currentMenu.ActivateCursor();
    }

    public void MoveCursor(Vector2 cursor) {
        currentMenu.MoveCursor(cursor);
    }

    public bool GoBack() {
        bool isDefault = currentMenuObject == defaultMenu;

        currentMenu.CloseMenu();

        return isDefault;
    }

    public void ChangeMenu(GameObject newMenu) {
        currentMenuObject = newMenu;
        currentMenu = newMenu.GetComponent<IMenu>();

        currentMenu.OpenMenu();
    }

    public void ChangeMenuToDefault() {
        SetMenuToDefault();
    }

    public void OpenDefaultMenu() {
        if (isMenuOpen) {
            DefaultExit();
        }
        else {
            DefaultEnter();
        }
    }

    private void DefaultEnter() {
        isMenuOpen = true;
        Time.timeScale = 0f;
        currentMenu.OpenMenu();
    }

    private void DefaultExit() {
        isMenuOpen = false;
        Time.timeScale = 1f;
        currentMenu.CloseMenu();
    }

    private void SetMenuToDefault() {
        currentMenuObject = defaultMenu;
        currentMenu = defaultMenu.GetComponent<IMenu>();
    }
}
