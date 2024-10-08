using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// CPSC 386 Example04 SceneSwitcher.cs (LoadScene())
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html
public class GameUI : MonoBehaviour
{
    Label pointsLabel;
    int points = 0;
    Label livesLabel;
    int lives = 3;

    private void OnEnable()
    {
        // Find UI Document on game object
        var UIDoc = GetComponent<UIDocument>();

        // Find Points and Lives Labels in UI Document as Labels
        pointsLabel = UIDoc.rootVisualElement.Q("Points") as Label;
        livesLabel = UIDoc.rootVisualElement.Q("Lives") as Label;
    }

    // Function called in Stars Script
    public void IncrementPoints()
    {
        // Increase current points by 1 and update points label to this new number
        points++;
        pointsLabel.text = "Points: " + points.ToString();
    }

    // Function called in Meteor Script
    public void DecrementLives()
    {
        // Decrease current lives by 1
        lives--;
        // Check if lives is less than zero, if so then load game over scene
        if (lives < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // otherwise, update lives label to new lives count
            livesLabel.text = "Lives: " + lives.ToString();
        }
    }
}
