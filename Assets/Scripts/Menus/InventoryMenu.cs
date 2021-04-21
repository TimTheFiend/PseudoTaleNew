using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour, IState
{
    [Header("Inventory")]
    [SerializeField]
    private GameObject invMenu;
    [SerializeField]
    private GameObject invSelector;
    [SerializeField]
    private GameObject[] invOptions;

    public void Enter() {
        invMenu.SetActive(true);
    }

    public void Execute() {

    }

    public void Exit() {
        invMenu.SetActive(false);
    }
}
