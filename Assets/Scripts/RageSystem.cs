using System.Collections; 
using UnityEngine;
using UnityEngine.UI;

public class RageSystem : MonoBehaviour
{
    public static RageSystem Instance;

    [Header("Rage Settings")]
    public float maxRage = 100f;
    public float currentRage = 0f;
    public Slider rageSlider;

    [Header("Rage Buffs")]
    public float rageDuration = 10f;
    public float speedMultiplier = 1.8f;
    public Vector3 axeScaleMultiplier = new Vector3(2f, 2f, 1f);
    
    private bool isRaging = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (rageSlider != null)
        {
            rageSlider.maxValue = maxRage;
            rageSlider.value = currentRage;
        }
    }

    public void AddRage(float amount)
    {
        if (isRaging) return;

        currentRage += amount;
        currentRage = Mathf.Clamp(currentRage, 0, maxRage);
        
        if (rageSlider != null)
        {
            rageSlider.value = currentRage;
        }

        if (currentRage >= maxRage)
        {
            StartCoroutine(ActivateRageMode());
        }
    }

    IEnumerator ActivateRageMode()
    {
        isRaging = true;
        Debug.Log("!!! PURPLE RAGE ACTIVATED !!!");

        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
        // 1. TRIGGER THE SOUND AND FLAG IN PLAYER CONTROLLER
        player.ActivateRage(); 

        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        Transform axe = player.attackPoint.GetChild(0);

        float originalSpeed = player.moveSpeed;
        Vector3 originalAxeScale = axe.localScale;

        player.moveSpeed *= speedMultiplier;
        axe.localScale = Vector3.Scale(axe.localScale, axeScaleMultiplier);
        playerSprite.color = new Color(0.6f, 0f, 1f); 

        float timer = rageDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            currentRage = (timer / rageDuration) * maxRage; 
            if (rageSlider != null) rageSlider.value = currentRage;
            yield return null; 
        }

        player.moveSpeed = originalSpeed;
        axe.localScale = originalAxeScale;
        playerSprite.color = Color.white;
        currentRage = 0;
        if (rageSlider != null) rageSlider.value = 0;
        isRaging = false;
        
        // 2. TELL PLAYER CONTROLLER RAGE IS OVER
        player.DeactivateRage(); 
        
        Debug.Log("Rage Mode Ended.");
    }
}