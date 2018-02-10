using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotatorComponent : MonoBehaviour {

    public float spinUpTime;
    public float spinDownTime;

    private Timer spinUpTimer;
    private Timer spinDownTimer;

    public bool up;

    public GameObject a;
    public GameObject b;

	// Use this for initialization
	void Start () {
        spinDownTimer = new Timer(spinDownTime);
        spinUpTimer = new Timer(spinUpTime);
        spinUpTimer.Start();
        up = true;
	}

	// Update is called once per frame
	void Update () {
        if(up){
            a.transform.localScale = new Vector3(1.0f, 50.0f * spinUpTimer.Parameterized(), spinUpTimer.Parameterized());
            b.transform.localScale = new Vector3(1.0f, 50.0f * spinUpTimer.Parameterized(), spinUpTimer.Parameterized());
        } else {
            a.transform.localScale = new Vector3(1.0f, 50.0f * 1.0f - spinDownTimer.Parameterized(), 1.0f - spinDownTimer.Parameterized());
            b.transform.localScale = new Vector3(1.0f, 50.0f * 1.0f - spinDownTimer.Parameterized(), 1.0f - spinDownTimer.Parameterized());
        }

        if(spinUpTimer.Finished() && up){
            spinDownTimer.Start();
            up = false;
        }

        if(spinUpTimer.Finished() && spinDownTimer.Finished()){
            Destroy(gameObject);
        }
	}
}
