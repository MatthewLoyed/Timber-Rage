using System.Collections; // 1. Necessary for Coroutines
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
        // Don't gain rage while already in Rage Mode
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

        // 1. Get references to components
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        
        // Assuming AxeVisual is the first child of AttackPoint
        Transform axe = player.attackPoint.GetChild(0);

        // 2. Save original stats to reset later
        float originalSpeed = player.moveSpeed;
        Vector3 originalAxeScale = axe.localScale;

        // 3. Apply the Buffs
        player.moveSpeed *= speedMultiplier;
        axe.localScale = Vector3.Scale(axe.localScale, axeScaleMultiplier);
        playerSprite.color = new Color(0.6f, 0f, 1f); // Deep Purple

        // 4. Countdown loop (drains the bar while you're in Rage Mode)
        float timer = rageDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            currentRage = (timer / rageDuration) * maxRage; // Visual drain
            if (rageSlider != null) rageSlider.value = currentRage;
            yield return null; // Wait for the next frame
        }

        // 5. Reset everything back to normal
        player.moveSpeed = originalSpeed;
        axe.localScale = originalAxeScale;
        playerSprite.color = Color.white;
        currentRage = 0;
        if (rageSlider != null) rageSlider.value = 0;
        isRaging = false;
        
        Debug.Log("Rage Mode Ended.");
    }
}