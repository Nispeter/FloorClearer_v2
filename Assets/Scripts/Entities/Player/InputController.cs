using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InputController : MonoBehaviour
{
    private FirstPersonMovement playerMovement;
    private PlayerSpellCasting playerSpellCasting;
    private PlayerInteract playerInteract;
    private PlayerCamera playerCamController;
    private Dashing dashingController;
    private bool isCombatMode;

    private bool isMovementBoosted = false;
    private bool inputEnabled = true;
    private bool onlyPauseEnabled = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "HUB")
        {
            isCombatMode = false;
        }
        else
        {
            isCombatMode = true;
        }
        dashingController= GetComponent<Dashing>();
        playerCamController = GetComponentInChildren<PlayerCamera>();
        playerMovement = GetComponent<FirstPersonMovement>();
        playerSpellCasting = GetComponent<PlayerSpellCasting>();
        playerInteract = GetComponent<PlayerInteract>();
    }

    public void MovementBoost(float boostDuration){
        StartCoroutine(ActivateBoost(boostDuration));
    }
    public IEnumerator ActivateBoost(float boostDuration){
        playerMovement.airJumps++;
        isMovementBoosted = true;
        yield return new WaitForSeconds(boostDuration);
        DeactivateBoost();
    }

    public void DeactivateBoost(){
        playerMovement.airJumps--;
        isMovementBoosted = false;
    }

    public void DeactivateAllExceptPause()
    {
        inputEnabled = false;
        onlyPauseEnabled = true;
    }

    public void ReactivateAllInput()
    {
        inputEnabled = true;
        onlyPauseEnabled = false;
    }

    public void DialogueMode(){
        DeactivateAllExceptPause();
        playerCamController.isDialogueActive = true;
        UnlockCursor();
    }

    public void DialogueModeExit(){
        ReactivateAllInput();
        playerCamController.isDialogueActive = false;
        LockCursor();
    }

    private void Update()
    {
        if (InGameManager.Instance.timeManager.isGamePaused)
            return;
        if (!inputEnabled && !onlyPauseEnabled)
            return;
        PauseInput();
        if (onlyPauseEnabled)
            return;

        MovementInput();
        InteractInput();
        if (!isCombatMode)
            return;
        CombatInput();
    }

    private void MovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isWalking = Input.GetKey(KeyCode.LeftControl);

        playerMovement.HandleMovement(horizontalInput, verticalInput, isSprinting, isWalking);

        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.HandleJump();
        }
        if (Input.GetKeyDown(KeyCode.X) && isMovementBoosted)
        {
            dashingController.StartDash();
        }
        if (dashingController.isDashing)
        {
            dashingController.ContinueDash();
        }
    }

    private void PauseInput(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!InGameManager.Instance.timeManager.isGamePaused)
            {
                InGameManager.Instance.OpenPauseScreen();
            }
            else
            {
                InGameManager.Instance.ExitPauseScreen();
            }
        }
    }

    private void InteractInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerInteract.Interact();
        }
    }

    private void CombatInput()
    {
        if (
            Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.R) ||
            Input.GetKeyDown(KeyCode.E)
        )
        {
            KeyCode keyPressed = ConvertToKeyCode(Input.inputString);
            playerSpellCasting.CastSpellBasedOnKey(keyPressed);
        }
        if (Input.GetMouseButtonDown(0))
        {
            playerSpellCasting.CastSpell();
        }
    }

    KeyCode ConvertToKeyCode(string input)
    {
        if (input.Length == 1)
        {
            char character = input[0];
            Debug.Log(input);
            if (char.IsDigit(character))
            {
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + character);
            }
            else if (char.IsLetter(character))
            {
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), character.ToString().ToUpper());
            }
        }

        return KeyCode.None;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
