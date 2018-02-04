using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeComponent : MonoBehaviour {

    public float cooldownRate;
    public float shakeAmountTotal;
    public float shakeAmountTranslation;
    public float shakeAmountRotation;

	void Start(){

	}

	void Update(){

        shakeAmountTotal -= cooldownRate * Time.deltaTime;
        shakeAmountTotal = Mathf.Clamp(shakeAmountTotal, 0.0f, 1.0f);

        if(shakeAmountTotal > 0.0f){
            transform.localPosition = new Vector3(shakeAmountTotal * shakeAmountTranslation * Mathf.Sin(29.0f * Time.time),
                                                  200.0f + 20.0f * shakeAmountTotal * shakeAmountTranslation * Mathf.Cos(27.0f * Time.time),
                                                  shakeAmountTotal * shakeAmountTranslation * Mathf.Cos(33.0f * Time.time));
            transform.rotation = Quaternion.Euler(90.0f, 0.0f, shakeAmountTotal * shakeAmountRotation * (Mathf.Sin(29.0f * Time.time) + Mathf.Sin(50.0f * Time.time)));
        }
	}

    public void AddSmallShake(){
        shakeAmountTotal += 0.3f;
        shakeAmountTotal = Mathf.Clamp(shakeAmountTotal, 0.0f, 1.0f);
    }

    public void AddMediumShake(){
        shakeAmountTotal += 0.6f;
        shakeAmountTotal = Mathf.Clamp(shakeAmountTotal, 0.0f, 1.0f);
    }

    public void AddLargeShake(){
        shakeAmountTotal += 0.9f;
        shakeAmountTotal = Mathf.Clamp(shakeAmountTotal, 0.0f, 1.0f);
    }

    public void AddFriesWithThat(){

    }
}
