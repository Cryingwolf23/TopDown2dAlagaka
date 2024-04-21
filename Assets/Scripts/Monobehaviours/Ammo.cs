using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageInflicted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision is BoxCollider2D)  //check if the hit is a boxcollider
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); //get the component of the boxcollider that haas beenhit in the enemy

            StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));

            gameObject.SetActive(false);  //something about object pooling or some shit
        }
    }


}
