using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    UIInvoker _uiInvoker;

    [SerializeField] private GameObject shiftUI;
    [SerializeField, Tooltip("Clockwise")] private Button[] shiftButtons;
    [SerializeField] private Image pointer;
    [SerializeField] private Image centerPoint;
    [SerializeField] private PlayerStats playerStats;

    private Vector3 _mousePosition;
    private Camera _mainCamera;
    private Button _selectedButton;
    private int _selectedButtonIndex;

    //Controls when the shift UI is visible
    private bool _shifterActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _uiInvoker = InvokerHolder.Instance.UIInvoker;   
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Shift();
        }

        if (_shifterActive)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                _selectedButtonIndex--;

                if( _selectedButtonIndex < 0 )
                {
                    _selectedButtonIndex = shiftButtons.Length - 1;
                }

                _selectedButton = shiftButtons[_selectedButtonIndex];
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                _selectedButtonIndex++;

                if( _selectedButtonIndex > shiftButtons.Length - 1)
                {
                    _selectedButtonIndex = 0;
                }

                _selectedButton = shiftButtons[_selectedButtonIndex];
            }

            //Points the pointer towards the selected button
            if (_selectedButton != null)
            {
                Vector3 targ = _selectedButton.transform.position;
                targ.z = 0f;

                Vector3 objectPos = centerPoint.transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                centerPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
            }
        }
    }

    public void Shift()
    {
        _shifterActive = !_shifterActive;

        if (_shifterActive)
        {
            SetSelectedButtonDefault();
        }

        shiftUI.SetActive(_shifterActive);
    }

    public void SetSelectedButton(Button button)
    {
        _selectedButton = button;
        _selectedButtonIndex = Array.IndexOf(shiftButtons, button);
    }

    private void SetSelectedButtonDefault()
    {
        AnimalType currentAnimal = playerStats.GetCurrentAnimalType();
        
        for(int i = 0; i < shiftButtons.Length; i++)
        {
            if (shiftButtons[i].name.Contains(currentAnimal.ToString()))
            {
                _selectedButton = shiftButtons[i];
                _selectedButtonIndex = i;
                return;
            }
        }

        _selectedButton = null;
        Debug.LogWarning("Warning: No Shift Button matching animal name");
    }
}
