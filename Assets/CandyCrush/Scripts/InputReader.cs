
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CandyCrush.Scripts
{
    public class InputReader : MonoBehaviour
    {
        private GemActions _playerInput;
        private InputAction _selectAction;
        private InputAction _fireAction;

        public event Action OnFire;

        public Vector2 Selected
        {
            get
            {
                var selected = _selectAction.ReadValue<Vector2>();
                Debug.Log($"Input Reader - Selected: {selected}");
                return selected;
            }
        }

        private void Awake()
        {
            _playerInput = new();
            _selectAction = _playerInput.Player.Select;
            _fireAction = _playerInput.Player.Fire;
        }

        private void OnEnable()
        {
            
            _selectAction.Enable();
            _fireAction.Enable();
            _fireAction.performed += Fire;
        }

        private void OnDisable()
        {
            _fireAction.performed -= Fire;
            _selectAction.Disable();
            _fireAction.Disable();
        }

        private void Fire(InputAction.CallbackContext context)
        {
            Debug.Log("Input Reader - Fire().");
            OnFire?.Invoke();
        }
    }
}