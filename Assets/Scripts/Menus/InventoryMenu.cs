using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour, IMenu
{
    [Header("Inventory")]
    public GameObject menu;
    public GameObject selector;
    public GameObject[] options;

    public int menuIndex { get; set; } = 0;

    private void Awake() {
        menu.SetActive(false);
    }

    public void ActivateCursor() {
        Debug.Log("SELECTED : " + options[menuIndex].name);
    }

    public void CloseMenu() {
        menu.SetActive(false);      // Hide menu
        menuIndex = 0;              // Reset index
        UpdateCursorPosition();     // Reset Selector position

        MenuManager.instance.ChangeMenuToDefault();
    }

    public void MoveCursor(Vector2 cursorPos) {
        int _cursorPos = (int)cursorPos.y * -1 + menuIndex;
        if (_cursorPos < 0 || _cursorPos > options.Length - 1)
            return;

        menuIndex = _cursorPos;
        UpdateCursorPosition();
    }

    public void OpenMenu() {
        menu.SetActive(true);
    }

    private void UpdateCursorPosition() {
        selector.transform.position = new Vector3(selector.transform.position.x, options[menuIndex].transform.position.y);
    }
}
