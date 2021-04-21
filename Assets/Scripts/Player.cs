using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    public float movementSpeed = 5f;
    public StateMachine state = new StateMachine();

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
