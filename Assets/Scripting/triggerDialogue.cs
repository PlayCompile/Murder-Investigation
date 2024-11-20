using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class triggerDialogue : MonoBehaviour
{
    public GameObject camera;
    private AudioSource audio;
    private float timeStart;
    public GameObject dialogueTree;
    public GameObject treeCloses;
    private InputActionAsset inputAss;
    public GameObject search;
    public GameObject player;
    public MissionWaypoint marker;

    void OnEnable()
    {
        inputAss = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions;
        inputAss.Enable();
        camera.SetActive(true);
        search.SetActive(false);
        audio = camera.GetComponent<AudioSource>();
        timeStart = Time.timeSinceLevelLoad;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<FirstPersonController>().enabled = false;
        marker.location = null;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - timeStart > audio.clip.length)
        {
            dialogueTree.SetActive(true);
            treeCloses.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
