using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Material))]
public class FadeLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.sortingLayerName = "Scenery";
		renderer.sortingOrder = 1000;
	}
}
