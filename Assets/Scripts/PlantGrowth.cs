using NUnit.Framework.Internal.Commands;
using UnityEngine;

public enum PlantStage
{
    Seed,
    Mature,
    Overripe
}

// Gère la croissance d'une plantation : graine, légume mûr, légume trop mûr, éclatement.
public class PlantGrowth : MonoBehaviour
{
    [Header("Durée des étapes (seconde)")]
    [SerializeField] private float seedDuration = 8f;
    [SerializeField] private float matureDuration = 10f;
    [SerializeField] private float overripeDuration = 6f;

    [Header("Arrosage")]
    [SerializeField] private float wateringGrowthMultiplier = 2f;
    [SerializeField] private float wateringMemoryTime = 0.2f;

    [Header("Visuels par étape")]
    [SerializeField] private GameObject seedVisual;
    [SerializeField] private GameObject matureVisual;
    [SerializeField] private GameObject overripeVisual;

    public PlantStage CurrentStage { get; private set; } = PlantStage.Seed;
    public bool IsMature => CurrentStage == PlantStage.Mature;

    public float stageTimer;
    public float wateredUntil;

    private bool IsWatered => Time.time < wateredUntil;

    private void Start()
    {
        ApplyStageVisual();
    }

    private void Update()
    {
        float growthSpeed = IsWatered ? wateringGrowthMultiplier : 1f;
        stageTimer += Time.deltaTime * growthSpeed;

        if (stageTimer >= GetStageDuration(CurrentStage))
        {
            stageTimer = 0f;
            AdvanceStage();
        }
    }

    // Appelé à chaque frame par le drone tant qu'il arrose cette plantation.
    public void Water()
    {
        wateredUntil = Time.time + wateringMemoryTime;
    }

    private float  GetStageDuration(PlantStage stage)
    {
        switch (stage)
        {
            case PlantStage.Seed: return seedDuration;
            case PlantStage.Mature: return matureDuration;
            default: return overripeDuration;
        }
    }

    private void AdvanceStage()
    {
        switch (CurrentStage)
        {
            case PlantStage.Seed:
                CurrentStage = PlantStage.Mature;
                break;
            case PlantStage.Mature:
                CurrentStage = PlantStage.Overripe;
                break;
            case PlantStage.Overripe:
                burst();
                return;
        }
        ApplyStageVisual();
    }

    private void ApplyStageVisual()
    {
        if (seedVisual != null) seedVisual.SetActive(CurrentStage == PlantStage.Seed);
        if (matureVisual != null) matureVisual.SetActive(CurrentStage == PlantStage.Mature);
        if (overripeVisual != null) overripeVisual.SetActive(CurrentStage == PlantStage.Overripe);
    }

    private void burst()
    {
        Destroy(gameObject);
    }
}