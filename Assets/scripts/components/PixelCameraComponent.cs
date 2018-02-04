using UnityEngine;
using System.Collections;

public class PixelCameraComponent : MonoBehaviour {

    private float texelsToScreenX;
    private float texelsToScreenY;

    public RenderTexture renderTexture;

    void Start() {
        Cursor.visible = false;

        GetComponent<Camera>().targetTexture = renderTexture;

        texelsToScreenX = (float) Screen.width / (float) renderTexture.width;
        texelsToScreenY = (float) Screen.height / (float) renderTexture.height;
    }

    void OnGUI() {
        GUI.depth = 20;
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), renderTexture);
    }

    public Vector3 MouseToScreen(){
        Vector3 mouseposition = Input.mousePosition;
        return new Vector3(mouseposition.x / texelsToScreenX, mouseposition.y / texelsToScreenY, mouseposition.z);
    }

}
