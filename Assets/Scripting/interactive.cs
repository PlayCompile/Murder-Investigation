using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class interactive : MonoBehaviour
{
    public float fillSpeed = 0.05f;
    public GameObject objectSender;
    public bool inRange = false;
    public bool checkObject = false;
    public Text txtPrompt;
    public GameObject btnFill;
    public GameObject btnPrompt;
    public GameObject keyFill;
    public GameObject keySlash;
    public Texture texNoClue;
    public Texture texClue;
    public RawImage magnifier;
    public InputActionAsset inputAss;
    public PlayerInput pInput;
    public bool schemePad = false;
    private float holdTime;
    public clueSearch ClueSearch;

    void OnEnable()
    {
        holdTime = 0f;
        btnFill.GetComponent<Image>().fillAmount = 0f;
        keyFill.GetComponent<Image>().fillAmount = 0f;
    }

    void OnDisable()
    {
        btnFill.GetComponent<Image>().fillAmount = 0f;
        keyFill.GetComponent<Image>().fillAmount = 0f;
    }

    void startInteraction()
    {
        objectSender.GetComponent<interactiveObject>().objectTrigger.SetActive(true);
        ClueSearch.activeCamera.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        string currentScheme = pInput.currentControlScheme;
        if (currentScheme == "Gamepad") { schemePad = true; btnFill.SetActive(true); btnPrompt.SetActive(true); keyFill.SetActive(false); keySlash.SetActive(false); }
        if (currentScheme == "KeyboardMouse") { schemePad = false; btnFill.SetActive(false); btnPrompt.SetActive(false); keyFill.SetActive(true); keySlash.SetActive(true); }

        if (objectSender != null)
        {
            txtPrompt.text = objectSender.GetComponent<interactiveObject>().objName;
            btnFill.SetActive(true);
            btnPrompt.SetActive(true);
            magnifier.texture = texClue;

            if (inputAss.FindAction("Interact").IsPressed())
            {
                btnFill.GetComponent<Image>().fillAmount = holdTime;
                holdTime = Mathf.Clamp01(holdTime + (fillSpeed * Time.deltaTime));
                if (holdTime >= 1f) { startInteraction(); }
            }
            else 
            {
                btnFill.GetComponent<Image>().fillAmount = holdTime;
                holdTime = Mathf.Clamp01(holdTime - (fillSpeed * (Time.deltaTime) * 1.6f));
            }
            if (holdTime < 0) { holdTime = 0f; btnFill.GetComponent<Image>().fillAmount = holdTime; }
        }
        else
        {
            txtPrompt.text = "";
            btnFill.SetActive(false);
            btnPrompt.SetActive(false);
            magnifier.texture = texNoClue;
        }
    }
}
