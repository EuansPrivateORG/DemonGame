using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Perks/MaxHealth")]
public class MaxHealthPerk : Perk
{
	[SerializeField] int healthToGain;
	[SerializeField] int upgradeHealthToGain;
	protected override void OnEquip()
	{
		manager.GetComponent<Health>().maxHealth += healthToGain;
	}
	protected override void OnUpgrade()
	{
		Health health = manager.GetComponent<Health>();
		health.maxHealth -= healthToGain;
		health.maxHealth += upgradeHealthToGain;
	}

	protected override void OnUnEquip()
	{
		manager.GetComponent<Health>().maxHealth -= upgraded? upgradeHealthToGain : healthToGain;
	}
}
