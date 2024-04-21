using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    float hitPoints;

    public int damageStrength;

    Coroutine damageCourotine;

    private void OnEnable()
    {
        ResetCharacter();
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints; 
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while(true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;
            if(hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }

            else
            {
                break;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            
            if(damageCourotine == null)
            {
                damageCourotine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(damageCourotine != null)
            {
                StopCoroutine(damageCourotine);
                damageCourotine = null;
            }
        }
    }

    

}
