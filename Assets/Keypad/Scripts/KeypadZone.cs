using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadZone : MonoBehaviour
{
    public GameObject interactionUI; // Texto tipo "Presiona E para interactuar"
    public GameObject keypadCamera;
    public GameObject playerCamera;
    public GameObject keypadRoot; // Activa el teclado
    public GameObject playerController;

    private bool playerInside = false;

    private void Start()
    {
        keypadRoot.SetActive(false);
        keypadCamera.SetActive(false);
        interactionUI.SetActive(false);
    }

   void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            EnterKeypadMode();
        }

        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            EnterKeypadMode();
        }

        // NUEVO: salir con Escape
        if (Input.GetKeyDown(KeyCode.X))
        {
            ExitKeypadMode();
        }
    }

    private void EnterKeypadMode()
    {
        playerController.SetActive(false); // Desactiva control del jugador
        playerCamera.SetActive(false);
        keypadCamera.SetActive(true); // Activa cámara enfocada al keypad

        keypadRoot.SetActive(true); // Muestra el teclado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        interactionUI.SetActive(false);
    }

    public void ExitKeypadMode()
    {
        playerController.SetActive(true);
        playerCamera.SetActive(true);
        keypadCamera.SetActive(false);

        keypadRoot.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            interactionUI.SetActive(false);
        }
    }



}