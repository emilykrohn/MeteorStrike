using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
// https://discussions.unity.com/t/how-should-i-hide-show-a-visualelement/826879/4 (UI Toolkit display)

public class PowerUpScreen : MonoBehaviour
{
    [SerializeField]
    PlayerSaveData playerSaveData;
    UIDocument UIDoc;
    int powerUpDisplayCount = 3;
    List<string> powerUps = new List<string> {"Heal", "Speed", "Fire Rate"};
    List<string> tempList = new List<string>();

    Button button1;
    Button button2;
    Button button3;
    List<Button> buttonList = new List<Button>();

    ProgressBar healthBar;
    ProgressBar speedBar;
    ProgressBar fireRateBar;

    PlayerData playerData;
    GameUI gameUI;

    AudioSource audioSource;

    bool hasLoadedPowerUpScreen = false;

    void OnEnable()
    {
        audioSource = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
        playerData = FindAnyObjectByType<PlayerData>();
        gameUI = FindAnyObjectByType<GameUI>();
        UIDoc = GetComponent<UIDocument>();
    }

    void Update()
    {
        if (!hasLoadedPowerUpScreen && UIDoc.isActiveAndEnabled && !(powerUps.Count == 1 && playerData.current_health > 100))
        {
            button1 = UIDoc.rootVisualElement.Q("PowerUpButton1") as Button;
            button2 = UIDoc.rootVisualElement.Q("PowerUpButton2") as Button;
            button3 = UIDoc.rootVisualElement.Q("PowerUpButton3") as Button;

            healthBar = UIDoc.rootVisualElement.Q("HPBar") as ProgressBar;
            speedBar = UIDoc.rootVisualElement.Q("SpeedBar") as ProgressBar;
            fireRateBar = UIDoc.rootVisualElement.Q("FireRateBar") as ProgressBar;

            healthBar.value = playerData.current_health;
            healthBar.highValue = 100;

            speedBar.value = playerData.current_speed_level;
            speedBar.highValue = playerSaveData.maxSpeedLevel;

            fireRateBar.value = playerData.current_fire_rate_level;
            fireRateBar.highValue = playerSaveData.maxFireRateLevel;
            
            Debug.Log(powerUps.Count);

            if (playerData.current_health != 100)
            {
                buttonList.Add(button1);
                button1.RegisterCallback<ClickEvent>(Button1);
                playerData.current_has_max_stats = false;
            }
            else
            {
                button1.text = "Max Health";

                if (powerUps.Count == 1)
                {
                    playerData.current_has_max_stats = true;
                    DisablePowerUpScreen();
                }
                else
                {
                    playerData.current_has_max_stats = false;
                }
            }
            Debug.Log(playerData.current_has_max_stats);
            if(powerUps.Contains("Speed"))
            {
                buttonList.Add(button2);
                button2.RegisterCallback<ClickEvent>(Button2);
            }
            else
            {
                button2.text = "Max Speed";
            }
            if (powerUps.Contains("Fire Rate"))
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
        audioSource.volume = playerSaveData.sfxVolume / 100;
        audioSource.Play();
        PowerUpButton("Heal", button1);
    }

    void Button2(ClickEvent evt)
    {
        audioSource.volume = playerSaveData.sfxVolume / 100;
        audioSource.Play();
        PowerUpButton("Speed", button2);
    }

    void Button3(ClickEvent evt)
    {
        audioSource.volume = playerSaveData.sfxVolume / 100;
        audioSource.Play();
        PowerUpButton("Fire Rate", button3);
    }

    void PowerUpButton(string powerUp, Button button)
    {
        button.text = powerUp;
        if(powerUp == "Heal")
        {
            playerData.HealPowerUp();
        }
        else if(powerUp == "Speed")
        {
            playerData.SpeedPowerUp();
            playerData.current_speed_level++;

            if (playerData.current_speed > 11)
            {
                powerUps.Remove("Speed");
                Debug.Log("remove speed");
                if (powerUps.Count == 1)
                {
                    playerData.current_has_max_stats = true;
                }

            }
        }
        else if (powerUp == "Fire Rate")
        {
            if(playerData.current_fire_rate < 0.5)
            {
                powerUps.Remove("Fire Rate");
                Debug.Log("remove fire rate");
                if (powerUps.Count == 1)
                {
                    playerData.current_has_max_stats = true;
                }
            }
            playerData.FireRatePowerUp();
            playerData.current_fire_rate_level++;
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
