using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MissionWaypoint : MonoBehaviour
{
    // Indicator icon
    public Image img;
    public GameObject location;
    private GameObject lastLocation;
    // The target (location, enemy, etc..)
    private Transform target;
    // To adjust the position of the icon
    public Vector3 offset;
    public waypointArrow WaypointArrow;
    private int nowDistance;
    public int autoHideDistance;
    public bool teleportCheat = false;
    public Transform Sadie;
    private float sadieDistance; // Use to auto-hide Sadie when too close
    public int tipCondition = 0;
    public GameObject showCrouch;

    private void Update()
    {
        if (location != null)
        {
            if (location != lastLocation)
            {
                lastLocation = location;
            }

            WaypointArrow.gameObject.SetActive(true);
            target = location.transform;
            if (nowDistance > autoHideDistance)
            {
                WaypointArrow.gameObject.SetActive(true);
                float minX = img.GetPixelAdjustedRect().width / 2;
                float maxX = Screen.width - minX;

                float minY = img.GetPixelAdjustedRect().height / 2;
                float maxY = Screen.height - minY;

                Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

                WaypointArrow.arrowStyle = 0;

                // Check if the target is behind us, to only show the icon once the target is in front
                if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
                {
                    // Check if the target is on the left side of the screen
                    if (pos.x < Screen.width / 2)
                    {
                        // Place it on the right (Since it's behind the player, it's the opposite)
                        pos.x = maxX;
                        WaypointArrow.arrowStyle = 3;
                    }
                    else
                    {
                        // Place it on the left side
                        pos.x = minX;
                        WaypointArrow.arrowStyle = 2;

                    }
                }
                else { WaypointArrow.arrowStyle = 0; }

                // Limit the X and Y positions
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.y = Mathf.Clamp(pos.y, minY, maxY);
                if (pos.x == minX) { WaypointArrow.arrowStyle = 2; }
                if (pos.x == maxX) { WaypointArrow.arrowStyle = 3; }
                if (pos.y == maxY) { WaypointArrow.arrowStyle = 1; }

                // Update the marker's position
                img.transform.position = pos;
                // Change the meter text to the distance with the meter unit 'm'
                if (tipCondition == 0) { tipCondition = 1; }
            }
            else
            {
                WaypointArrow.gameObject.SetActive(false);
                if (tipCondition == 1)
                {
                    showCrouch.SetActive(true); tipCondition++;
                    location = null;
                }
            }
            nowDistance = ((int)Vector3.Distance(target.position, transform.position));
            sadieDistance = ((int)Vector3.Distance(Sadie.position, transform.position));
        }
        else { WaypointArrow.gameObject.SetActive(false); }
    }
}
