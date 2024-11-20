using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameConsoleAI : MonoBehaviour
{
    public GameObject floppyLight;
    private float nextOn;
    private float nextOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void calcNext()
    {
        nextOn = Time.timeSinceLevelLoad + Random.Range(0.8f, 3.8f);
        nextOff = nextOn + Random.Range(0.8f, 3.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > nextOn & floppyLight.activeSelf == false) { floppyLight.SetActive(true); }
        if (Time.timeSinceLevelLoad > nextOff & floppyLight.activeSelf == true) { floppyLight.SetActive(false); calcNext(); }

    }
}
