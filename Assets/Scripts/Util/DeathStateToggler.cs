using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateToggler : MonoBehaviour
{
    [SerializeField] bool activeOnAlive;

    [Tooltip("If this is false the below variables do not matter")]
    [SerializeField] bool deathStateMatChange;
    [SerializeField] Material deathStateMat;
    [SerializeField] GameObject objToChangeMat;

    private Material oldMat;
    private Renderer objRenderer;

    private void Awake()
    {
        if (deathStateMat)
        {
            objRenderer = objToChangeMat.GetComponent<Renderer>();
            oldMat = objRenderer.material;
        }
    }

    public void Toggle(bool alive)
    {
        gameObject.SetActive(alive == activeOnAlive);

        if (alive)
        {
            if (deathStateMatChange) objRenderer.material = oldMat; 
        }
        else
        {
            if (deathStateMatChange) objRenderer.material = deathStateMat;
        }
    }
}
