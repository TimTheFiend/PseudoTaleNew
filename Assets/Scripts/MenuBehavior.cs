using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuBehavior : MonoBehaviour
{
    public static MenuBehavior instance = null;

    private bool isMenuOpen = false;
    private int menuIndex = 0;


    [Header("Main Menu Objects")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject mainSelector;
    [SerializeField]
    private GameObject[] mainOptions;

    // To switch between
    private GameObject currentMenu;
    private GameObject currentSelector;
    private GameObject[] currentOptions;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        ResetCurrentMenu();
    }

    public void OpenMenu() {
        if (isMenuOpen) Resume();
        else Pause();
    }

    public void NavigateMenu(float y) {
        y *= -1;  // Since moving "Up" is "+1" we need to flip the value for "Up" and "Down"
        if (y + menuIndex < 0 || y + menuIndex > currentOptions.Length - 1) { 
            return;
        }
        menuIndex += (int)y;
        currentSelector.transform.position = new Vector3(currentSelector.transform.position.x, currentOptions[menuIndex].transform.position.y);
    }

    private void Resume() {
        isMenuOpen = false;
        Time.timeScale = 1f;
        currentMenu.SetActive(false);
    }

    private void Pause() {
        isMenuOpen = true;
        Time.timeScale = 0f;
        currentMenu.SetActive(true);
    }

    // Sets the `current`X´` variables to the first layer of menu (Main)
    private void ResetCurrentMenu() {
        currentMenu = mainMenu;
        currentSelector = mainSelector;
        currentOptions = mainOptions;

        isMenuOpen = false;
        currentMenu.SetActive(false);
    }

}
