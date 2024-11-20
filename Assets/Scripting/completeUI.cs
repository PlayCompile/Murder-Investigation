using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class completeUI : MonoBehaviour
{
    public AudioClip summary;
    public GameObject activeOnComp;
    public GameObject activeOnComp2;
    public GameObject tipSearch;
    public clueSearch ClueSearch;
    public InputActionAsset inputAss;
    private float timeEnd;

    void OnEnable()
    {
        inputAss.Enable();
        GetComponent<AudioSource>().clip = summary;
        GetComponent<AudioSource>().Play();
        timeEnd = Time.timeSinceLevelLoad + GetComponent<AudioSource>().clip.length;
    }


    void Update()
    {
        if (Time.timeSinceLevelLoad > timeEnd)
        {
            activeOnComp.SetActive(true);
            activeOnComp2.SetActive(true);

            bool showTip = true;
            if (activeOnComp2.name == "part4") { ClueSearch.isChar = true; ClueSearch.gameObject.SetActive(true); showTip = false; }
            if (activeOnComp2.name == "part5") { showTip = false; }
            if (showTip) { tipSearch.SetActive(true); }

            this.gameObject.SetActive(false);
        }
    }
}
