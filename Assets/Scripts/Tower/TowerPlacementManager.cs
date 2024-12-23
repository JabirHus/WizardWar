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
    public Color validPlacementColor = Color.green;
    public Color invalidPlacementColor = Color.red;
    public Color lockedPlacementColor = Color.yellow;

    public float placementRadius = 1.0f;

    private GameObject currentIndicator; // Hover indicator
    private Renderer indicatorRenderer;
    private Vector3 towerPlacementPosition;

    private bool isPlacementLocked = false; // Prevent hover updates during UI

    void Start()
    {
        SetupPlacementIndicator();
    }

    void Update()
    {
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

    private void HandlePlacementHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            towerPlacementPosition = hit.point;
            currentIndicator.SetActive(true);
            currentIndicator.transform.position = towerPlacementPosition + Vector3.up * 0.05f;

            if (IsPlacementAreaOccupied())
            {
                indicatorRenderer.material.color = invalidPlacementColor;
            }
            else
            {
                indicatorRenderer.material.color = validPlacementColor;
            }
        }
        else
        {
            currentIndicator.SetActive(false);
        }
    }

    private void HandleMouseClick()
    {
        if (IsPlacementAreaOccupied())
        {
            Debug.Log("Cannot place a tower here. Area is occupied.");
            return;
        }

        // Lock placement indicator and show UI
        isPlacementLocked = true;
        indicatorRenderer.material.color = lockedPlacementColor;

        // Show Popup UI
        popupPanel.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        popupPanel.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    #endregion

    #region Tower Deployment

    public void DeployPhysicalTower()
    {
        DeployTower(physicalTowerPrefab, physicalTowerCost, "Physical tower deployed!");
    }

    public void DeployFireTower()
    {
        DeployTower(fireTowerPrefab, fireTowerCost, "Fire tower deployed!");
    }

    public void DeployIceTower()
    {
        DeployTower(iceTowerPrefab, iceTowerCost, "Ice tower deployed!");
    }

    public void DeployLightningTower()
    {
        DeployTower(lightningTowerPrefab, lightningTowerCost, "Lightning tower deployed!");
    }

    private void DeployTower(GameObject towerPrefab, int cost, string successMessage)
    {
        if (PlaceTower(towerPrefab, cost))
        {
            Debug.Log(successMessage);
        }
    }

    private bool PlaceTower(GameObject towerPrefab, int cost)
    {
        if (!playerStats.SpendSkillPoints(cost))
        {
            Debug.Log("Not enough skill points to place the tower.");
            popupPanel.SetActive(false); // Hide popup
            Time.timeScale = 1f; // Resume game
            isPlacementLocked = false;
            return false;
        }

        // Place the tower
        Instantiate(towerPrefab, towerPlacementPosition + Vector3.up * 0.05f, Quaternion.identity);
        currentTowerCount++;
        Debug.Log($"Tower placed successfully. Total towers: {currentTowerCount}");

        // Reset placement state
        popupPanel.SetActive(false);
        Time.timeScale = 1f; // Resume game
        isPlacementLocked = false;

        // Mark area as permanently occupied
        indicatorRenderer.material.color = invalidPlacementColor;
        return true;
    }

    public void CancelPopup()
    {
        // Hide the popup without deploying a tower
        popupPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPlacementLocked = false;
    }

    #endregion

    #region Validation Logic

    private bool IsPlacementAreaOccupied()
    {
        Collider[] colliders = Physics.OverlapSphere(towerPlacementPosition, placementRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Tower"))
            {
                return true; // Area is occupied
            }
        }
        return false;
    }

    #endregion
}
