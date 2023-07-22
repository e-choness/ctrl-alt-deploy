using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset playerControls;
    public TextMeshProUGUI counterText;
    public GameObject winTextObject;
    public float movementSpeed = 1.0f;
    public float jumpHeight = 100.0f;

    private Rigidbody _rigidbody = null;
    private InputActionMap _playerActionMap;
    private InputAction _moveActionMap;
    private InputAction _resetInputAction;
    private InputAction _jumpInputAction;
    private Vector2 _move;
    private int _counter;

    // Start is called before the first frame update
    private void Start()
    {
        _counter = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerActionMap = playerControls.FindActionMap("Player");

        _moveActionMap = _playerActionMap.FindAction("Move");
        _resetInputAction = _playerActionMap.FindAction("Reset");
        _jumpInputAction = _playerActionMap.FindAction("Jump");

        _moveActionMap.performed += context => _move = context.ReadValue<Vector2>();
        _jumpInputAction.performed += context => JumpBall();
        _resetInputAction.performed += context => ResetBall();
        
        _moveActionMap.canceled += context => _move = context.ReadValue<Vector2>();
        _jumpInputAction.canceled += context => JumpBall();
        _resetInputAction.canceled += context => ResetBall();
        
    }

    private void OnEnable()
    {
        _moveActionMap.Enable();
        _jumpInputAction.Enable();
        _resetInputAction.Enable();
    }

    private void OnDisable()
    {
        _moveActionMap.Disable();
        _jumpInputAction.Disable();
        _resetInputAction.Disable();
    }

    // Update is called once per frame, all physics calculations are better here
    private void FixedUpdate()
    {
        MoveBall();
    }

    private void MoveBall()
    {
        var movementVector3 = new Vector3(_move.x, 0.0f, _move.y);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(movementVector3 * movementSpeed);
    }

    private void ResetBall()
    {
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        _rigidbody.isKinematic = true;
        
        
    }

    private void JumpBall()
    {
        _rigidbody.AddForce(Vector3.up * jumpHeight);
        Debug.Log($"{jumpHeight.ToString(CultureInfo.InvariantCulture)} performed.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            _counter++;
            SetCountText();
        }
            
    }

    private void SetCountText()
    {
        counterText.text = $"Count: {_counter.ToString()}";
        if(_counter >= 12)
            winTextObject.SetActive(true);
    }
}
