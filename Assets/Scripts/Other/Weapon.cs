using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameObject Bullet;
	public Transform GunPoint;

	public bool isLethal;
	//private float shootingRange = 20;
	public float BulletSpeed = 20f;
	public Quaternion LocalRotation
	{
		set => transform.localRotation =  value;	
	}


	private float shootingRate = 1;
	private float delayTime = 0.5f;
	private float timeSinceShot = 100;

	private int clipCapacity = 15;
	private int ammoInClip;

	private float reloadTime = 3f;
	private float reloadCounter;
	public bool IsReloading = false;

	//private bool isOnHands = false;
	//public bool isOnPlayer;

	void Start()
	{
		reloadCounter = reloadTime * 2;
		ammoInClip = clipCapacity;

		Bullet bullet = Bullet.GetComponent<Bullet>();
		bullet.IsLethal = this.isLethal;
		if(isLethal)
			BulletSpeed = 100f;
		else
			BulletSpeed = 20f;
	}

	// Update is called once per frame
	void Update()
	{
		reloadCounter += Time.deltaTime;

		if (ammoInClip == 0 || (ammoInClip < clipCapacity && Input.GetKey(KeyCode.R)))
		{
			ammoInClip++;
			reloadCounter = 0;
			IsReloading = true;
		}
		if (Mathf.Round(reloadCounter) == reloadTime)
		{
			Reload();
			reloadCounter++;
			IsReloading = false;
		}
		if (reloadCounter == reloadTime * 10) // сброс счетчика, чтобы не убежал в бесконечность
			reloadCounter = reloadTime * 2;

		timeSinceShot += Time.deltaTime;
	}
	public void Fire(bool isFacingRight)
	{
		if (!IsReloading && timeSinceShot > delayTime && ammoInClip > 0)
		{
			int direction;
			if (isFacingRight) direction = 1; else direction = -1;

			// угол для пули 
			float bulletAngle = transform.rotation.eulerAngles.z;

			// спавним пулю
			Rigidbody2D clone = Instantiate(Bullet.GetComponent<Rigidbody2D>(), GunPoint.position, Quaternion.Euler(0, 0, bulletAngle)) as Rigidbody2D;

			// направление полета пули 					
			clone.velocity = new Vector2(
				direction * Mathf.Cos(bulletAngle * Mathf.Deg2Rad) * BulletSpeed,
				direction * Mathf.Sin(bulletAngle * Mathf.Deg2Rad) * BulletSpeed
				);

			// ТРЯСКА КАМЕРЫ
			//if (isOnPlayer)
			//	GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShaking>().Shake(false);

			//АНИМАЦИЯ ВЫСТРЕЛА
			//animator.SetTrigger("Fire"); 

			// Обнуляем время после выстрела
			timeSinceShot = 0;
			ammoInClip--;
		}
	}
	public void Reload()
	{
		IsReloading = true;
		ammoInClip = clipCapacity;
	}
}
