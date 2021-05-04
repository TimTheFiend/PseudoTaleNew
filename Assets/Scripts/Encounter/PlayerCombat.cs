using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Player Variables")]
    [Range(1f, 10f)] public float playerSpeed = 5f;
    [Range(1f, 10f)] public float waitTimeForMovement = 1f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject damageArea;

    private float playerWidth;  // Not sure if needed with current system
    private float currentPlayerSpeed;  // Used internally to set speed
    private Vector2 playerStartPosition;


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerWidth = transform.localScale.x / 2f;  // Half width
        playerStartPosition = transform.position;

        StartCoroutine(InitializePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {

            StartCoroutine(_PlayerAttack());
            //PlayerAttack();
        }
    }

    // Moves the player from startPosition and towards the right
    private void MovePlayer() {
        rbody.velocity = new Vector2(1f * currentPlayerSpeed, 0f);
    }

    private IEnumerator _PlayerAttack() {
        currentPlayerSpeed = 0;
        int foobar = 100;

        sprite.color = Color.blue;
        for (int i = 0; i < foobar; i++) {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.0f);
            sprite.color = new Color(0f, 1f, 1f, 1 - (1 / 100));
            yield return new WaitForSeconds(0.01f);
        }

        sprite.color = Color.white;
        StartCoroutine(InitializePlayer());

        //yield return new WaitForSeconds(2.5f);
    }

    //TODO
    private void PlayerAttack() {
        transform.localPosition = playerStartPosition;

        StartCoroutine(InitializePlayer());
    }

    // Ensures the player has time to react before `Update`
    private IEnumerator InitializePlayer() {
        transform.localPosition = playerStartPosition;
        currentPlayerSpeed = 0;

        Debug.Log("START");
        yield return new WaitForSeconds(waitTimeForMovement);
        Debug.Log("END");

        currentPlayerSpeed = playerSpeed;
    }

    // Sets the gameobject to damageArea for damagecalculation
    private void OnTriggerEnter2D(Collider2D other) {
        damageArea = other.gameObject;
    }

    // Removes the damageArea reference, unless it's not itself (in the case of overlapping Colliders)
    private void OnTriggerExit2D(Collider2D other) {
        if (damageArea == other.gameObject) {
            damageArea = null;
        }
    }
}
