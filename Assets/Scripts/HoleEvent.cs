using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleEvent : MonoBehaviour {

    private AudioSource _holeSound;
    public GameManager manager;

	// Use this for initialization
	void Start () {
        _holeSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator OnTriggerEnter(Collider other)
    {
        _holeSound.PlayDelayed(0.7f);
        yield return new WaitForSeconds(2);
        if (other.tag == "Ball")
        {
            Destroy(other.gameObject);
            manager.GameEnded();
        }
    }
}
