using UnityEngine;
using UnityEngine.SceneManagement;

public class startup : MonoBehaviour

{
    public GameObject music;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(music);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > 10f) { SceneManager.LoadScene("enviro"); }
    }
}
