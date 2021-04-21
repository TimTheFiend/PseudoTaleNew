using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuBehavior : MonoBehaviour
{
    public static MenuBehavior instance = null;

    private bool isMenuOpen = false;
    private int menuIndex = 0;

    [SerializeField]
    public Submenu inventory;


    [Header("Main Menu Objects")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject mainSelector;
    [SerializeField]
    private GameObject[] mainOptions;

    [Header("Inventory")]
    [SerializeField]
    private GameObject invMenu;
    [SerializeField]
    private GameObject invSelector;
    [SerializeField]
    private GameObject[] invOptions;


    //[Header("Stat")]

    //[Header("Cell")]



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

        Initialize();
    }

    public void OpenMenu() {
        if (isMenuOpen) Resume();
        else Pause();
    }

    // Needs to be re-written to account for different sub menus
    public void NavigateMenu(Vector2 menuPosition) {
        menuPosition.y *= -1;  // Since moving "Up" is "+1" we need to flip the value for "Up" and "Down"
        if (menuPosition.y + menuIndex < 0 || menuPosition.y + menuIndex > currentOptions.Length - 1) { 
            return;
        }
        menuIndex += (int)menuPosition.y;
        currentSelector.transform.position = new Vector3(currentSelector.transform.position.x, currentOptions[menuIndex].transform.position.y);
    }

    public void ActivateSelected() {
        IState menu = currentOptions[menuIndex].gameObject.GetComponent<IState>();
        print(menu);
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
    private void Initialize() {
        currentMenu = mainMenu;
        currentSelector = mainSelector;
        currentOptions = mainOptions;

        isMenuOpen = false;
        mainMenu.SetActive(false);
        invMenu.SetActive(false);
    }
    
    public class Submenu
    {
        [Header("Inventory")]
        [SerializeField]
        private GameObject invMenu;
        [SerializeField]
        private GameObject invSelector;
        [SerializeField]
        private GameObject[] invOptions;
    }
}
