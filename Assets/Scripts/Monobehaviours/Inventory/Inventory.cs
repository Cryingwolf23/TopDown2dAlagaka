using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;  // this stores a reference to the slot prefab

    public const int numSlots = 5;  // The inventory will contain 5 slots

    Image[] itemImages = new Image[numSlots]; //array to hold 5 images

    Item[] items = new Item[numSlots]; //item array to hold 5 scriptable objects

    GameObject[] slots = new GameObject[numSlots]; // array to hold each a slot prefab
    // Start is called before the first frame update
    public void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSlots()
    {
        if(slotPrefab != null) //Check to make sure if there is in fact a slotPrefab
        {
            for(int i = 0; i < numSlots; i++) // Loop through 5 times
            {
                GameObject newSlot = Instantiate(slotPrefab); // instantiate a slotPrefab and assign it to newSlot variable
                newSlot.name = "ItemSlot_" + i; // Naming the instantiated prefabs with 0, 1 , 2, 3 , and 4 with a total sum of 5 numslots

                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform); //the script will be attached to InventoryObject prefab

                slots[i] = newSlot;  //assign the newSlot to the slots array

                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>(); // Child is ItemImage of Slot prefab
            }
        }
    }

    public bool AddItem(Item itemToAdd) //type Bool to identify if it added the item
    {
        for(int i = 0; i < items.Length; i++) // loop through the items array indexes, which is essentially 0, 1, 2, 3 and 4 because numslots = 5
        {
            if(items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)  // if one exists, if one of the same type, and if it is stackable
            {
                //Adding Existing Slot

                items[i].quantity = items[i].quantity + 1; //increment 

                Slot slotScript = slots[i].GetComponent<Slot>(); //script reference 

                Text quantityText = slotScript.qtyText;  //grabbing the text object from the script reference

                quantityText.enabled = true; //enable the text object that will be used to display quantity

                quantityText.text = items[i].quantity.ToString();  //convert the text to the items array, and the array to be converted to a string

                return true; //if we were able to add an item, the method/class will return true indicating succesfull addition of an item
            }

            if(items[i] == null)  //if there is nothing in the array
            {
                //Adding to Empty Slot
                //Copy Item & add to invetory. copying so we don't change original Scriptable object

                items[i] = Instantiate(itemToAdd);  //assigning an instantiated itemToAdd into the items array

                items[i].quantity = 1; //setting the items array's quantity to 1, para mag start sa 1 instead na 0?

                itemImages[i].sprite = itemToAdd.sprite; //assign the sprite of itemImages 

                itemImages[i].enabled = true; //enable the item image and return true, in the slot prefab the ItemImage gameobject, child of Slot game object image component was disabled
                return true;
            }
        }
        return false; //nothing was added
    }
}
