using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShotDetector : MonoBehaviour
{
	public BodyParts BodyPart;
	private Health health;

	public void InitializeHealth(Health health)
	{
		this.health = health;
	}
	private void OnTriggerEnter2D(Collider2D otherColl)
	{
		var bullet = otherColl.transform.GetComponent<Bullet>();
		if (bullet!=null)
			health.EstimateDamage(bullet, BodyPart);
	}
}

