using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick = null;

    [SerializeField] private float _speed = 10f, _lookRotationSpeed = 500f;
    [SerializeField] private float _gravity = -10f;

    public float LookRotationSpeed
    {
        get => _lookRotationSpeed;
        private set => _lookRotationSpeed = value;
    }

    private CharacterController _characterController = null;

    private CharacterController _controller
    {
        get => _characterController = _characterController ?? GetComponent<CharacterController>();
    }

    private EMoveStatus _moveStatus;

    public EMoveStatus MoveStatus
    {
        get => _moveStatus;
    }

    public enum EMoveStatus
    {
        idle,
        walk
    }

    private void Update()
    {
        if (GameManager.Instance.GameStatus != EGameStatus.Loading)
        {
            Move();
        }
    }

    private void Move()
    {
        //Debug.Log(_joystick.Horizontal + " " + _joystick.Vertical);
        Vector3 movement = new Vector3(_joystick.Horizontal * _speed, _gravity, _joystick.Vertical * _speed);

        _controller.Move(movement * Time.deltaTime);

        if (_moveStatus != EMoveStatus.idle)
        {
            _moveStatus = EMoveStatus.idle;
        }

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _controller.transform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));

            if (_moveStatus != EMoveStatus.walk)
            {
                _moveStatus = EMoveStatus.walk;
            }
        }
    }
}