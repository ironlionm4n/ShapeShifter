using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
