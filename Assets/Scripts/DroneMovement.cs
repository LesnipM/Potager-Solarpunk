using UnityEngine;
using UnityEngine.InputSystem;

/// <sumary>
/// Dépalce le drone dans plan horizontal (vue de dessus),
/// à hauteur  constante, au clavier comme à la manette.
/// </sumary>

public class DroneMovement : MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] private float speed = 6f;

    [Header("Entrées")]
    [SerializeField] private InputActionReference moveAction;

    private float flightHeight;

    private void Start()
    {
        // La hauteur de départ placée dans la scène devient la hauteur de vol.
        flightHeight = transform.position.y;
    }

    private void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.action.Disable();
        }
    }

    private void Update()
    {
        if (moveAction == null)
        {
            return;
        }

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y);

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Garde toujours la même hauteur au dessus du sol
        Vector3 position = transform.position;
        position.y = flightHeight;
        transform.position = position;
    }
}