using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance = null;
    private InteractableEntity currentEntity;

    [Header("UI Element")]
    public GameObject dialogueBox;

    [Header("Text Only")]
    public Text textOnly;

    [Header("Text + Image")]
    public Text textImage;
    public Image image;

    private bool hasImage = false;
    private Text currentText;

    private Queue<string> sentences;

    private void Awake() {
        dialogueBox.SetActive(false);

        textOnly.gameObject.SetActive(false);
        textImage.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }

    void Start()
    {
        #region Singleton pattern
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        #endregion

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, InteractableEntity entity) {
        dialogueBox.SetActive(true);

        currentEntity = entity;

        hasImage = entity.dialogueImages.Count > 0;

        currentText = hasImage ? textImage : textOnly;

        //switch (currentEntity.entityType) {
        //    case EntityEnum.NOTSET:
        //        Debug.Log("ENTITY TYPE NOT SET!!!!");
        //        break;
        //    case EntityEnum.Object:
        //        currentText = textOnly;
        //        hasImage = false;
        //        break;
        //    case EntityEnum.Entity:
        //        currentText = textImage;
        //        hasImage = true;
        //        image.gameObject.SetActive(true);
        //        break;
        //}

        image.gameObject.SetActive(hasImage);
        currentText.gameObject.SetActive(true);

        sentences.Clear();  // Clear sentences

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        AdvanceDialogue();
    }

    public bool AdvanceDialogue() {
        if (sentences.Count == 0) {
            EndDialogue();
            return false;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return true;
    }

    private IEnumerator TypeSentence(string sentence) {
        currentText.text = "";

        var textGen = currentText.cachedTextGenerator;
        var textSettings = currentText.GetGenerationSettings(Vector2.zero);
        
        float width = 0;
        float maxWidth = textGen.rectExtents.width;
        //float maxWidth = currentText.rectTransform.rect.width;

       

        if (hasImage) {
            int startIndex = sentence.IndexOf('[');
            if (startIndex != -1) {
                string setting = sentence.Substring(startIndex + 1, sentence.IndexOf(']') - 1);

                image.sprite = currentEntity.dialogueImages.Find(x => x.name == setting);
                sentence = sentence.Substring(sentence.IndexOf(']') + 1);
            }
        }

        foreach (string word in sentence.Split(' ')) {
            width += textGen.GetPreferredWidth(word + " ", textSettings);   // + ' ' because otherwise we only calculate where we are in the text box based only on letters.

            if (width >= maxWidth) {
                currentText.text += "\n";
                Debug.Log($"Width: {width} -- MaxWidth: {maxWidth}");
                width = textGen.GetPreferredWidth(word + " ", textSettings);
            }

            foreach (char letter in (word + " ").ToCharArray()) {
                currentText.text += letter;
                //yield return new WaitForSeconds(0.00f);
                yield return new WaitForSeconds(0.025f);
            }
        }

    }

    public void EndDialogue() {
        dialogueBox.SetActive(false);
    }
}
