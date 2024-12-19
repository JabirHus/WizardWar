using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacementManager : MonoBehaviour
{
    public GameObject popupPanel; // Reference to the popup panel in the scene
    public PlayerStats playerStats; // Reference to the PlayerStats script
    public int maxTowers = 5; // Maximum number of towers allowed
    private int currentTowerCount = 0; // Current number of towers placed

    // Prefabs for different tower types
    public GameObject physicalTowerPrefab;
    public GameObject fireTowerPrefab;
    public GameObject iceTowerPrefab;
    public GameObject lightningTowerPrefab;

    // Costs for different towers
    public int physicalTowerCost = 50;
    public int fireTowerCost = 100;
    public int iceTowerCost = 150;
    public int lightningTowerCost = 200;

    private Vector3 towerPlacementPosition; // Stores the position where the tower will be placed

    void Update()
    {
        // Check for mouse click and ensure it's not over UI
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ShowPopup();
            }
        }
    }

    void ShowPopup()
    {

        // Check if the maximum tower limit is reached
        if (currentTowerCount >= maxTowers)
        {
            Debug.Log("Maximum number of towers reached. Cannot place more towers.");
            return;
        }

        // Pause the game
        Time.timeScale = 0f;

        // Determine the position where the tower should be placed
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Save the position to place the tower later
            towerPlacementPosition = hit.point;

            // Ensure the popup always appears at the center of the screen
            popupPanel.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            popupPanel.SetActive(true); // Show the popup
        }
    }

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
        // Check if the player has enough skill points
        if (!playerStats.SpendSkillPoints(cost))
        {
            Debug.Log("Not enough skill points to place the tower.");
            popupPanel.SetActive(false); // Hide popup if not enough points
            Time.timeScale = 1f; // Unpause the game 
            return false;
        }

        // Instantiate the selected tower at the saved position
        Instantiate(towerPrefab, towerPlacementPosition, Quaternion.identity);
        currentTowerCount++; // Increase the tower count
        Debug.Log("Tower placed successfully. Total towers: " + currentTowerCount);

        // Hide and reset popup
        popupPanel.SetActive(false);
        // Unpause the game 
        Time.timeScale = 1f;
        return true;
    }

    public void CancelPopup()
    {
        // Hide the popup without deploying a tower
        popupPanel.SetActive(false);
        // Unpause the game
        Time.timeScale = 1f;
    }
}
