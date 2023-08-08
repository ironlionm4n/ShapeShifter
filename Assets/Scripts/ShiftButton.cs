using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShiftButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UIManager manager;

    public void OnPointerEnter(PointerEventData data)
    {
        manager.SetSelectedButton(GetComponent<Button>());
    }
}
