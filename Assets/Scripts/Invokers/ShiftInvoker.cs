using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftInvoker : MonoBehaviour
{
    private IAction lastAction = null;

    public Action<AnimalType> shiftOccurred;

    /// <summary>
    /// Undose the last action before doing the passed action. If is the intial load assigns the last action var for the first time
    /// </summary>
    /// <param name="action"></param>
    /// <param name="initialLoad"></param>
    public void AddAction(IAction action, bool initialLoad)
    {
        //Make sure action and last action are not of same type
        if (!initialLoad && !lastAction.GetAnimalType().Equals(action.GetAnimalType()))
        {
            lastAction?.Undo();

            lastAction = action;

            lastAction.Execute();

            shiftOccurred?.Invoke(action.GetAnimalType());
        }
        else if(initialLoad)
        {
            lastAction = action;

            lastAction.Execute();
        }
    }
}
