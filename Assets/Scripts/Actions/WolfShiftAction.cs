using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action for turning the player into their wolf form
/// </summary>
public class WolfShiftAction : IAction
{
    private GameObject player;

    private const string animalType = "Wolf";

    //Going to need a reference to the wolf movement or other controllers

    /// <summary>
    /// Takes in a reference to the player and a reference to the scripts that will need to be enabled for the transformation
    /// </summary>
    public WolfShiftAction(GameObject player) { 
        this.player = player;
    }

    public void Execute()
    { 
        //Enable scripts refering to the wolf movement or abilties
        Debug.Log("Shifting into wolf form");
    }

    public void Undo()
    { 
        //Disable scripts refering to the wolf movement or abilties
        Debug.Log("Undoing Wolf Shift");
    }

    /// <summary>
    /// Returns the animal type of the action
    /// </summary>
    /// <returns></returns>
    public string GetAnimalType()
    {
        return animalType;
    }
}
