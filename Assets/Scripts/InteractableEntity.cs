using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEntity : MonoBehaviour
{
    [Header("Character information")]
    public string Name = "<NOT SET>";

    [Header("Dialogue Files")]
    public List<Dialogue> dialogues;

    [Header("Debug")]
    [SerializeField]
    private int currentDialogueIndex;

    private void Awake() {
    }

    public void StartInteraction() {
        currentDialogueIndex = 0;
        
        DialogueManager.instance.StartDialogue(dialogues[currentDialogueIndex]);
    }

    public void EndInteraction() {
        if (!dialogues[currentDialogueIndex].isRepeatable) {
            dialogues.RemoveAt(currentDialogueIndex);
        }
    }
}
