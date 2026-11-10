using UnityEngine;
using UnityEngine.InputSystem;

//récolte la plantation quand le drone est au-dessus, que le légume est mûr et que le joueur appuie sur Harvest.
[RequireComponent(typeof(PlantGrowth))]
public class PlantHarvest : MonoBehaviour
{
    [Header("Entrée")]
    [SerializeField] private InputActionReference harvestAction;

    private PlantGrowth growth;
    private ScoreManager scoreManager;

    // Compteur et non booléen : robuste si le drone a plusieurs colliders.
    private int droneColliderCount;
    private bool isHarvested;

    private bool IsDroneAbove => droneColliderCount > 0;

    private void Awake()
    {
        growth = GetComponent<PlantGrowth>();
    }

    private void OnEnable()
    {
        if (harvestAction == null) return;
        harvestAction.action.Enable();
        harvestAction.action.performed += OnHarvestPerformed;
    }

    private void OnDisable()
    {
        if (harvestAction == null) return;
        // On se désabonne seulement : l'action est partagée par toutes les plantations, on ne la désactive pas.
        harvestAction.action.performed -= OnHarvestPerformed;
    }

    public void Initialize(ScoreManager manager)
    {
        scoreManager = manager;
    }

    private void OnHarvestPerformed(InputAction.CallbackContext context)
    {
        if (isHarvested || !IsDroneAbove) return;

        // Seul un légume mûr est récolté.
        if (!growth.IsMature) return;

        Harvest();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<DroneMovement>() != null)
        {
            droneColliderCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<DroneMovement>() != null)
        {
            droneColliderCount = Mathf.Max(0, droneColliderCount - 1);
        }
    }

    private void Harvest()
    {
        isHarvested = true;

        if (scoreManager != null)
        {
            scoreManager.AddHarvest();
        }

        Destroy(gameObject);
    }
}
