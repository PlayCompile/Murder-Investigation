using UnityEngine;
using UnityEngine.InputSystem;

public class triggerClue : MonoBehaviour
{
    public GameObject camera;
    private AudioSource audio;
    private float timeStart;
    public clueSearch ClueSearch;
    private GameObject activeCamera;
    public Objectives objectives;
    private InputActionAsset inputAss;
    void OnEnable()
    {
        inputAss = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions;
        inputAss.Enable();

        transform.parent.transform.Find("particles").gameObject.SetActive(false);
        activeCamera = ClueSearch.activeCamera.gameObject;
        activeCamera.SetActive(false);

        camera.SetActive(true);
        audio = camera.GetComponent<AudioSource>();
        timeStart = Time.timeSinceLevelLoad;
        ClueSearch.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - timeStart > audio.clip.length)
        {
            camera.SetActive(false);
            activeCamera.SetActive(true);
            objectives.clueFound = GetComponentInParent<interactiveObject>().clueIndex;
            objectives.gameObject.SetActive(true);
            ClueSearch.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}