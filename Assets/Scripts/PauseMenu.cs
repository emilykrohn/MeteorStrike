using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// https://docs.unity3d.com/ScriptReference/Input.GetKeyDown.html (Check if esc key is pressed)
// https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/ (Pause game when menu open)

public class PauseMenu : MonoBehaviour
{
    UIDocument uiDoc;
    // Start is called before the first frame update
    void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiDoc.enabled = true;
            Time.timeScale = 0;
        }
    }
}
