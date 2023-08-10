//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Inputs/HareControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @HareControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @HareControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HareControls"",
    ""maps"": [
        {
            ""name"": ""HareMovement"",
            ""id"": ""b99f8362-8506-4bad-a3e5-01fae2485b07"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0e4d37cd-c57f-4d6f-ad12-8d33d93ff568"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1b1fb753-0b8d-4589-8b28-d117487753e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5570fc35-119e-49b4-9e6d-a9953ba4cad7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24137d0b-34cc-45b1-b858-29f78a120ce7"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d9abf352-694a-40c9-8a25-e8b4d354e3b1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""26fd492f-80ef-44a7-9a25-ea2f597ba3fd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""26632745-14d6-4e28-925a-54f620d37e7c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e8037940-6e3d-47dd-b7d0-76e15a73ed48"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6dde4b41-083a-4fad-b694-d2ea32271be0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""bde2d3f0-8c56-4dbf-a6bd-f954bb82eeea"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f7c59895-2b72-4846-a618-9080bc947d28"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""78fcf2e2-70fe-49bb-b812-2cf60e1f7f87"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f872b84f-a2cb-4e37-9770-e964c5883103"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f68dd4a8-e2a8-4ea0-a52c-696e49a12e19"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Shifting"",
            ""id"": ""6f174cb6-9143-4c98-b6cd-b2bf6f53a26a"",
            ""actions"": [
                {
                    ""name"": ""OpenShiftWheel"",
                    ""type"": ""Button"",
                    ""id"": ""b5dba357-a003-479f-9a65-0ac1b4407e4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NavigateShiftWheel"",
                    ""type"": ""Button"",
                    ""id"": ""01e949c7-88d4-4c65-9bc2-d810945197ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WheelSelection"",
                    ""type"": ""Button"",
                    ""id"": ""d26de2e8-4b40-4112-8c9f-c5bde7052739"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a3e87fb0-2d44-4615-9641-cc850ddd7ddd"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenShiftWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9846d140-35b6-4b19-b99d-937fee8918e7"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenShiftWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""887a09f6-4c35-45cc-b1a5-d945f11440b8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateShiftWheel"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""66aaf17b-d5be-4069-908d-8eeb55495bcc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateShiftWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a2355783-e8ef-40ad-b062-0c748637e22c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateShiftWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""4176df3a-fb24-4c32-b31e-706fcf8291df"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateShiftWheel"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fc101e84-d893-4f97-b210-0caf432ffd39"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateShiftWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fbd47209-bc1d-4540-b1ea-9edd7eb972e1"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateShiftWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5d4492d7-4a1e-46a8-a015-c3b6dec409c7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WheelSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5665492d-e73c-4b9b-8f86-b4f7997b1ec9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WheelSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // HareMovement
        m_HareMovement = asset.FindActionMap("HareMovement", throwIfNotFound: true);
        m_HareMovement_Movement = m_HareMovement.FindAction("Movement", throwIfNotFound: true);
        m_HareMovement_Jump = m_HareMovement.FindAction("Jump", throwIfNotFound: true);
        // Shifting
        m_Shifting = asset.FindActionMap("Shifting", throwIfNotFound: true);
        m_Shifting_OpenShiftWheel = m_Shifting.FindAction("OpenShiftWheel", throwIfNotFound: true);
        m_Shifting_NavigateShiftWheel = m_Shifting.FindAction("NavigateShiftWheel", throwIfNotFound: true);
        m_Shifting_WheelSelection = m_Shifting.FindAction("WheelSelection", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // HareMovement
    private readonly InputActionMap m_HareMovement;
    private List<IHareMovementActions> m_HareMovementActionsCallbackInterfaces = new List<IHareMovementActions>();
    private readonly InputAction m_HareMovement_Movement;
    private readonly InputAction m_HareMovement_Jump;
    public struct HareMovementActions
    {
        private @HareControls m_Wrapper;
        public HareMovementActions(@HareControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_HareMovement_Movement;
        public InputAction @Jump => m_Wrapper.m_HareMovement_Jump;
        public InputActionMap Get() { return m_Wrapper.m_HareMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HareMovementActions set) { return set.Get(); }
        public void AddCallbacks(IHareMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_HareMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_HareMovementActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IHareMovementActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IHareMovementActions instance)
        {
            if (m_Wrapper.m_HareMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IHareMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_HareMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_HareMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public HareMovementActions @HareMovement => new HareMovementActions(this);

    // Shifting
    private readonly InputActionMap m_Shifting;
    private List<IShiftingActions> m_ShiftingActionsCallbackInterfaces = new List<IShiftingActions>();
    private readonly InputAction m_Shifting_OpenShiftWheel;
    private readonly InputAction m_Shifting_NavigateShiftWheel;
    private readonly InputAction m_Shifting_WheelSelection;
    public struct ShiftingActions
    {
        private @HareControls m_Wrapper;
        public ShiftingActions(@HareControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenShiftWheel => m_Wrapper.m_Shifting_OpenShiftWheel;
        public InputAction @NavigateShiftWheel => m_Wrapper.m_Shifting_NavigateShiftWheel;
        public InputAction @WheelSelection => m_Wrapper.m_Shifting_WheelSelection;
        public InputActionMap Get() { return m_Wrapper.m_Shifting; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShiftingActions set) { return set.Get(); }
        public void AddCallbacks(IShiftingActions instance)
        {
            if (instance == null || m_Wrapper.m_ShiftingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ShiftingActionsCallbackInterfaces.Add(instance);
            @OpenShiftWheel.started += instance.OnOpenShiftWheel;
            @OpenShiftWheel.performed += instance.OnOpenShiftWheel;
            @OpenShiftWheel.canceled += instance.OnOpenShiftWheel;
            @NavigateShiftWheel.started += instance.OnNavigateShiftWheel;
            @NavigateShiftWheel.performed += instance.OnNavigateShiftWheel;
            @NavigateShiftWheel.canceled += instance.OnNavigateShiftWheel;
            @WheelSelection.started += instance.OnWheelSelection;
            @WheelSelection.performed += instance.OnWheelSelection;
            @WheelSelection.canceled += instance.OnWheelSelection;
        }

        private void UnregisterCallbacks(IShiftingActions instance)
        {
            @OpenShiftWheel.started -= instance.OnOpenShiftWheel;
            @OpenShiftWheel.performed -= instance.OnOpenShiftWheel;
            @OpenShiftWheel.canceled -= instance.OnOpenShiftWheel;
            @NavigateShiftWheel.started -= instance.OnNavigateShiftWheel;
            @NavigateShiftWheel.performed -= instance.OnNavigateShiftWheel;
            @NavigateShiftWheel.canceled -= instance.OnNavigateShiftWheel;
            @WheelSelection.started -= instance.OnWheelSelection;
            @WheelSelection.performed -= instance.OnWheelSelection;
            @WheelSelection.canceled -= instance.OnWheelSelection;
        }

        public void RemoveCallbacks(IShiftingActions instance)
        {
            if (m_Wrapper.m_ShiftingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IShiftingActions instance)
        {
            foreach (var item in m_Wrapper.m_ShiftingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ShiftingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ShiftingActions @Shifting => new ShiftingActions(this);
    public interface IHareMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IShiftingActions
    {
        void OnOpenShiftWheel(InputAction.CallbackContext context);
        void OnNavigateShiftWheel(InputAction.CallbackContext context);
        void OnWheelSelection(InputAction.CallbackContext context);
    }
}
