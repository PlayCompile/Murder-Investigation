using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehaviour : MonoBehaviour
{
    public InputActionAsset inputAsss;
    public GameObject camStand;
    public GameObject camCrouch;
    public MissionWaypoint marker;
    public Animator tipCrouch;
    public GameObject tipClues;
    public GameObject clueSearch;
    public GameObject interactives;
    public bool taughtCrouch = false;

    void Start()
    {
        inputAsss.Enable();
    }

    void Update()
    {
        if (inputAsss.FindAction("Crouch").WasPressedThisFrame())
        {
            if (camStand.activeSelf)
            {// crouch
                taughtCrouch = true;
                camCrouch.SetActive(true);
                camStand.SetActive(false);
                if (marker.tipCondition == 2) { tipCrouch.Play("controlTipOff");
                    marker.tipCondition = 3; tipClues.SetActive(true); }
            }
            else
            {// stand
                camCrouch.SetActive(false);
                camStand.SetActive(true);
                tipClues.SetActive(false);
            }
        }

        if (inputAsss.FindAction("Search").WasPressedThisFrame() && taughtCrouch)
        {
            if (clueSearch.activeSelf == false)
            {// clue search on
                if (tipClues.activeSelf)
                {
                    tipClues.SetActive(false);
                    interactives.SetActive(true);
                }
                clueSearch.SetActive(true);
                
                tipCrouch.gameObject.SetActive(false);

            }
            else
            {// clue search off
                clueSearch.SetActive(false);
                tipCrouch.gameObject.SetActive(false);
            }
        }
    }
}