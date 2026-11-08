using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Fait apparaitre une graine sous le drone quand le joueur appuie sur l'action Plant.
public class DronePlanter : MonoBehaviour
{
    [Header("Entrées")]
    [SerializeField] private InputActionReference plantAction;

    [Header("Plantation")]
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private Transform SeedSpawnPoint;
    [SerializeField] private float plantCooldown = 0.3f;

    private float nextPlantTime;

    private void OnEnable()
    {
        if (plantAction == null) return;
        plantAction.action.Enable();
        plantAction.action.performed += OnPlantPerformed;
    }

    private void OnDisable()
    {
        if (plantAction == null) return;
        plantAction.action.performed -= OnPlantPerformed;
        plantAction.action.Disable();
    }

    private void OnPlantPerformed(InputAction.CallbackContext context)
    {
        if (Time.time < nextPlantTime) return;
        if (seedPrefab == null || SeedSpawnPoint == null) return;

        Instantiate(seedPrefab, SeedSpawnPoint.position, Quaternion.identity);
        nextPlantTime = Time.time + plantCooldown;
    }
}
