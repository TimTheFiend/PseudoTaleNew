using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    [Range(1f, 10f)]
    public float movementSpeed = 5f;
    public string playerName = "Frisk";
    public int lv = 1;
    public int maxHP = 20;

    public StateMachine state = new StateMachine();

    [Header("Runtime Variables")]
    public int currentHP;
    [SerializeField]
    private List<GameObject> interactableEntities;  // TODO


    private void Awake() {
        interactableEntities = new List<GameObject>();

        state.ChangeState(new MovementState(this));
    }

    private void Update() {
        state.Update();
    }

}
