using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private float displayTime = 4f;

    [Header("Sound")]
    [SerializeField] private AudioSource typingSource;

    private bool isRunning = false;
    private Queue<string> sentences = new Queue<string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        EndDialogue();
    }
    public void StartDialogue(string[] lines)
    {
        foreach(string line in lines)
        {
            sentences.Enqueue(line);
        }

        if (!isRunning)
        {
            StartCoroutine(TypeNextSentence());
        }
    }
    private IEnumerator TypeNextSentence()
    {
        isRunning = true;
        tutorialPanel.SetActive(true);

        while (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            tutorialText.text = sentence;
            tutorialText.maxVisibleCharacters = 0;
            yield return null;
            int totalVisibleCharacters = tutorialText.textInfo.characterCount;

            for(int i = 0; i <= totalVisibleCharacters; i++)
            {
                tutorialText.maxVisibleCharacters = i;
                if (typingSource != null && i < totalVisibleCharacters)
                {
                    typingSource.pitch = Random.Range(0.9f, 1.1f);
                    typingSource.Play();
                }
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(displayTime);
        }
        EndDialogue();
    }
    private void EndDialogue()
    {
        isRunning = false;
        tutorialPanel.SetActive(false);
        tutorialText.text = "";
    }
}
