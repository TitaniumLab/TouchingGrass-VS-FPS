using UnityEngine.InputSystem;
using UnityEngine;

namespace GrassVsFps
{
    public class CameraController : MonoBehaviour
    {

        private float _moveSpeed;
        [SerializeField] private float _defaultMoveSpeed = 5;
        [SerializeField] private float _shiftMoveSpeed = 10;
        [SerializeField] private float _camRotationSpeed = 1;
        private bool _isLooking = false;
        private Vector3 _moveDirection = Vector3.zero;
        private Vector2 _deltaCamRotation = Vector2.zero;
        [SerializeField] private Transform _cameraTransform;

        #region Internal
        private void Awake()
        {
            _moveSpeed = _defaultMoveSpeed;
        }


        private void Update()
        {
            //Rotation
            if (_isLooking && _deltaCamRotation != Vector2.zero)
            {
                float xAxisAngle = -_deltaCamRotation.y * _camRotationSpeed;
                float yAxisAngle = _deltaCamRotation.x * _camRotationSpeed;

                // The most obvious and short way to implement camera rotation without glitches ¯\_(ツ)_/¯
                _cameraTransform.Rotate(_cameraTransform.right, xAxisAngle, Space.World);
                _cameraTransform.localEulerAngles += new Vector3(0, yAxisAngle, 0);
            }

            //Movement
            if (_moveDirection != Vector3.zero)
            {
                var hMove = new Vector3(_moveDirection.x, 0, _moveDirection.z);
                var vMove = new Vector3(0, _moveDirection.y, 0);
                _cameraTransform.Translate(hMove * _moveSpeed * Time.deltaTime);
                _cameraTransform.Translate(vMove * _moveSpeed * Time.deltaTime, Space.World);
            }
        }


        public void OnLooking(InputAction.CallbackContext context)
        {
            switch (context.ReadValue<float>())
            {
                case 1:
                    {
                        _isLooking = true;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        break;
                    }
                case 0:
                    {
                        _isLooking = false;
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        break;
                    }
            }
        }
        #endregion


        // Group of methods which called in PlayerInput
        #region OnAction
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector3>();
        }


        public void OnCameraRotation(InputAction.CallbackContext context)
        {
            _deltaCamRotation = context.ReadValue<Vector2>();
        }

        public void OnMoveShifting(InputAction.CallbackContext context)
        {
            switch (context.ReadValue<float>())
            {
                case 1: { _moveSpeed = _shiftMoveSpeed; break; }
                case 0: { _moveSpeed = _defaultMoveSpeed; break; }
            }
        }
        #endregion
    }
}
