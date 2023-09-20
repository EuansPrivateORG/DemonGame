using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBuy : ShopInteractable
{
	public bool upgraded;
	[SerializeField] public Perk perk;
	
	protected override void DoBuy(Interactor interactor)
	{
		interactor.perkManager.AddPerk(Instantiate(perk));
		if(upgraded)
		{
			Upgrade(interactor.perkManager);
		}
	}
	
	protected override bool CanBuy(Interactor interactor)
	{
		return !interactor.perkManager.HasPerk(perk);
	}
	
	public void Upgrade(PerkManager perkManager)
	{
		if(perkManager.HasPerk(perk))
		{
			perkManager.GetPerk(perk).Upgrade();
			upgraded = true;
		}
	}
	[ContextMenu("Upgrade")]
	void TestUpgrade()
	{
		Upgrade(FindObjectOfType<PerkManager>());
	}

	

}
