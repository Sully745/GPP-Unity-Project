using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour {

    public GameObject platform;
    public float shake_duration;
    public float magnitude;
    // Use this for initialization

    private Vector3 velocity = Vector3.zero;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(ShakeAndDestroy(shake_duration, magnitude));
        }
    }

    public IEnumerator ShakeAndDestroy(float duration, float magnitude)
    {
        Vector3 original_pos = platform.transform.position;

        float elapsed = 0;
        while (elapsed < duration)
        {
            Vector3 new_pos = original_pos;
            new_pos += new Vector3(Random.Range(-1, 1) * magnitude, Random.Range(-1, 1) * magnitude, 0);

            platform.transform.position = Vector3.SmoothDamp(platform.transform.position, new_pos, ref velocity, 0);

            //transform.position = new_pos;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = original_pos;
        platform.SetActive(false);
        yield return new WaitForSeconds(3);
        platform.SetActive(true);
    }
}
