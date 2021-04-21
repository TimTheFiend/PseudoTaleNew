using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface IMenu
{
    public GameObject menu { get; set; }
    public GameObject selector { get; set; }
    public List<GameObject> options { get; set; }

    public void OpenMenu();
    public void CloseMenu();
    public void NavigateMenu();


}
