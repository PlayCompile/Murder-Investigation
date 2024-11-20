using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraTransitionDetector : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain;
    public GameObject player;
    public List<CinemachineCamera> playerCams = new List<CinemachineCamera>();

    private void Start()
    {
        // Make sure to set the references to the Cinemachine Brain and Player Input script in the Inspector.
        if (cinemachineBrain == null)
        {
            cinemachineBrain = FindFirstObjectByType<CinemachineBrain>();
        }
    }

    private void Update()
    {
        // Check if the Cinemachine Brain is blending between two cameras.
        bool isTransitioning = cinemachineBrain.IsBlending;

        // Disable or enable player input based on the camera transition state.
        if (player != null)
        {
            if (isTransitioning)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<FirstPersonController>().enabled = false;
            }
            else
            {
                foreach (CinemachineCamera cam in playerCams)
                {
                    if (ReferenceEquals(cinemachineBrain.ActiveVirtualCamera, cam))
                    {
                        player.GetComponent<CharacterController>().enabled = true;
                        player.GetComponent<FirstPersonController>().enabled = true;
                    }
                }
            }
        }
    }
}
