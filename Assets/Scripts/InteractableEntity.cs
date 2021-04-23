using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableEntity : MonoBehaviour
{
    [Header("Character information")]
    public string Name = "<NOT SET>";

    [Header("Dialogue Data")]
    public List<Dialogue> dialogues;
    public List<Sprite> dialogueImages;

    [Header("Debug")]
    [SerializeField]
    private int currentDialogueIndex;

    public void StartInteraction() {
        currentDialogueIndex = 0;
        
        DialogueManager.instance.StartDialogue(dialogues[currentDialogueIndex], this);
    }

    public void AdvanceInteraction() {

    }

    public void EndInteraction() {
        if (dialogues.Count > 1) {
            dialogues.RemoveAt(currentDialogueIndex);
        }
        return;
    }
}
