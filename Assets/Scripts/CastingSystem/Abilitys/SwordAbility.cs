using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "abilities/Sword")]
public class SwordAbility : Ability
{
	[SerializeField] LayerMask layers;
	[SerializeField] float range;
	[SerializeField] float rad;
	[SerializeField] VfxSpawnRequest vfx;
	[SerializeField] float swingsPerMin;
	[SerializeField] float damage;
	[SerializeField] int points;
	float cooldown;
	float timer;

	protected override void OnEquip()
	{
		cooldown = 1 / (swingsPerMin / 60);
	}


	public override void Cast(Vector3 origin, Vector3 direction)
	{
		if (timer < cooldown)
			return;
		timer = 0;
		List<Health> healths = new List<Health>();

		caster.RemoveBlood(bloodCost);
		RaycastHit[] hits = Physics.SphereCastAll(origin,rad, direction, range, layers);
		foreach(RaycastHit hit in hits)
		{
			HitBox hb;
			if(hit.collider.TryGetComponent(out hb))
			{
				if (healths.Contains(hb.health))
					continue;
				healths.Add(hb.health);
				OnHit();
				hb.health.TakeDmg(damage);
				if(hit.point == Vector3.zero)
				{
					Vector3 pos = hit.collider.ClosestPoint(origin);
					vfx.Play(pos, pos - origin);
				}
				else
				{
					vfx.Play(hit.point, hit.normal);
				}
				
			}
		}
	}

	public override void Tick()
	{
		timer += Time.deltaTime;
	}
	public override void OnHit()
	{
		if (caster.playerStats.Enabled)
			caster.playerStats.Value.GainPoints(points);
	}
}
