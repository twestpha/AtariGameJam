using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingMaterialComponent : MonoBehaviour {

    private Renderer rend;

	void Start(){
        rend = GetComponent<Renderer>();
	}

	void Update(){
        rend.material.SetTextureOffset("_MainTex", new Vector2(Mathf.Sin(Time.time), Mathf.Cos(Time.time * 1.3f)));
	}
}
