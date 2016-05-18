using UnityEngine;
using System.Collections;

public class Parallex : MonoBehaviour {

	[SerializeField]
	protected Vector3 scrollingDirection = Vector3.zero;
	[SerializeField]
	public float scrollingSpeed;


	void Update () {
		if (scrollingDirection != Vector3.zero && !GameManager.instance.pause) {
			this.transform.position += scrollingDirection * scrollingSpeed * Time.deltaTime;
		}
	}


}
