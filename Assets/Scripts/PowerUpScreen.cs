using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/ (Pause game when menu open)
// https://stackoverflow.com/questions/723211/quick-way-to-create-a-list-of-values-in-c (How to Use C# Lists)
// https://stackoverflow.com/questions/61022245/c-sharp-list-add-works-but-not-append (Add to a list)
// https://www.w3schools.com/cs/cs_foreach_loop.php (Foreach Loop)
// https://www.w3schools.com/python/python_lists_remove.asp (Remove from list)
// https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html (How to make the UI Buttons functional)
// https://www.geeksforgeeks.org/c-sharp-how-to-check-whether-a-list-contains-a-specified-element/# (Contains)

public class PowerUpScreen : MonoBehaviour
{
    UIDocument UIDoc;
    int powerUpDisplayCount = 3;
    List<string> powerUps = new List<string> {"Heal", "Speed", "Fire Rate"};
    List<string> tempList = new List<string>();

    Button button1;
    Button button2;
    Button button3;
    List<Button> buttonList = new List<Button>();

    PlayerData playerData;
    GameUI gameUI;

    bool hasLoadedPowerUpScreen = false;

    void OnEnable()
    {
        playerData = FindAnyObjectByType<PlayerData>();
        gameUI = FindAnyObjectByType<GameUI>();
        UIDoc = GetComponent<UIDocument>();
    }

    void Update()
    {
        if (!hasLoadedPowerUpScreen && UIDoc.isActiveAndEnabled && !(powerUps.Count == 1 && playerData.current_health > 100))
        {
            tempList.Clear();
            hasLoadedPowerUpScreen = true;
            foreach(string powerUp in powerUps)
            {
                tempList.Add(powerUp);
                print("List Item: " + powerUp);
            }

            while(tempList.Count > powerUpDisplayCount)
            {
                tempList.Remove(tempList[Random.Range(0, tempList.Count - 1)]);
            }

            button1 = UIDoc.rootVisualElement.Q("PowerUpButton1") as Button;
            button2 = UIDoc.rootVisualElement.Q("PowerUpButton2") as Button;
            button3 = UIDoc.rootVisualElement.Q("PowerUpButton3") as Button;

            buttonList.Add(button1);
            button1.RegisterCallback<ClickEvent>(Button1);
            if(tempList.Contains("Speed"))
            {
                buttonList.Add(button2);
                button2.RegisterCallback<ClickEvent>(Button2);
            }
            else
            {
                button2.text = "Max Speed";
            }
            if (tempList.Contains("Fire Rate"))
            {
                buttonList.Add(button3);
                button3.RegisterCallback<ClickEvent>(Button3);
            }
            else
            {
                button3.text = "Max Fire Rate";
            }
        }
        else
        {
            playerData.current_has_max_power_ups = true;
            gameUI.maxPowerUpsLabel.visible = true;
        }
    }

    void Button1(ClickEvent evt)
    {
        PowerUpButton(0);
    }

    void Button2(ClickEvent evt)
    {
        PowerUpButton(1);
    }

    void Button3(ClickEvent evt)
    {
        PowerUpButton(2);
    }

    void PowerUpButton(int index)
    {
        string currentPowerUp = tempList[index];
        buttonList[index].text = currentPowerUp;
        if(currentPowerUp == powerUps[0])
        {
            playerData.HealPowerUp();
        }
        else if(currentPowerUp == powerUps[1])
        {
            if (playerData.current_speed >= 12)
            {
                powerUps.Remove("Speed");
            }
            else
            {
                playerData.SpeedPowerUp();
            }
        }
        else if (currentPowerUp == powerUps[2])
        {
            if(playerData.current_fire_rate <= 0.2)
            {
                powerUps.Remove("Fire Rate");
                Debug.Log("FireRate");
            }
            else
            {
                playerData.FireRatePowerUp();
            }
        }
        DisablePowerUpScreen();
    }

    public void EnablePowerUpScreen()
    {
        UIDoc.enabled = true;
        Time.timeScale = 0;
    }

    void DisablePowerUpScreen()
    {
        hasLoadedPowerUpScreen = false;
        Time.timeScale = 1;
        UIDoc.enabled = false;
    }
}
