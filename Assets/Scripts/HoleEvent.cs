using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleEvent : MonoBehaviour {

    public GameManager manager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(2);
        if (other.tag == "Ball")
        {
            Destroy(other.gameObject);
            manager.GameEnded();
        }
    }
}
