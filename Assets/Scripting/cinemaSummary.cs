using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class cinemaSummary : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>();
    public List<float> timings = new List<float>();
    public CinemachineBrain cineBrain;
    private int camIndex = 0;
    private float startTime;
    private float nextCut;
    public GameObject objectives;
    public GameObject wooshSFX;


    // Start is called before the first frame update
    void Start()
    {
        int index = 1;
        while (index < cameras.Count)
        {
            cameras[index].SetActive(false);
            index++;
        }
        cameras[0].SetActive(true);
        nextCut = timings[0];
        startTime = Time.timeSinceLevelLoad;
    }


    void Update()
{       if (Time.timeSinceLevelLoad > 6.4f & wooshSFX.activeSelf == false)
        {
            wooshSFX.SetActive(true);
        }
        if (Time.timeSinceLevelLoad > (startTime + timings[camIndex]))
        {

                if (camIndex == cameras.Count - 1)
            {
                cameras[camIndex].SetActive(false);
                objectives.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
            {
                cameras[camIndex].SetActive(false);
                camIndex++;
                cameras[camIndex].SetActive(true);

            }
        }
    }
}
