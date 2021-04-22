using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMenu : MonoBehaviour, IMenu
{
    [Header("Stat Menu")]
    public GameObject menu;

    private void Awake() {
        menu.SetActive(false);
    }

    public int menuIndex { get; set; } = 0;

    public void ActivateCursor() { return; }

    public void CloseMenu() {
        menu.SetActive(false);
        MenuManager.instance.ChangeMenuToDefault();
    }

    public void MoveCursor(Vector2 cursorPos) { return; }

    public void OpenMenu() {
        menu.SetActive(true);
    }
}
