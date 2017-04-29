using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleVelocity : MonoBehaviour {

	public float emissionFactor = 2f;

	public Rigidbody player;

	private ParticleSystem _ps;
	// Use this for initialization
	void Start () {
		_ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Vector3 emitPos = player.transform.position - player.velocity.normalized;
		emitPos.y= 0f;

		_ps.transform.LookAt(emitPos, transform.up);
		_ps.transform.position = emitPos;
		var em = _ps.emission;
		em.rateOverTime = player.velocity.magnitude * emissionFactor;
	}
}
