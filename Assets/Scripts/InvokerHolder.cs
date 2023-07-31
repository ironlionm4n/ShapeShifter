using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokerHolder : MonoBehaviour
{
    public static InvokerHolder Instance;

    public ShiftInvoker ShiftInvoker;

    private void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("More than one InvokerHolder detected. Deleting " + gameObject.name);
            Destroy(gameObject);
        }

        ShiftInvoker = new ShiftInvoker();
    }

}
