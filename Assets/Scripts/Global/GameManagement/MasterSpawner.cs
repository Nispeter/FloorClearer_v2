using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    public GameObject gameEnvPrefab;
    public GameObject canvasPrefab;
    public GameObject playerPrefab;

    private void Start()
    {
        // GameObject gameEnv = Instantiate(gameEnvPrefab);
        // GameObject canvas = Instantiate(canvasPrefab);
        // GameObject player = Instantiate(playerPrefab);

        // // Get the references from the canvas.
        // SpellUI spellUI = canvas.transform.Find("InGameUI/SpellSlots").GetComponent<SpellUI>();
        // HealthBar healthBar = canvas.transform.Find("InGameUI/HealthBar").GetComponent<HealthBar>();
        // PointCounter pointCounter = canvas.transform.Find("InGameUI/Points").GetComponent<PointCounter>();

        // // Set up references for the player.
        // PlayerController pc = player.GetComponent<PlayerController>();
        // pc.spellUI = spellUI;

        // // Set up references for the game environment.
        // InGameManager inGameManager = gameEnv.GetComponent<InGameManager>();
        // inGameManager.spellUI = spellUI;
        // inGameManager.healthBar = healthBar;
        // inGameManager.pointCounter = pointCounter;

        // // Fire off a game started event.
        // gameStartEvent.Raise();
    }
}