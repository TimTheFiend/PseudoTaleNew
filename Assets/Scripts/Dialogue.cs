using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("ConversationID")]
    public int id;

    [Header("Settings")]
    public bool isRepeatable;
    [TextArea(3, 3)]
    public string[] sentences;

}
