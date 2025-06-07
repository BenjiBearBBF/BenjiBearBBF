//===========================================================================//
//                       FreeFlyCamera modified by BBF                       //
//                                                                           //
//===========================================================================//

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class QFlyCam : MonoBehaviour
{
    #region UI

    [Space]

    [SerializeField]
    [Tooltip("The script is currently active")]
    private bool _active = true;

    [Space]

    [SerializeField]
    [Tooltip("Camera rotation by mouse movement is active")]
    private bool _enableRotation = false;

    [SerializeField]
    [Tooltip("Sensitivity of mouse rotation")]
    private float _mouseSense = 1.8f;

    [Space]

    [SerializeField]
    [Tooltip("Camera zooming in/out by 'Mouse Scroll Wheel' is active")]
    private bool _enableTranslation = true;

    [SerializeField]
    [Tooltip("Velocity of camera zooming in/out")]
    private float _translationSpeed = 55f;

    [Space]

    [SerializeField]
    [Tooltip("Camera movement by 'W','A','S','D','Q','E' keys is active")]
    private bool _enableMovement = true;

    [SerializeField]
    [Tooltip("Camera movement speed")]
    private float _movementSpeed = 10f;

    [SerializeField]
    [Tooltip("Speed of the quick camera movement when holding the 'Left Shift' key")]
    private float _boostedSpeed = 50f;

    [SerializeField]
    [Tooltip("Boost speed")]
    private KeyCode _boostSpeed = KeyCode.LeftShift;

    [SerializeField]
    [Tooltip("Move up")]
    private KeyCode _moveUp = KeyCode.E;

    [SerializeField]
    [Tooltip("Move down")]
    private KeyCode _moveDown = KeyCode.Q;

    [Space]

    [SerializeField]
    [Tooltip("Acceleration at camera movement is active")]
    private bool _enableSpeedAcceleration = true;

    [SerializeField]
    [Tooltip("Rate which is applied during camera movement")]
    private float _speedAccelerationFactor = 1.5f;

    [Space]

    [SerializeField]
    [Tooltip("This keypress will move the camera to initialization position")]
    private KeyCode _initPositonButton = KeyCode.R;

    #endregion UI

    private CursorLockMode _wantedMode;

    private float _currentIncrease = 1;
    private float _currentIncreaseMem = 0;

    private bool _WASD = false;
    private bool _hidePointer = false;

    private Vector3 _initPosition;
    private Vector3 _initRotation;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_boostedSpeed < _movementSpeed)
            _boostedSpeed = _movementSpeed;
    }
#endif
    
    public void ApplyQuantumGate(Vector3 axis, float angle)
    {
        transform.Rotate(axis, angle, Space.Self); // Or Space.World if needed
    }

    private void Start()
    {
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;
        
    }

    private void OnEnable()
    {
        if (_active)
            _wantedMode = CursorLockMode.Locked;
          //  _wantedMode = CursorLockMode.None;
    }

    // Apply requested cursor state
    private void SetCursorState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = _wantedMode = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
           // _wantedMode = CursorLockMode.Locked;

        }

        // Apply cursor state
        Cursor.lockState = _wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != _wantedMode);
    }

    private void CalculateCurrentIncrease(bool moving)
    {
        _currentIncrease = Time.deltaTime;

        if (!_enableSpeedAcceleration || _enableSpeedAcceleration && !moving)
        {
            _currentIncreaseMem = 0;
            return;
        }

        _currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1);
        _currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3) * Time.deltaTime;
    }

    private void Update()
    {
        if (!_active)
            return;

        SetCursorState();

        if (Cursor.visible)
            return;

        // Translation
        if (_enableTranslation)
        {
            transform.Translate( Time.deltaTime * _translationSpeed * Input.mouseScrollDelta.y * Vector3.forward) ;
        }

        // Movement
        if (_enableMovement)
        {
            Vector3 deltaPosition = Vector3.zero;
            float currentSpeed = _movementSpeed;

            if (Input.GetKey(_boostSpeed))
                currentSpeed = _boostedSpeed;
            
            if (Input.GetKey(KeyCode.W))
                _WASD = true;
            
// Move forward when both mouse buttons are held
            if (_WASD = true)
            {
                if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                    deltaPosition += transform.forward;

                if (Input.GetMouseButton(2) && Input.GetMouseButton(1))
                    deltaPosition += -transform.forward;

                if (Input.GetKey(KeyCode.W))
                    deltaPosition += transform.forward;

                if (Input.GetKey(KeyCode.S))
                    deltaPosition -= transform.forward;

                if (Input.GetKey(KeyCode.A))
                    deltaPosition -= transform.right;

                if (Input.GetKey(KeyCode.D))
                    deltaPosition += transform.right;
            }

            if (Input.GetKey(_moveUp))
                deltaPosition += transform.up;

            if (Input.GetKey(_moveDown))
                deltaPosition -= transform.up;
            

            // Calc acceleration
            CalculateCurrentIncrease(deltaPosition != Vector3.zero);

            transform.position += deltaPosition * currentSpeed * _currentIncrease;
            // this may not be efficient
        }

        // Rotation
        if (
            //_enableRotation && 
            Input.GetMouseButton(1) &&
            !(Input.GetMouseButton(0)))
        {
            // Pitch
            transform.rotation *= Quaternion.AngleAxis(
                -Input.GetAxis("Mouse Y") * _mouseSense,
                Vector3.right
            );

            // Yaw
            transform.rotation = Quaternion.Euler(
                transform.eulerAngles.x,
                transform.eulerAngles.y + Input.GetAxis("Mouse X") * _mouseSense,
                transform.eulerAngles.z
            );
        }
        // Tilt head left/right when Mouse Button 4 is held
        if (Input.GetMouseButton(3)) // Mouse Button 4
        {
            float tiltAmount = Input.GetAxis("Mouse X") * _mouseSense;

            transform.rotation = Quaternion.Euler(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z - tiltAmount
            );
        }
        
        // Tilt head right/left when Mouse Button 5 is held
        if (Input.GetMouseButton(4) 
            ) // Mouse Button 5
        {
            float tiltAmount = Input.GetAxis("Mouse X") * -_mouseSense;

            transform.rotation = Quaternion.Euler(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z - tiltAmount
            );
        }
  
        
        // Return to init position
        if (Input.GetKeyDown(_initPositonButton))
        {
            transform.position = _initPosition;
            transform.eulerAngles = _initRotation;
        }
    }
}
