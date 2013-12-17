using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleLayer : MonoBehaviour {

	public string sortingLayer;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().renderer.sortingLayerName = sortingLayer;
	}
}
