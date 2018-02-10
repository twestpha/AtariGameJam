using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotatorComponent : MonoBehaviour {

    private Timer spinUpTimer;
    private Timer spinDownTimer;

    public bool up;

    public GameObject a;
    public GameObject b;

	// Use this for initialization
	void Start () {
        spinDownTimer = new Timer(2.5f);
        spinUpTimer = new Timer(2.5f);
        spinUpTimer.Start();
        up = true;
	}

	// Update is called once per frame
	void Update () {
        if(up){
            a.transform.localScale = new Vector3(1.0f, 50.0f, spinUpTimer.Parameterized());
            b.transform.localScale = new Vector3(1.0f, 50.0f, spinUpTimer.Parameterized());
        } else {
            a.transform.localScale = new Vector3(1.0f, 50.0f, 1.0f - spinDownTimer.Parameterized());
            b.transform.localScale = new Vector3(1.0f, 50.0f, 1.0f - spinDownTimer.Parameterized());
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
