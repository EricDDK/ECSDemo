using ECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagacyDrop : MonoBehaviour
{
	public float mass;
	public float delay;
	public float velocity;

	void Update()
	{
		if (this.delay > 0)
		{
			this.delay -= Time.deltaTime;
		}
		else
		{
			Vector3 pos = transform.position;
			float v = this.velocity + GravitySystem.G * this.mass * Time.deltaTime;
			pos.y += v;
			if (pos.y < GravitySystem.bottomY)
			{
				pos.y = GravitySystem.topY;
				this.velocity = 0f;
				this.delay = Random.Range(0, 10f);
			}
			transform.position = pos;
		}
	}
}