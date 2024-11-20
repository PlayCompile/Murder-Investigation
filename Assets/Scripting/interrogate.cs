using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class interrogate : MonoBehaviour
{
    public InputActionAsset inputAss;
    public PlayerInput pInput;
    public bool schemePad = false;
    public AudioSource sadieTalk;
    public List<GameObject> txtOptions = new List<GameObject>();
    public List<GameObject> objGamepad = new List<GameObject>();
    public List<GameObject> objKeyboard = new List<GameObject>();
    public List<AudioClip> questions = new List<AudioClip>();
    public List<AudioClip> answers = new List<AudioClip>();
    public List<GameObject> autoHide = new List<GameObject>();
    private bool picked1 = false;
    private bool picked2 = false;
    private bool picked3 = false;
    private bool picked4 = false;
    private int currentOption = 0;
    private float endTime;
    private int stateMachine = 0;
    public Objectives objectives;
 
    void OnEnable()
    {
        inputAss.Enable();
    }


    void Update()
    {
        string currentScheme = pInput.currentControlScheme;
        if (objGamepad.Count > 0)
        {
            if (currentScheme == "Gamepad") { schemePad = true; objGamepad[0].transform.parent.gameObject.SetActive(true); objKeyboard[0].transform.parent.gameObject.SetActive(false); } 
            else { schemePad = false; objKeyboard[0].transform.parent.gameObject.SetActive(true); objGamepad[0].transform.parent.gameObject.SetActive(false); }
        }

        if (inputAss.FindAction("Triangle").WasPressedThisFrame() & picked1 == false)
        {
            hideOptions();
            GetComponent<AudioSource>().clip = questions[0];
            GetComponent<AudioSource>().Play();
            endTime = Time.timeSinceLevelLoad + questions[0].length;
            stateMachine = 1; picked1 = true;
            RemoveObjectByName(txtOptions, "Text (Legacy) (1)");
            RemoveObjectByName(objGamepad, "RawImage (1)");
            RemoveObjectByName(objKeyboard, "RawImage (1)");
            currentOption = 1;
        }

        if (inputAss.FindAction("Circle").WasPressedThisFrame() & picked2 == false)
        {
            hideOptions();
            GetComponent<AudioSource>().clip = questions[1];
            GetComponent<AudioSource>().Play();
            endTime = Time.timeSinceLevelLoad + questions[1].length;
            stateMachine = 1; picked2 = true;
            RemoveObjectByName(txtOptions, "Text (Legacy) (2)");
            RemoveObjectByName(objGamepad, "RawImage (2)");
            RemoveObjectByName(objKeyboard, "RawImage (2)");
            currentOption = 2;
        }

        if (inputAss.FindAction("Cross").WasPressedThisFrame() & picked3 == false)
        {
            hideOptions();
            GetComponent<AudioSource>().clip = questions[2];
            GetComponent<AudioSource>().Play();
            endTime = Time.timeSinceLevelLoad + questions[2].length;
            stateMachine = 1; picked3 = true;
            RemoveObjectByName(txtOptions, "Text (Legacy) (3)");
            RemoveObjectByName(objGamepad, "RawImage (3)");
            RemoveObjectByName(objKeyboard, "RawImage (3)");
            currentOption = 3;
        }

        if (inputAss.FindAction("Square").WasPressedThisFrame() & picked4 == false)
        {
            hideOptions();
            GetComponent<AudioSource>().clip = questions[3];
            GetComponent<AudioSource>().Play();
            endTime = Time.timeSinceLevelLoad + questions[3].length;
            stateMachine = 1; picked4 = true;
            RemoveObjectByName(txtOptions, "Text (Legacy) (4)");
            RemoveObjectByName(objGamepad, "RawImage (4)");
            RemoveObjectByName(objKeyboard, "RawImage (4)");
            currentOption = 4;
        }

        if (stateMachine == 1 & Time.timeSinceLevelLoad > endTime) { stateMachine = 2; sadieRespond(); }
        if (stateMachine == 2 & Time.timeSinceLevelLoad > endTime) 
        { 
            if (objGamepad.Count == 0)
            {
                objectives.clueFound = currentOption - 1;
                objectives.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
            {
                objectives.clueFound = currentOption-1;
                objectives.gameObject.SetActive(true);
                stateMachine = 0; showOptions();
            }
        }
    }

    void sadieRespond()
    {
        sadieTalk.clip = answers[currentOption - 1];
        sadieTalk.Play();
        endTime = Time.timeSinceLevelLoad + answers[currentOption - 1].length;
    }

    void RemoveObjectByName(List<GameObject> list, string objectName)
    {
        GameObject objectToRemove = null;

        // Iterate through the list and find the GameObject with the matching name
        foreach (GameObject obj in list)
        {
            if (obj.name == objectName)
            {
                objectToRemove = obj;
                break; // Stop searching once you find a match
            }
        }

        // Remove the object from the list if it was found
        if (objectToRemove != null)
        {
            list.Remove(objectToRemove);
            Destroy(objectToRemove); // Optionally destroy the GameObject
        }
    }

    void hideOptions()
    {
        foreach (GameObject obj in autoHide)
        {
            Transform objTransform = obj.transform;
            int childCount = objTransform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform child = objTransform.GetChild(i);
                child.gameObject.SetActive(false);
            }
        }
    }

    void showOptions()
    {
        foreach (GameObject obj in autoHide)
        {
            Transform objTransform = obj.transform;
            int childCount = objTransform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform child = objTransform.GetChild(i);
                child.gameObject.SetActive(true);
            }
        }
    }
}
