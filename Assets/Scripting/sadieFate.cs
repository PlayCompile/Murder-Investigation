using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class sadieFate : MonoBehaviour
{
    public PlayerInput pInput;
    public InputActionAsset inputAss;
    public GameObject padControls;
    public GameObject keyControls;
    public float fillSpeed = 0.05f;
    private float holdTime;
    private int lastHold = -1;
    public GameObject scalePivot;
    private AudioSource audioVerdict;
    public AudioSource soundOutcome;
    public AudioClip outcomeGuilty;
    public AudioClip outcomeInnocent;
    public GameObject tipSearch;
    public GameObject jailCell;
    public GameObject finalG;
    public GameObject finalI;
    private bool allowFinish = false;

    void OnEnable()
    {
        inputAss.Enable();
        audioVerdict = transform.Find("verdict").GetComponent<AudioSource>();
        holdTime = 0f;
        padControls.transform.Find("gFill").GetComponent<Image>().fillAmount = 0f;
        padControls.transform.Find("iFill").GetComponent<Image>().fillAmount = 0f;
        keyControls.transform.Find("gFill").GetComponent<Image>().fillAmount = 0f;
        keyControls.transform.Find("iFill").GetComponent<Image>().fillAmount = 0f;
    }

    void OnDisable()
    {
        padControls.transform.Find("gFill").GetComponent<Image>().fillAmount = 0f;
        padControls.transform.Find("iFill").GetComponent<Image>().fillAmount = 0f;
        keyControls.transform.Find("gFill").GetComponent<Image>().fillAmount = 0f;
        keyControls.transform.Find("iFill").GetComponent<Image>().fillAmount = 0f;
    }


    void Update()
    {
        tipSearch.SetActive(false);
        string currentScheme = pInput.currentControlScheme;
        if (currentScheme == "Gamepad") 
        { padControls.SetActive(true); keyControls.SetActive(false); }
        if (currentScheme == "KeyboardMouse") { keyControls.SetActive(true); padControls.SetActive(false); }


        if (inputAss.FindAction("Guilty").IsPressed())
        {
            if (audioVerdict.isPlaying) { } else { audioVerdict.Play(); }
            if (lastHold == 1) { holdTime = 0f; }
            padControls.transform.Find("gFill").GetComponent<Image>().fillAmount = holdTime;
            keyControls.transform.Find("gFill").GetComponent<Image>().fillAmount = holdTime;

            holdTime = Mathf.Clamp01(holdTime + (fillSpeed * Time.deltaTime));
            if (holdTime >= 1f) { allowFinish = true; StartCoroutine(giveVerdict(0)); }
            adjustScales(holdTime);
            lastHold = 0;
        }
        else
        {
            if (lastHold == 0)
            {
                if (audioVerdict.isPlaying & !allowFinish) { audioVerdict.Stop(); } else {  }

                padControls.transform.Find("gFill").GetComponent<Image>().fillAmount = holdTime;
                keyControls.transform.Find("gFill").GetComponent<Image>().fillAmount = holdTime;
                holdTime = Mathf.Clamp01(holdTime - (fillSpeed * (Time.deltaTime) * 1.6f));
                adjustScales(holdTime);
            }
        }
        if (holdTime < 0) { 
            holdTime = 0f;
            padControls.transform.Find("gFill").GetComponent<Image>().fillAmount = holdTime;
            keyControls.transform.Find("gFill").GetComponent<Image>().fillAmount = holdTime;
            adjustScales(holdTime);
        }

        if (inputAss.FindAction("Innocent").IsPressed())
        {
            if (audioVerdict.isPlaying) { } else { audioVerdict.Play(); }

            if (lastHold == 0) { holdTime = 0f; }
            padControls.transform.Find("iFill").GetComponent<Image>().fillAmount = holdTime;
            keyControls.transform.Find("iFill").GetComponent<Image>().fillAmount = holdTime;

            holdTime = Mathf.Clamp01(holdTime + (fillSpeed * Time.deltaTime));
            if (holdTime >= 1f) { allowFinish = true; StartCoroutine(giveVerdict(1)); }
            adjustScales(holdTime - (holdTime * 2));
            lastHold = 1;
        }
        else
        {
            if (lastHold == 1)
            {
                if (audioVerdict.isPlaying & !allowFinish) { audioVerdict.Stop(); } else {  }

                padControls.transform.Find("iFill").GetComponent<Image>().fillAmount = holdTime;
                keyControls.transform.Find("iFill").GetComponent<Image>().fillAmount = holdTime;
                holdTime = Mathf.Clamp01(holdTime - (fillSpeed * (Time.deltaTime) * 1.6f));
                adjustScales(holdTime - (holdTime * 2));
            }
        }
        if (holdTime < 0)
        {
            holdTime = 0f;
            padControls.transform.Find("iFill").GetComponent<Image>().fillAmount = holdTime;
            keyControls.transform.Find("iFill").GetComponent<Image>().fillAmount = holdTime;
            adjustScales(holdTime - (holdTime * 2));
        }
    }

    void adjustScales(float fillAmount)
    {
        float minAngle = 0f;
        float maxAngle = 0f;
        if (fillAmount >= 0)
        {
            minAngle = 0f;
            maxAngle = 15f;
        }
        else
        {
            minAngle = 0f;
            maxAngle = -15f;
            fillAmount = -fillAmount; // Take the absolute value for the negative range
        }
        float mappedAngle = Mathf.Lerp(minAngle, maxAngle, fillAmount);
        scalePivot.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, mappedAngle);
    }


    IEnumerator giveVerdict(int decision)
    {
        GetComponent<Animator>().Play("fateChosen");

        yield return new WaitForSeconds(4f);
        // 0 = Guilty,  1= Innocent
        if (decision == 0)
        {
            soundOutcome.clip = outcomeGuilty;
        } else { soundOutcome.clip = outcomeInnocent; }
        soundOutcome.Play();
        StartCoroutine (loadOutcome(decision));
    }

    IEnumerator loadOutcome(int decision)
    {
        float waitTime = soundOutcome.clip.length;
        yield return new WaitForSeconds(waitTime);
        if (decision == 0) { jailCell.SetActive(true); finalG.SetActive(true); }
        else { finalI.SetActive(true); }
        yield return null;
        this.gameObject.SetActive(false);
    }
}
