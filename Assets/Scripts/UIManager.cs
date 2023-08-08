using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    UIInvoker _uiInvoker;
    ShiftInvoker _shiftInvoker;

    [SerializeField] private GameObject shiftUI;
    [SerializeField, Tooltip("Clockwise")] private Button[] shiftButtons;
    [SerializeField] private Image pointer;
    [SerializeField] private Image centerPoint;
    [SerializeField] private PlayerStats playerStats;

    private Vector3 _mousePosition;
    private Camera _mainCamera;
    private Button _selectedButton;
    private Button _lastSelectedButton;
    private int _selectedButtonIndex;

    //Controls when the shift UI is visible
    private bool _shifterActive = false;

    private HareControls _hareActionMap;
    private InputAction _shiftOpen;
    private InputAction _navigateShiftWheel;
    private InputAction _selectShift;

    private void OnEnable()
    {
        _hareActionMap = new HareControls();
        _hareActionMap.Enable();

        _shiftOpen = _hareActionMap.Shifting.OpenShiftWheel;
        _navigateShiftWheel = _hareActionMap.Shifting.NavigateShiftWheel;
        _selectShift = _hareActionMap.Shifting.WheelSelection;
    }

    private void OnDisable()
    {
        _hareActionMap.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _uiInvoker = InvokerHolder.Instance.UIInvoker;
        _shiftInvoker = InvokerHolder.Instance.ShiftInvoker;
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shiftOpen.WasPressedThisFrame())
        {
            Shift();
        }

        if (_shifterActive)
        {
            if (_navigateShiftWheel.WasPressedThisFrame())
            {
                float shiftMovement = _navigateShiftWheel.ReadValue<float>();
                if (shiftMovement < 0)
                {
                    _selectedButtonIndex--;

                    if (_selectedButtonIndex < 0)
                    {
                        _selectedButtonIndex = shiftButtons.Length - 1;
                    }

                    _selectedButton = shiftButtons[_selectedButtonIndex];
                }

                if (shiftMovement > 0)
                {
                    _selectedButtonIndex++;

                    if (_selectedButtonIndex > shiftButtons.Length - 1)
                    {
                        _selectedButtonIndex = 0;
                    }

                    _selectedButton = shiftButtons[_selectedButtonIndex];
                }
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

                if (_selectShift.WasPressedThisFrame())
                {
                    _selectedButton.onClick.Invoke();
                    _selectedButton.interactable = false;

                    _lastSelectedButton.interactable = true;
                    _lastSelectedButton = _selectedButton;

                    Shift();
                }
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
        _lastSelectedButton = _selectedButton;
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

                if (_lastSelectedButton == null)
                {
                    _lastSelectedButton = _selectedButton;
                }

                _selectedButton.interactable = false;
                return;
            }
        }



        _selectedButton = null;
        Debug.LogWarning("Warning: No Shift Button matching animal name");
    }

    public void ShiftToWolf()
    {
        IAction newAction = new WolfShiftAction(GameObject.Find("Player"));
        _shiftInvoker.AddAction(newAction, false);
    }

    public void ShiftToRaven()
    {
        IAction newAction = new RavenShiftAction(GameObject.Find("Player"));
        _shiftInvoker.AddAction(newAction, false);
    }

    public void ShiftToRabbit()
    {
        IAction newAction = new RabbitShiftAction(GameObject.Find("Player"));
        _shiftInvoker.AddAction(newAction, false);
    }
}
