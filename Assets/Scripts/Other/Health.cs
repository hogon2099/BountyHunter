using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public List<ShotDetector> ShotDetectors;
	public int MaxHP = 10;
	[SerializeField] private int hp;
	public int HP
	{
		get => hp;
		set
		{
			hp = value;
			if (hp > MaxHP) hp = MaxHP;
			if (hp <= 0)
			{
				hp = 0;
				IsDead = true;
			}
			
		}
	}
	//public bool IsEnemy = false;

	[SerializeField] private bool _isOnLegs = true;
	public bool IsOnLegs
	{
		get => _isOnLegs;
		set => _isOnLegs = value;
	}

	[SerializeField] private bool _IsOnStun = false;
	public bool IsOnStun
	{
		get => _IsOnStun;
		set => _IsOnStun = value;
	}

	[SerializeField] private bool _isDead = false;
	public bool IsDead
	{
		get => _isDead;		
		set
		{
			_isDead = true;
			IsOnLegs = false;
			IsOnStun = false;
		}
	}

	[SerializeField]  private bool _isOnSleep = false;
	public bool IsOnSleep
	{
		get =>  _isOnSleep;	
		set
		{
			_isOnSleep = true;
			IsOnLegs = false;
			IsOnStun = false;
		}
	}


	// Start is called before the first frame update
	void Start()
	{
		HP = MaxHP;
		foreach (var shotDetector in ShotDetectors)
			shotDetector.InitializeHealth(this);
	}

	// Update is called once per frame
	void Update()
	{

	}
	private void ReduceHealth(int damagePoints)
	{ 
		HP -= damagePoints;
	}

	private void RestoreHealth(int healthPoints)
	{
		HP += healthPoints;
	}

	public void EstimateDamage(Bullet bullet, BodyParts bodyPart)
	{
		if(bullet.IsLethal) 
		{
			if(bodyPart == BodyParts.Head)
				ReduceHealth(MaxHP);

			if (bodyPart == BodyParts.Body)
			{
				ReduceHealth(Mathf.CeilToInt(MaxHP / 3.0f)); // За сколько выстрелов можно убить врага
				IsOnStun = true;
			}

			if (bodyPart == BodyParts.Legs)
			{
				IsOnStun = true;
				IsOnLegs = false;
			}
		}
		else // при попадании нелеталки в любую часть тела врага вырубает
		{
			IsOnSleep = true;
			IsOnLegs = false;
		}
	}
}
