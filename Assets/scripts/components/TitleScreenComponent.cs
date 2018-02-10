using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenComponent : MonoBehaviour {

    public float tPlayerDriver = 0.0f;
    public float transitionTime;

    private Timer transitionTimer;

    public GameObject blockingPlane;
    public GameObject uiCanvas;

    void Start(){
        Time.timeScale = 1.0f;
        transitionTimer = new Timer(transitionTime);

        // temp
        transitionTimer.Start();
        uiCanvas.SetActive(false);
    }

    void Update(){
        tPlayerDriver = transitionTimer.Parameterized();

        if(tPlayerDriver > 0.5f){
            blockingPlane.GetComponent<Renderer>().enabled = false;
            uiCanvas.SetActive(true);
        }
    }
}
