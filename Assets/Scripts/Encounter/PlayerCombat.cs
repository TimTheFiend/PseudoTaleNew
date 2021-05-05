using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerCombat : MonoBehaviour
{
    #region Components
    private Rigidbody2D rbody;
    private SpriteRenderer sprite;
    #endregion

    [Header("Debug")]
    [SerializeField] private GameObject damageArea;  // Which `Encounter.DamageArea` object `PlayerCombat` is currently inside
    [SerializeField] private bool isPlayerTurn;  // if true, user input is registered
    
    [Header("Player Variables")]
    [Range(1f, 10f)] public float playerSpeed = 5f;
    private Vector2 _playerStartPosition;

    [Header("\"Animation vars\"")]
    [Range(0.1f, 5f)] public float actionLengthInSec = 2.5f;    // Length in seconds of end-of-turn "animation".
    [Range(1, 100)] public int actionLengthInFrames = 50;       // Length in frames of end-of-turn "animation". Higher value = smoother transition
    [Range(1f, 10f)] public float waitTimeForMovement = 1f;     // Length in seconds inputs are registered.
    [SerializeField] [Range(1.0f, 2.0f)] private float endPlayerSize = 1.4f;
    


    private float _playerWidth;  // Not sure if needed with current system

    #region Awake, Start, Update
    void Start()
    {
        #region Set Initial Variables
        // Components
        rbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        // Player Variables
        _playerWidth = transform.localScale.x / 2f;  // Half width, side to center
        _playerStartPosition = transform.position;
        #endregion

        ResetPlayer();
    }

    void Update()
    {
        MovePlayer();

        if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) && isPlayerTurn) {
            StartCoroutine(PlayerAttack());
        }
    }
    #endregion

    /// <summary>Moves the <see cref="Player"/> object along the x-axis, at <see cref="playerSpeed"/> speed, until
    /// either <see cref="Player"/> collides with a non-trigger collider, or <see cref="PlayerAttack"/> is called.
    /// </summary>
    private void MovePlayer() {
        rbody.velocity = new Vector2(1f * (isPlayerTurn ? playerSpeed : 0), 0f);
    }


    #region Coroutines
    /// <summary> Ends player turn, displays the `animation`, and resets player.</summary>
    private IEnumerator PlayerAttack() {
        isPlayerTurn = false;

        if (damageArea != null) {
            damageArea.GetComponent<DamageArea>().OnAttack(transform.position.x);
        }

        // Update sprite opacity and size
        for (float i = 0.0f; i < actionLengthInSec; i += actionLengthInSec / actionLengthInFrames) {
            float newSizeXY = Mathf.Lerp(1, 1 * endPlayerSize, i); // new size
            
            sprite.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, i));  // * -1 to make the value negative
            transform.localScale = new Vector3(newSizeXY, newSizeXY);
            
            yield return new WaitForSeconds(actionLengthInSec / actionLengthInFrames);
        }
        ResetPlayer();

    }

    /// <summary> Sets <see cref="isPlayerTurn"/> to false, runs the "animation", and resets <see cref="PlayerCombat"/>.</summary>
    private IEnumerator PlayerLeavesArea() {
        isPlayerTurn = false;  // Player input disabled.
        try {
            StopCoroutine("PlayerAttack");
        }
        catch (Exception) {

            throw;
        }

        for (float i = 0.0f; i < actionLengthInSec; i += actionLengthInSec / actionLengthInFrames) {
            sprite.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, i));  // * -1 to make the value negative
            yield return new WaitForSeconds(actionLengthInSec / actionLengthInFrames);
        }
        ResetPlayer();
    }

    
    /// <summary> /// Easier to call than <code>StartCoroutine(ResetPlayerPositionForCombat());</code></summary>
    private void ResetPlayer() {
        StartCoroutine(ResetPlayerPositionForCombat());
    }

    /// <summary> Sets and prepares default values for Player's turn. </summary>
    private IEnumerator ResetPlayerPositionForCombat() {  // Name is intentionally long to justify `ResetPlayer`s existance
        transform.localScale = Vector2.one;
        transform.position = _playerStartPosition;
        sprite.color = new Color(1, 1, 1, 0);
        
        // Countdown to isPlayerTurn
        yield return new WaitForSeconds(waitTimeForMovement);
        sprite.color = Color.white; 
        isPlayerTurn = true;
    }
    #endregion

    #region Collision functions
    // Conditional makes it so, no matter what, this only happens once per player turn
    private void OnCollisionEnter2D(Collision2D collision) {
        if (isPlayerTurn) {
            StartCoroutine(PlayerLeavesArea());
        }
    }

    // Sets the gameobject to damageArea for damagecalculation
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("EncounterTarget")) {
            damageArea = other.gameObject;
        }
    }

    // Removes the damageArea reference, unless it's not itself (in the case of overlapping Colliders)
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == damageArea) {
            damageArea = null;
        }
    }
#endregion
}
