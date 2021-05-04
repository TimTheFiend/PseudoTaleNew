using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageArea : MonoBehaviour
{
    [Header("Damage Variables")]
    [Range(0.0f, 1.0f)]
    public float damageModifier;
    [Range(0.0f, 1.0f)]
    public float critModifier;
}
