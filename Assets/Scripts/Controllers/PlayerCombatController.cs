using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
	public Weapon Weapon;

	private Animator animator;
	private bool isWeaponOnHands = false;
	private float angle;
	private bool isFacingRight
	{
		get => GetComponent<PlayerMovementController>()?.isFacingRight ?? true;
	}

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			animator.SetTrigger("Punch");

		if (Input.GetKeyDown(KeyCode.Alpha2))
			animator.SetTrigger("Kick");

		if (Input.GetMouseButtonDown(1))
			SwitchGun();

		if (isWeaponOnHands)
		{
			if (Input.GetMouseButton(0)) Shoot();

			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = new Vector2(mousePosition.x - Weapon.transform.position.x, mousePosition.y - Weapon.transform.position.y);
			 angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			if (!isFacingRight)
			{
				if (angle > 0) angle = 180 - angle;
				else angle = -(angle + 180);
			}

			//поворот оружия
			// Из-за вращения костей рук спрайт пистолета при угле в 0 градусов повернут вниз; 
			// Поэтому тут доп. переменная, чтобы "отогнуть" его наверх (угол найден опытым путем)
			float compensationAngle = 66f;
			Weapon.LocalRotation = Quaternion.Euler(0, 0, angle + compensationAngle);

			//поворот рук
			animator.SetFloat("Degree", angle);

			// должно быть внизу, чтобы angle обновился
		}
	}
	private void Shoot()
	{
		if (GetComponent<PlayerCarryingController>() && GetComponent<PlayerCarryingController>().IsCarrying)
			return;
		Weapon.Fire(isFacingRight);
	}
	private void SwitchGun()
	{
		if (isWeaponOnHands)
		{
			animator.SetTrigger("RemoveGun");
			Weapon.enabled = isWeaponOnHands = false;
			Weapon.LocalRotation = Quaternion.Euler(0, 0, 0);
		}
		else
		{
			animator.SetTrigger("TakeGun");
			Weapon.enabled = isWeaponOnHands = true;
		}
	}
}
