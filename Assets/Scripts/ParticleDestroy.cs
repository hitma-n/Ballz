using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(destroyAf2());
	}

    IEnumerator destroyAf2()
    {
        yield return new WaitForSeconds(2);
        //Destroy(gameObject);
        BoxSpwanner.particles.Push(transform.gameObject.GetComponent<ParticleSystem>());
    }
}
