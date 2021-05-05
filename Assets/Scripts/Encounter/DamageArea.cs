using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageArea : MonoBehaviour
{
    [Header("Damage Calculation")]
    public float xPos;
    [Range(0.0f, 1.0f)] public float centreSize = 0.16f;  // 1 game unit is 32x32 pixels in size, so 1 entire tile. 32px / 100 to get the size of 0.01 unit.
    public bool playerInsideCentre;

    GameObject player;

    private void Start() {
        xPos = transform.position.x;
        player = GameObject.Find("Player Object");
    }

    private void Update() {
        playerInsideCentre = Vector2.Distance(transform.position, player.transform.position) < centreSize;
        if (playerInsideCentre) {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else {
            GetComponent<SpriteRenderer>().color = Color.white;

        }
    }

    public void OnAttack(float playerXPos) {
        float minX = Mathf.Max(xPos, playerXPos);
        float maxX = Mathf.Min(xPos, playerXPos);

        Debug.Log($"{maxX} - {minX} = {maxX - minX}");
    }

}
