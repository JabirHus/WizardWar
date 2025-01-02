using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("UI & Player Stats")]
    public GameObject popupPanel; // UI popup for selecting towers
    public PlayerStats playerStats; // Player stats for cost validation

    [Header("Tower Settings")]
    public int maxTowers = 5; // Max allowed towers
    private int currentTowerCount = 0; // Current placed towers

    [Header("Tower Prefabs & Costs")]
    public GameObject physicalTowerPrefab;
    public GameObject fireTowerPrefab;
    public GameObject iceTowerPrefab;
    public GameObject lightningTowerPrefab;

    public int physicalTowerCost = 50;
    public int fireTowerCost = 100;
    public int iceTowerCost = 150;
    public int lightningTowerCost = 200;

    [Header("Placement Settings")]
    public GameObject placementIndicatorPrefab; // Hover indicator prefab
    public GameObject rangeIndicatorPrefab; // Attack range prefab
    public Material rangeIndicatorMaterial; // Transparent grey material
    public Color validPlacementColor = Color.green;
    public Color invalidPlacementColor = Color.red;
    public Color lockedPlacementColor = Color.yellow;

    [Header("Range Indicator Settings")]
    public float placementRadius = 1.0f;
    public float defaultTowerRange = 5.0f; // Default range visualization size
    public float rangeIndicatorHeight = 0.3f; // Height adjustment for range indicator
    public float rangeIndicatorTransparency = 0.5f; // Default transparency

    private GameObject currentIndicator; // Hover indicator
    private GameObject rangeIndicator; // Range visualization
    private Renderer indicatorRenderer;
    private Renderer rangeRenderer;
    private Vector3 towerPlacementPosition;

    private bool isPlacementLocked = false; // Prevent hover updates during UI
    private bool maxTowersReached = false; // Lock placement after max towers

    void Start()
    {
        enabled = false;
        Invoke(nameof(EnableScript), 5f); // Enable the script after 5 seconds
    }

    private void EnableScript()
    {
        enabled = true; // Reactivate the script
        SetupPlacementIndicator();
        SetupRangeIndicator();
    }

    void Update()
    {
        if (maxTowersReached)
        {
            DisableIndicators();
            return;
        }

        if (!isPlacementLocked)
        {
            HandlePlacementHover();
        }

        if (Input.GetMouseButtonDown(0) && !isPlacementLocked && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleMouseClick();
        }
    }

    #region Placement Hover Logic

    private void SetupPlacementIndicator()
    {
        currentIndicator = Instantiate(placementIndicatorPrefab);
        indicatorRenderer = currentIndicator.GetComponent<Renderer>();
        currentIndicator.SetActive(false);
    }

    private void SetupRangeIndicator()
    {
        rangeIndicator = Instantiate(rangeIndicatorPrefab);
        rangeRenderer = rangeIndicator.GetComponentInChildren<Renderer>();

        if (rangeRenderer == null)
        {
            Debug.LogError("Renderer not found on RangeIndicator prefab or its children!");
        }

        if (rangeIndicatorMaterial != null)
        {
            rangeRenderer.material = rangeIndicatorMaterial;
            SetRangeIndicatorTransparency(rangeIndicatorTransparency);
        }

        UpdateRangeIndicatorSize(defaultTowerRange);
        rangeIndicator.SetActive(false);
    }

    private void UpdateRangeIndicatorSize(float radius)
    {
        rangeIndicator.transform.localScale = new Vector3(radius * 2, 0.1f, radius * 2);
    }

    private void SetRangeIndicatorTransparency(float alpha)
    {
        if (rangeRenderer != null)
        {
            Color color = rangeRenderer.material.color;
            color.a = alpha;
            rangeRenderer.material.color = color;
        }
    }

    private bool IsPlacementColliding()
    {
        Collider[] colliders = Physics.OverlapSphere(towerPlacementPosition, placementRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NoBuildZone") || collider.CompareTag("Tower") || collider.CompareTag("Enemy"))
            {
                Debug.Log($"Collision detected with: {collider.gameObject.name}");
                return true; // Found an obstruction or invalid area
            }
        }
        return false; // No collision detected
    }

    private bool IsPlacementAreaOccupied()
    {
        Collider[] colliders = Physics.OverlapSphere(towerPlacementPosition, placementRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Tower") || collider.CompareTag("NoBuildZone"))
            {
                return true; // Placement area is occupied
            }
        }
        return false; // Placement area is free
    }

    private void HandlePlacementHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            towerPlacementPosition = hit.point;
            currentIndicator.SetActive(true);
            rangeIndicator.SetActive(true);

            float fixedYHeight = 0.5f; // Fixed height for indicators
            currentIndicator.transform.position = new Vector3(towerPlacementPosition.x, fixedYHeight, towerPlacementPosition.z);
            rangeIndicator.transform.position = new Vector3(towerPlacementPosition.x, fixedYHeight + rangeIndicatorHeight, towerPlacementPosition.z);

            if (IsPlacementAreaOccupied() || IsPlacementColliding())
            {
                indicatorRenderer.material.color = invalidPlacementColor;
                rangeRenderer.material.color = invalidPlacementColor;
            }
            else
            {
                indicatorRenderer.material.color = validPlacementColor;
                rangeRenderer.material.color = validPlacementColor;
            }
        }
        else
        {
            currentIndicator.SetActive(false);
            rangeIndicator.SetActive(false);
        }
    }

    private void HandleMouseClick()
    {
        if (currentTowerCount >= maxTowers)
        {
            Debug.Log("Maximum number of towers reached. Cannot place more towers.");
            DisableIndicators();
            return;
        }

        if (IsPlacementAreaOccupied() || IsPlacementColliding())
        {
            Debug.Log("Cannot place a tower here. Area is occupied or invalid.");
            return;
        }

        isPlacementLocked = true;
        indicatorRenderer.material.color = lockedPlacementColor;
        rangeIndicator.SetActive(false);

        popupPanel.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        popupPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    #endregion

    #region Tower Deployment

    public void DeployPhysicalTower()
    {
        PlaceTower(physicalTowerPrefab, physicalTowerCost);
    }

    public void DeployFireTower()
    {
        PlaceTower(fireTowerPrefab, fireTowerCost);
    }

    public void DeployIceTower()
    {
        PlaceTower(iceTowerPrefab, iceTowerCost);
    }

    public void DeployLightningTower()
    {
        PlaceTower(lightningTowerPrefab, lightningTowerCost);
    }

    private bool PlaceTower(GameObject towerPrefab, int cost)
    {
        if (!playerStats.SpendSkillPoints(cost))
        {
            Debug.Log("Not enough skill points to place the tower.");
            CancelPopup();
            return false;
        }

        Instantiate(towerPrefab, towerPlacementPosition + Vector3.up * 0.05f, Quaternion.identity);
        currentTowerCount++;

        if (currentTowerCount >= maxTowers)
        {
            maxTowersReached = true;
            DisableIndicators();
        }

        CancelPopup();
        return true;
    }

    public void CancelPopup()
    {
        Debug.Log("Cancel button pressed");
        popupPanel.SetActive(false);
        Time.timeScale = 1f;
        isPlacementLocked = false;
        currentIndicator.SetActive(false);
        rangeIndicator.SetActive(false);
    }

    private void DisableIndicators()
    {
        currentIndicator.SetActive(false);
        rangeIndicator.SetActive(false);
        isPlacementLocked = true;
    }

    #endregion
}
