using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        transform.SetParent(GameObject.Find("PowerupInactive").transform);
        BoxSpwanner.powerStack.Push(transform.gameObject);
        StartCoroutine(hdObj());
    }

    IEnumerator hdObj()
    {
        yield return new WaitForSeconds(0.01f);
        transform.gameObject.SetActive(false);
    }


}
