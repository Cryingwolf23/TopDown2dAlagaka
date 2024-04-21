using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character      //This means that Player script inherits from character
{
    public HealthBar healthBarPrefab;  //Using HealthBar as a class to get a reference to the prefab

    HealthBar healthBar; // use to instantiate the healthBarPrefab

    public Inventory inventoryPrefab; // store a reference to the Inventory prefab

    Inventory inventory; // used to store a reference to the invetory once it's instantiated

    public HitPoints hitPoints;

    private void OnEnable()
    {
        ResetCharacter();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item; 

            if (hitObject != null)
            {
                bool shouldDisappear = false; 

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject); // refer to the inventory script
                       break;
                    //hitObject.quantity of item type Health = 1, refer to the Heart Scriptable object
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity); 
                        break; 
                    default:
                        break;
                }
                    if (shouldDisappear)
                    {
                        collision.gameObject.SetActive(false);
                    }
            }
        }

    }
    public bool AdjustHitPoints(int amount) 
        {
            if (hitPoints.value < maxHitPoints)
            {
                hitPoints.value = hitPoints.value + amount;
                print("Adjusted HP by: " + amount + ". New Value: " + hitPoints.value);

                return true;
            } 
            return false;
        }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints.value = hitPoints.value - damage; 

            if(hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if(interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }

            else
            {
                break;
            }
        }

    }

    public override void KillCharacter()
    {
        base.KillCharacter();

        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab); //instantiate the inventory prefab, and will store a reference to the prefab in the inventory variable
        healthBar = Instantiate(healthBarPrefab);      // Instantiates the healthbar prefab in accorandace to the value of the line above
        healthBar.character = this;  //This essentially means the character property in HealthBar script to the Player holding this script component\
        hitPoints.value = startingHitPoints;          //This sets the value of the hitPoints to the startingHitPoints value
    }
}
