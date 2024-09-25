using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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
        var UIDoc = GetComponent<UIDocument>();

        pointsLabel = UIDoc.rootVisualElement.Q("Points") as Label;
        livesLabel = UIDoc.rootVisualElement.Q("Lives") as Label;
    }

    public void IncrementPoints()
    {
        points++;
        pointsLabel.text = "Points: " + points.ToString();
    }

    public void DecrementLives()
    {
        lives--;
        if (lives < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            livesLabel.text = "Lives: " + lives.ToString();
        }
    }
}
