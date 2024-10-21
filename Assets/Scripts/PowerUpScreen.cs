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
    // Power up list
    List<string> powerUps = new List<string> {"Heal", "Speed", "Fire Rate"};

    [SerializeField] PlayerSaveData playerSaveData;
    PlayerData playerData;
    
    AudioSource audioSource;

    // UI Elements
    UIDocument UIDoc;
    Button button1;
    Button button2;
    Button button3;
    ProgressBar healthBar;
    ProgressBar speedBar;
    ProgressBar fireRateBar;

    bool hasLoadedPowerUpScreen = false;

    void OnEnable()
    {
        audioSource = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
        playerData = FindAnyObjectByType<PlayerData>();
        UIDoc = GetComponent<UIDocument>();
    }

    void Update()
    {
        if (!hasLoadedPowerUpScreen && UIDoc.isActiveAndEnabled && !(powerUps.Count == 1 && playerData.current_health > 100))
        {
            // Button options for the power ups
            button1 = UIDoc.rootVisualElement.Q("PowerUpButton1") as Button;
            button2 = UIDoc.rootVisualElement.Q("PowerUpButton2") as Button;
            button3 = UIDoc.rootVisualElement.Q("PowerUpButton3") as Button;

            // Power up progress bars
            healthBar = UIDoc.rootVisualElement.Q("HPBar") as ProgressBar;
            speedBar = UIDoc.rootVisualElement.Q("SpeedBar") as ProgressBar;
            fireRateBar = UIDoc.rootVisualElement.Q("FireRateBar") as ProgressBar;


            // Update the values for the progress bars
            healthBar.value = playerData.current_health;
            healthBar.highValue = 100;

            speedBar.value = playerData.current_speed_level;
            speedBar.highValue = playerSaveData.maxSpeedLevel;

            fireRateBar.value = playerData.current_fire_rate_level;
            fireRateBar.highValue = playerSaveData.maxFireRateLevel;
            
            // If the current health is not full, then allow heal button functionality
            if (playerData.current_health != 100)
            {
                button1.RegisterCallback<ClickEvent>(Button1);
                // If health isn't full stats aren't max, allow the player to heal.
                playerData.current_has_max_stats = false;
            }
            else
            {
                // Update button text so player knows they can't use this power up option
                button1.text = "Max Health";

                // If heal is the only power up and the health is full then the stats are max and and teh Power up screen should be disabled
                if (powerUps.Count == 1)
                {
                    playerData.current_has_max_stats = true;
                    DisablePowerUpScreen();
                }
                // Else there are other power up options and the stats are not max, just means that heal isn't an option
                else
                {
                    playerData.current_has_max_stats = false;
                }
            }

            // If the powerUps list contains "Speed"
            if(powerUps.Contains("Speed"))
            {
                // Allow Button 2 for speed functionality
                button2.RegisterCallback<ClickEvent>(Button2);
            }
            else
            {
                // Update button text so player knows they can't use this power up option
                button2.text = "Max Speed";
            }
            if (powerUps.Contains("Fire Rate"))
            {
                // Allow Button 3 for fire rate functionality
                button3.RegisterCallback<ClickEvent>(Button3);
            }
            else
            {
                // Update button text so player knows they can't use this power up option
                button3.text = "Max Fire Rate";
            }
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
            // Heal the player
            playerData.HealPowerUp();
        }
        else if(powerUp == "Speed")
        {
            // update speed stat and level
            playerData.SpeedPowerUp();
            playerData.current_speed_level++;

            // If speed stat is at max, remove it form powerUps list options
            if (playerData.current_speed > 11)
            {
                powerUps.Remove("Speed");
                // If heal is only power up left, current has max stats is true, will check later if player health is full of not in Update
                if (powerUps.Count == 1)
                {
                    playerData.current_has_max_stats = true;
                }

            }
        }
        else if (powerUp == "Fire Rate")
        {
            // If fire rate stat is at max, remove it form powerUps list options
            if(playerData.current_fire_rate < 0.5)
            {
                powerUps.Remove("Fire Rate");
                // If heal is only power up left, current has max stats is true, will check later if player health is full of not in Update
                if (powerUps.Count == 1)
                {
                    playerData.current_has_max_stats = true;
                }
            }
            // Update fire rate stat and level
            playerData.FireRatePowerUp();
            playerData.current_fire_rate_level++;
        }
        // Close power up screen once the player has picked a power up
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
