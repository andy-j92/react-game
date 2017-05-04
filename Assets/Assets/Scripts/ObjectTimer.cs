using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimer : MonoBehaviour {

    public float timer;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        StartCoroutine(ScaleOverTime(timer));
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Vector3 destinationScale = GameObject.FindWithTag("Target").transform.localScale;

        Debug.Log(destinationScale);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }
}
