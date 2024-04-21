using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    //[HideInInspector]
    public Player character; // "Programmatically sets it to the player"
    public Image meterImage;
    public Text hpText;

    float maxHitpoints; 

    void Start()
    {
       
        
            maxHitpoints = character.maxHitPoints; //sets the Player's maxHitPoints value into a variable here
       
    }

    
    void Update()
    {
        if(character != null) 
        {
            //starting hitpoints value is 6, maxHitPoints value is 10, this is set in the unity editor through the playerObject
            // 6/10 = 0.6
            meterImage.fillAmount = hitPoints.value / maxHitpoints; 

            //0.6 x 100 = 60
            hpText.text = "HP: " + (meterImage.fillAmount * 100);
        }
    }
}
