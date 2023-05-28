//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Player/GameplayInput.inputactions
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

public partial class @GameplayInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameplayInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameplayInput"",
    ""maps"": [
        {
            ""name"": ""Spaceship"",
            ""id"": ""fa87c965-8819-4cc3-bc46-01287950cee6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""92b971ca-6a9d-46ae-b3e3-1543c1c3e928"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""cbaa3017-6240-4516-92b6-020c93440ea7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""bbfe08e8-e01d-4f10-bed3-133fc755de6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""6fb860b3-9cad-424e-9c07-2a371c4cddea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ultimate"",
                    ""type"": ""Button"",
                    ""id"": ""a8c95f0c-ec6b-4473-80c8-575d53acc058"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5257303e-dbbf-4454-b43c-8377c7d24c82"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdb3378e-7bc2-48f3-bbe6-562f1d49de39"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""SecondaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1933cf2c-49d9-460f-b6d0-6ae82b4fc7dd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cff86f04-4814-4911-83ac-0ece4c8c47e8"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""35bc83a0-2fe7-4935-bec5-a12de74594f2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6a832916-d90a-4e30-8688-90b7fa21eb2b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2e534bb1-4655-46aa-9e82-28efebc45f86"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""466ed7ec-8178-4810-ab3d-12c0ad2ed5c7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1c617c3d-e634-42ad-a23c-74d6a1cd9edc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseNKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseNKeyboard"",
            ""bindingGroup"": ""MouseNKeyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Spaceship
        m_Spaceship = asset.FindActionMap("Spaceship", throwIfNotFound: true);
        m_Spaceship_Move = m_Spaceship.FindAction("Move", throwIfNotFound: true);
        m_Spaceship_PrimaryFire = m_Spaceship.FindAction("PrimaryFire", throwIfNotFound: true);
        m_Spaceship_SecondaryFire = m_Spaceship.FindAction("SecondaryFire", throwIfNotFound: true);
        m_Spaceship_Roll = m_Spaceship.FindAction("Roll", throwIfNotFound: true);
        m_Spaceship_Ultimate = m_Spaceship.FindAction("Ultimate", throwIfNotFound: true);
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

    // Spaceship
    private readonly InputActionMap m_Spaceship;
    private ISpaceshipActions m_SpaceshipActionsCallbackInterface;
    private readonly InputAction m_Spaceship_Move;
    private readonly InputAction m_Spaceship_PrimaryFire;
    private readonly InputAction m_Spaceship_SecondaryFire;
    private readonly InputAction m_Spaceship_Roll;
    private readonly InputAction m_Spaceship_Ultimate;
    public struct SpaceshipActions
    {
        private @GameplayInput m_Wrapper;
        public SpaceshipActions(@GameplayInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Spaceship_Move;
        public InputAction @PrimaryFire => m_Wrapper.m_Spaceship_PrimaryFire;
        public InputAction @SecondaryFire => m_Wrapper.m_Spaceship_SecondaryFire;
        public InputAction @Roll => m_Wrapper.m_Spaceship_Roll;
        public InputAction @Ultimate => m_Wrapper.m_Spaceship_Ultimate;
        public InputActionMap Get() { return m_Wrapper.m_Spaceship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpaceshipActions set) { return set.Get(); }
        public void SetCallbacks(ISpaceshipActions instance)
        {
            if (m_Wrapper.m_SpaceshipActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMove;
                @PrimaryFire.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnPrimaryFire;
                @SecondaryFire.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnSecondaryFire;
                @SecondaryFire.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnSecondaryFire;
                @SecondaryFire.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnSecondaryFire;
                @Roll.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnRoll;
                @Ultimate.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnUltimate;
                @Ultimate.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnUltimate;
                @Ultimate.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnUltimate;
            }
            m_Wrapper.m_SpaceshipActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @PrimaryFire.started += instance.OnPrimaryFire;
                @PrimaryFire.performed += instance.OnPrimaryFire;
                @PrimaryFire.canceled += instance.OnPrimaryFire;
                @SecondaryFire.started += instance.OnSecondaryFire;
                @SecondaryFire.performed += instance.OnSecondaryFire;
                @SecondaryFire.canceled += instance.OnSecondaryFire;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Ultimate.started += instance.OnUltimate;
                @Ultimate.performed += instance.OnUltimate;
                @Ultimate.canceled += instance.OnUltimate;
            }
        }
    }
    public SpaceshipActions @Spaceship => new SpaceshipActions(this);
    private int m_MouseNKeyboardSchemeIndex = -1;
    public InputControlScheme MouseNKeyboardScheme
    {
        get
        {
            if (m_MouseNKeyboardSchemeIndex == -1) m_MouseNKeyboardSchemeIndex = asset.FindControlSchemeIndex("MouseNKeyboard");
            return asset.controlSchemes[m_MouseNKeyboardSchemeIndex];
        }
    }
    public interface ISpaceshipActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPrimaryFire(InputAction.CallbackContext context);
        void OnSecondaryFire(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnUltimate(InputAction.CallbackContext context);
    }
}
