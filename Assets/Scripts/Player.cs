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

    public StateMachine state = new StateMachine();  // Handles different player states.

    [Header("Runtime Variables")]
    public int currentHP;
    [SerializeField]
    private List<GameObject> interactableEntities;
    
    public bool canInteract;  // Is set by collider, and is based on if interactableEntities


    private void Awake() {
        DontDestroyOnLoad(gameObject);

        interactableEntities = new List<GameObject>();
        state.ChangeState(new MovementState(this));
    }

    private void Update() {
        state.Update();
    }

    // Returns the only/closest Entity in `interactableEntities`
    public InteractableEntity GetEntity() {
        if (interactableEntities.Count == 0) {
            return interactableEntities[0].GetComponent<InteractableEntity>();
        }
        return GetClosestEntity();
    }

    // Calculates and returns the closest Entity within `interactableEntities`
    private InteractableEntity GetClosestEntity() {
        GameObject closestEntity = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject entity in interactableEntities) {
            float distance = Vector2.Distance(entity.transform.position, transform.position);
            if (distance < minDistance) {
                closestEntity = entity;
                minDistance = distance;
            }
        }
        return closestEntity.GetComponent<InteractableEntity>();
    }

    #region Collisions
    // On collision, adds the gameobject to `interactableEntities`.
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Entity") {
            interactableEntities.Add(other.gameObject);
        }
        UpdateCanInteract();
    }
    // On collisionExit, removes the gameobject from
    private void OnCollisionExit2D(Collision2D other) {
        if (interactableEntities.Contains(other.gameObject)) {
            interactableEntities.Remove(other.gameObject);
        }
        UpdateCanInteract();
    }

    private void UpdateCanInteract() {
        canInteract = interactableEntities.Count > 0;
    }
    #endregion
}
