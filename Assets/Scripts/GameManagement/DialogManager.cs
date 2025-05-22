using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance = null;

    public TextMeshProUGUI dialogText;
    public GameObject playerUI;
    public GameObject dialogUI;

    private Coroutine currentDialogCoroutine;
    
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void StartDialog(string[] dialogLines)
    {
        playerUI.SetActive(false);
        dialogUI.SetActive(true);

        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
        }

        currentDialogCoroutine = StartCoroutine(ShowDialog(dialogLines));
    }

    private IEnumerator ShowDialog(string[] dialogLines)
    {
        foreach (var line in dialogLines)
        {
            yield return StartCoroutine(TypewriterEffect(line));
        }

        playerUI.SetActive(true);
        dialogUI.SetActive(false);
    }

    private IEnumerator TypewriterEffect(string line)
    {
        dialogText.text = "";

        foreach (char letter in line)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }
}
