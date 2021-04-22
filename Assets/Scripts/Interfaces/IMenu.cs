using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenu
{
    public int menuIndex { get; set; }

    public void OpenMenu();
    public void CloseMenu();
    public void MoveCursor(Vector2 cursorPos);
    public void ActivateCursor();
}
