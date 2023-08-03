using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatInvoker : MonoBehaviour
{
    public Action<float> playerHitAction;

    public void AddAction(IAction action)
    {
        action.Execute();

        if(action is PlayerTakeDamageAction)
        {
            PlayerTakeDamageAction playerTakeDamageAction = (PlayerTakeDamageAction)action;
            playerHitAction?.Invoke(playerTakeDamageAction.damageAmount);
        }
    }
}
