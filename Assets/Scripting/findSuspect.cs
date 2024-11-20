using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class findSuspect : MonoBehaviour
{
    public MissionWaypoint marker;
    public GameObject sadiePos;
    public RawImage disableTip;

    void OnEnable()
    {
        marker.location = sadiePos;
        disableTip.enabled = false;
    }
}
