using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Responsável pela execução dos comportamentos relacionados a movimentação do player
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Parâmetros")]
    [SerializeField] private float _moveSpeed;

    [Header("Referências")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputActionReference _moveAction;

    // Não serializadas
    private Vector2 _moveInput = Vector2.zero;

    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _moveAction.action.performed += GetMoveInput;
        _moveAction.action.canceled += ResetMoveInput;
    }

    private void Update() => ApplyMovement();

    private void GetMoveInput(InputAction.CallbackContext contextValue)
    {
        _moveInput = contextValue.ReadValue<Vector2>();
    }

    private void ResetMoveInput(InputAction.CallbackContext contextValue)
    {
        _moveInput = Vector2.zero;
    }

    private void ApplyMovement() => _characterController.Move(new Vector3(_moveInput.x, 0f, _moveInput.y) * _moveSpeed * Time.deltaTime);
}
