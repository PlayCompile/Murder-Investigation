using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    public string mainObjective = "Investigate the body";
    public List<string> clues = new List<string>();
    public List<bool> foundClue = new List<bool>();
    private List<Text> txtClues = new List<Text>();
    public AudioSource typingSFX;
    public Text txtObjective;
    public GameObject cluePrefab;
    public int clueFound = -1;
    private int cluesFound = 0;
    public GameObject completeUI;
    public AudioClip summary;
    public GameObject standCam;
    public GameObject crouchCam;
    public GameObject search;
    public GameObject nextObjectives;
    public GameObject nextInteractives;
    public GameObject interactives;

    void OnEnable()
    {
        StartCoroutine(TypeText(mainObjective, txtObjective));
        txtClues.Clear();
        foreach (string clue in clues)
        {
            foundClue.Add(false);
            GameObject newClue = Instantiate(cluePrefab);
            newClue.transform.parent = transform.Find("CLUES");
            newClue.name = clue;
            newClue.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            newClue.GetComponent<Text>().text = "???";
            txtClues.Add(newClue.GetComponent<Text>());
        }
    }

    IEnumerator TypeText(string theText, Text txtComponent)
    {
        typingSFX.Play();
        txtComponent.text = "";
        // Type text one character at a time to the txtComponent
        for (int i = 0; i < theText.Length; i++)
        {
            txtComponent.text += theText[i];
            yield return new WaitForSeconds(0.07f); // Adjust the typing speed as needed
        }
        typingSFX.Stop();
        if (cluesFound == clues.Count) { showComplete(); }
        yield return null;
    }

    void showComplete() 
    {
        completeUI.GetComponent<completeUI>().summary = summary;
        completeUI.GetComponent<completeUI>().activeOnComp = nextObjectives;
        completeUI.GetComponent<completeUI>().activeOnComp2 = nextInteractives;

        completeUI.SetActive(true);
        crouchCam.SetActive(false);
        standCam.SetActive(true);
        search.SetActive(false);
        interactives.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (clueFound > -1) { MarkClueAsFound(clueFound);
            clueFound = -1;
        }
    }

    public void MarkClueAsFound(int clueIndex)
    {
        if (foundClue[clueIndex] == false)
        {
            foundClue[clueIndex] = true;
            cluesFound++;
        }
        StartCoroutine(TypeText(clues[clueIndex], txtClues[clueIndex]));
    }
}
