using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class HealthDisplay : MonoBehaviour
{
    Health health;
    CanvasGroup canvasGroup;
    [SerializeField,Range(0,1)] float startinghealth, endHealth,maxAlpha;
    
    // Start is called before the first frame update
    void Awake()
    {
        health = FindObjectOfType<PlayerStats>().GetComponent<Health>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        canvasGroup.alpha = maxAlpha * (1 - Mathf.Clamp01((health.health/health.maxHealth) - startinghealth) / (endHealth - startinghealth));
    }
}
