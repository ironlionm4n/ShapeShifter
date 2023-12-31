using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftManager : MonoBehaviour
{
    ShiftInvoker invoker;

    // Start is called before the first frame update
    void Start()
    {
        invoker = InvokerHolder.Instance.ShiftInvoker;

        IAction startingAction = new RabbitShiftAction(transform.parent.gameObject);
        invoker.AddAction(startingAction);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            IAction newAction = new RabbitShiftAction(transform.parent.gameObject);
            invoker.AddAction(newAction);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            IAction newAction = new RavenShiftAction(transform.parent.gameObject);
            invoker.AddAction(newAction);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            IAction newAction = new WolfShiftAction(transform.parent.gameObject);
            invoker.AddAction(newAction);
        }
    }
}
