using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HitDetector : MonoBehaviour
{
	private Animator animator;
	//private float distanceOfIdle = 1f;
	private List<Transform> lastTransforms = new List<Transform>();
	void Start()
	{
		animator = this.GetComponent<Animator>();
	}
	private void Update()
	{
		//if (lastTransforms.Count > 0)
		//{
		//	for (int i = 0; i < lastTransforms.Count; i++)			
		//		if (Mathf.Abs(lastTransforms[i].position.x - this.transform.position.x) > distanceOfIdle)
		//			lastTransforms.Remove(lastTransforms[i]);

		//}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		animator.SetTrigger("GetHit");
		lastTransforms.Add(collision.transform);

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{

		animator.SetTrigger("GetHit");
		lastTransforms.Add(collision.transform);

	}
}
