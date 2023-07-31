using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftInvoker : MonoBehaviour
{
    private IAction lastAction = null;

    /// <summary>
    /// Undose the last action before doing the passed action
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(IAction action)
    {
        //Make sure action and last action are not of same type
        if (!lastAction.GetAnimalType().Equals(action.GetAnimalType()))
        {
            lastAction?.Undo();

            lastAction = action;

            lastAction.Execute();
        }
    }
}
