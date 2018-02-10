using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenComponent : MonoBehaviour {

    public float tPlayerDriver = 0.0f;
    public float transitionTime;

    private Timer transitionTimer;

    public GameObject blockingPlane;
    public GameObject title;
    public GameObject uiCanvas;
    public GameManagerComponent gameManager;

    public GameObject spawner;

    private bool started = false;

    void Start(){
        Time.timeScale = 1.0f;
        transitionTimer = new Timer(transitionTime);

        uiCanvas.SetActive(false);
    }

    void Update(){

        if(!started && Input.GetMouseButtonDown(0)) {
            transitionTimer.Start();
            started = true;
        }

        if(started){
            tPlayerDriver = transitionTimer.Parameterized();

            if(tPlayerDriver > 0.5f){
                blockingPlane.GetComponent<Renderer>().enabled = false;
                title.GetComponent<Renderer>().enabled = false;
                uiCanvas.SetActive(true);
                gameManager.StartGameTimer();
            }

            if(tPlayerDriver > 0.9f){
                spawner.GetComponent<RandomSpawnerComponent>().StartSpawning();
            }
        }
    }
}
