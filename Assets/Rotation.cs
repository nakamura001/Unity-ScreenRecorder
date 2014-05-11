using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	void Update () {
		transform.Rotate(new Vector3(0, Time.deltaTime*100f, 0));
	}
}
