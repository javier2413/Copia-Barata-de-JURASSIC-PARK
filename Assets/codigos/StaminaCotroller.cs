using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaCotroller : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float jumpCost = 20;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool weAreSprinting = false;

    [Header("Stamina Main Parameters")]
    [Range(0, 50)] [SerializeField] private float staminaDrain = 0.5f;
    [Range(0,50)] [SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina Main Parameters")]
    [SerializeField] private int slowedRunSpeed = 4;
    [SerializeField] private int normalRunSpeed = 8;

    [Header("Stamina UI -elements")]
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private Canvas sliderCanvasGrouo = null;

    private PlayerController controlesPlayer;

    private void Start()
    {
        controlesPlayer = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!weAreSprinting)
        {
            if (playerStamina <= maxStamina - 0.01)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                //updateStamina

                if ( playerStamina >= maxStamina)
                {
                    // set to normal speed
                    // reset  our alpha value slider 
                    hasRegenerated = true;
                }
            }
        }
    }

    void UpdateStamina (int valune)
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;
    }
}
