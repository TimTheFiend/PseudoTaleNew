using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Element")]
    public GameObject dialogueBox;

    [Header("Text Only")]
    public Text textOnly;

    [Header("Text + Image")]
    public GameObject textImage;
    public Text text;
    public Image image;

    public static DialogueManager instance = null;

    //public Text dialogueText;


    private Queue<string> sentences;

    private void Awake() {
        dialogueBox.SetActive(false);

        text = textImage.GetComponent<Text>();
        image = textImage.GetComponent<Image>();


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

    public void StartDialogue(Dialogue dialogue) {
        dialogueBox.SetActive(true);

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
        textOnly.text = "";

        float width = 0;
        float maxWidth = textOnly.rectTransform.rect.width;
        var textGen = textOnly.cachedTextGenerator;
        var textSettings = textOnly.GetGenerationSettings(Vector2.zero);

        

        foreach (string word in sentence.Split(' ')) {
            width += textGen.GetPreferredWidth(word, textSettings);
            if (width >= maxWidth) {
                textOnly.text += "\n";
            }

            foreach (char letter in (word + " ").ToCharArray()) {
                textOnly.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }

    }

    public void EndDialogue() {
        dialogueBox.SetActive(false);
    }
}
