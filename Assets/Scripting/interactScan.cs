using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactScan : MonoBehaviour
{
    public interactive Interactive;
    public GameObject cameraRoot;
    public GameObject mainCamera;

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraForward = cameraRoot.transform.forward;

        float rayLength = 15f; // the length of the ray

           Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward* rayLength, Color.blue); //start at the position of the camera and then set the direction to the way the camera is facing

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward * rayLength);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            Debug.Log(hit.collider.name + " , " + hit.distance);

            if (hit.collider.name.StartsWith("interact"))
            {
                Interactive.inRange = true;
                Interactive.objectSender = hit.collider.gameObject;
                Interactive.gameObject.SetActive(true);
            }

        }
        else
        {
            Interactive.inRange = false;
            Interactive.objectSender = null;
            Interactive.gameObject.SetActive(false);
        }
    }
}
