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

        IAction startingAction = new HareShiftAction(transform.parent.gameObject);
        invoker.AddAction(startingAction, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            IAction newAction = new HareShiftAction(transform.parent.gameObject);
            invoker.AddAction(newAction, false);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            IAction newAction = new RavenShiftAction(transform.parent.gameObject);
            invoker.AddAction(newAction, false);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            IAction newAction = new WolfShiftAction(transform.parent.gameObject);
            invoker.AddAction(newAction, false);
        }
    }
}
