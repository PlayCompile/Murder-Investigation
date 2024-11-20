using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class waypointArrow : MonoBehaviour
{
    public int arrowStyle = 0;
    public Sprite arrow0; //down
    public Sprite arrow1; //up
    public Sprite arrow2; //right
    public Sprite arrow3; //left
    public FirstPersonController FPS;
    public CharacterController charCtrl;

    void Update()
    {
        FPS.enabled = true;
        charCtrl.enabled = true;
        if (arrowStyle == 0) { GetComponent<Image>().sprite = arrow0; }
        if (arrowStyle == 1) { GetComponent<Image>().sprite = arrow1; }
        if (arrowStyle == 2) { GetComponent<Image>().sprite = arrow2; }
        if (arrowStyle == 3) { GetComponent<Image>().sprite = arrow3; }
    }
}
