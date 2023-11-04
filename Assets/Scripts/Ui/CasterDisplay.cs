using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CasterDisplay : MonoBehaviour
{
    [SerializeField] Image[] icons;
    [SerializeField] Slider bloodMeter;
    [SerializeField] CanvasGroup onEmptyVisualization;
    
    [SerializeField] PlayerAbilityCaster caster;

    [SerializeField] float frequncey;
    [SerializeField] float damping;
    [SerializeField] float theOtherOne;
    SecondOrderDynamics dynamics;

    private bool isEmpty = false;
    // Start is called before the first frame update
    void Awake()
    {
        caster = FindObjectOfType<PlayerAbilityCaster>();
        
    }

	private void Start()
	{
        dynamics = new SecondOrderDynamics(frequncey, damping, theOtherOne, caster.caster.blood / caster.caster.maxBlood);
    }

	// Update is called once per frame
	void Update()
    {
        for(int i = 0; i < icons.Length; i++)
		{
            icons[i].sprite = caster.caster.abilities[i].icon;
            icons[i].GetComponent<Outline>().enabled = i == caster.activeIndex;
		}
        bloodMeter.value = dynamics.Update(Time.unscaledDeltaTime,caster.caster.blood / caster.caster.maxBlood);
        if(bloodMeter.value <= 0.1f)
        {
            isEmpty = true;

        }
        else
        {
            isEmpty = false;
            onEmptyVisualization.DOKill();
            onEmptyVisualization.alpha = 0f;
        }
    }

    private void TweenEmptyVisals()
    {
        onEmptyVisualization.DOFade(1, 2f).SetLoops(-1, LoopType.Yoyo);
    }
}
