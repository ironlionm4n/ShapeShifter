using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action created when the player takes damage. Takes in a float value for the amount of damage to deal to the player
/// </summary>
public class PlayerTakeDamageAction : IAction
{
    public float damageAmount { get; private set; }
   
    public PlayerTakeDamageAction(float damageAmount)    
    {
        this.damageAmount = damageAmount;
    }

    public void Execute()
    {

    }

    public void Undo()
    {

    }

    public AnimalType GetAnimalType()
    {
        return AnimalType.None;
    }
}
