using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitShiftAction : IAction
{
    private GameObject player;

    private const AnimalType animalType = AnimalType.Rabbit;

    //Going to need a reference to the raven movement or other controllers

    /// <summary>
    /// Takes in a reference to the player and a reference to the scripts that will need to be enabled for the transformation
    /// </summary>
    public RabbitShiftAction(GameObject player)
    {
        this.player = player;
    }

    public void Execute()
    {
        //Enable scripts refering to the rabbit movement or abilties
        Debug.Log("Shifting into rabbit form");
    }

    public void Undo()
    {
        //Disable scripts refering to the rabbit movement or abilties
        Debug.Log("Undoing rabbit Shift");
    }

    /// <summary>
    /// Returns the animal type of the action
    /// </summary>
    /// <returns></returns>
    public AnimalType GetAnimalType()
    {
        return animalType;
    }
}
