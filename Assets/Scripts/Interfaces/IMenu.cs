using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenu
{
    // Tracks the current position of the cursor, or tracks the currently selected option
    public int menuIndex { get; set; }

    public void OpenMenu();
    public void CloseMenu();
    public void MoveCursor(Vector2 cursorPos);
    // Activates the selected option
    public void ActivateCursor();
}
