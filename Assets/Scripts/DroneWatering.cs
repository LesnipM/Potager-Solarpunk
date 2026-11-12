using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Arrose les plantations situées dans la zone trigger sous le drone
public class DroneWatering : MonoBehaviour
{
    [Header("Entrée")]
    [SerializeField] private InputActionReference waterAction;

    [Header("Effet")]
    [SerializeField] private ParticleSystem waterJet;

    [SerializeField] private AudioSource waterSound;

    private readonly List<PlantGrowth> plantsInZone = new List<PlantGrowth>();

    private void OnEnable()
    {
        if (waterAction != null) waterAction.action.Enable();
    }

    private void OnDisable()
    {
        if (waterAction != null) waterAction.action.Disable();
        if (waterJet != null) waterJet.Stop();
        if (waterSound != null) waterSound.Stop();
    }

    private void Update()
    {
        if (waterAction == null) return;

        bool isWatering = waterAction.action.IsPressed();
        UpdateJet(isWatering);

        if (!isWatering) return;

        // Une plantation détruite (éclatée) ne déclenche pas OnTriggerExit : on nettoie la liste.
        plantsInZone.RemoveAll(plant => plant == null);

        foreach (PlantGrowth plant in plantsInZone)
        {
            plant.Water();
        }
    }

    private void UpdateJet(bool isWatering)
    {
        UpdateWaterSound(isWatering);
        if (waterJet == null) return;

        if (isWatering && !waterJet.isPlaying) waterJet.Play();
        else if (!isWatering && waterJet.isPlaying) waterJet.Stop();
    }

    private void UpdateWaterSound(bool isWatering)
    {
        if (waterSound == null) return;

        if (isWatering && !waterSound.isPlaying) waterSound.Play();
        else if (!isWatering && waterSound.isPlaying) waterSound.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlantGrowth plant = other.GetComponentInParent<PlantGrowth>();
        if (plant != null && !plantsInZone.Contains(plant))
        {
            plantsInZone.Add(plant);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlantGrowth plant = other.GetComponentInParent<PlantGrowth>();
        if (plant != null)
        {
            plantsInZone.Remove(plant);
        }
    }
}
