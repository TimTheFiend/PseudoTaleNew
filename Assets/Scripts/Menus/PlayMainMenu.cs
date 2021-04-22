using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMainMenu : MonoBehaviour, IMenu
{
    [Header("Main Menu")]
    public GameObject menu;
    public GameObject selector;
    public GameObject[] options;

    public int menuIndex { get; set; } = 0;

    private void Awake() {
        gameObject.SetActive(false);
    }

    public void OpenMenu() {
        menu.SetActive(true);
    }

    public void CloseMenu() {
        menu.SetActive(false);
        menuIndex = 0;
        UpdateSelectorPosition();
    }

    public void ActivateCursor() {
        MenuManager.instance.ChangeMenu(options[menuIndex]);
    }

    public void MoveCursor(Vector2 cursorPos) {
        int newCursorPos = (int)cursorPos.y * -1 + menuIndex;
        if (newCursorPos < 0 || newCursorPos > options.Length - 1) {
            return;
        }
        menuIndex = newCursorPos;
        UpdateSelectorPosition();
    }

    private void UpdateSelectorPosition() {
        selector.transform.position = new Vector3(selector.transform.position.x, options[menuIndex].transform.position.y);
    }



}
