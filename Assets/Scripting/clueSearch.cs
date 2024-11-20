using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class clueSearch : MonoBehaviour
{
    public interactive Interactive;
    public GameObject cameraRoot;
    public GameObject mainCamera;
    public GameObject standCam;
    public GameObject crouchCam;
    public CinemachineCamera activeCamera;

    public Texture texNoClue;
    public Texture texClue;
    public RawImage magnifier;
    public bool isChar = false;

    void Update()
    {
        if (isChar) { GetComponent<RawImage>().enabled = false; } else { GetComponent<RawImage>().enabled = true; }
        if (standCam.activeSelf == true) { activeCamera = standCam.GetComponent<CinemachineCamera>(); }
        else { activeCamera = crouchCam.GetComponent<CinemachineCamera>(); }

        Vector3 cameraForward = cameraRoot.transform.forward;

        float rayLength = 5f; // the length of the ray

     //   Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * rayLength, Color.blue); //start at the position of the camera and then set the direction to the way the camera is facing

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward * rayLength);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
      //      Debug.Log(hit.collider.name + " , " + hit.distance);
            if (hit.collider.name.StartsWith("interact"))
            {
                Interactive.inRange = true;
                Interactive.objectSender = hit.collider.gameObject;
                Interactive.gameObject.SetActive(true);
                magnifier.texture = texClue;
            }
        }
        else
        {
            Interactive.inRange = false;
            Interactive.objectSender = null;
            Interactive.gameObject.SetActive(false);
            magnifier.texture = texNoClue;

        }
    }
}
