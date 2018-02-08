using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour {

    private Plane plane;

    public float shields;
    public float shieldsDrainRate;

    public GameObject offset;

    public GameObject wing1;
    public GameObject wing2;

    public GameObject particle;

    public GameObject bulletPrefab;

    public GameObject overshield;

	void Start(){
        Cursor.visible = false;

        plane = new Plane(Vector3.up, 0.0f);
	}

	void Update(){
        Vector3 prevposition = transform.position;

        Ray ray;
        PixelCameraComponent pixelcam = Camera.main.GetComponent<PixelCameraComponent>();
        ray = Camera.main.ScreenPointToRay(pixelcam.enabled ? pixelcam.MouseToScreen() : Input.mousePosition);

        float enter = 0.0f;
        if(plane.Raycast(ray, out enter)){
            transform.position = ray.GetPoint(enter);
        }

        offset.transform.localPosition = new Vector3(0.1f * Mathf.Sin(10.0f * Time.time), 0.0f, 0.1f * Mathf.Cos(15.0f * Time.time));

        float zdeltascale = (transform.position.z - prevposition.z);
        float xdeltascale = (transform.position.x - prevposition.x);

        zdeltascale = Mathf.Clamp(zdeltascale, -2.0f, 2.0f);
        xdeltascale = Mathf.Clamp(xdeltascale, -2.0f, 2.0f);

        transform.rotation = Quaternion.Euler(-30.0f * zdeltascale, 180.0f, 20.0f * xdeltascale);

        wing1.transform.localRotation = Quaternion.Euler(-90.0f + 20.0f * Mathf.Sin(60.0f * Time.time), 0.0f, -180.0f);
        wing2.transform.localRotation = Quaternion.Euler(-90.0f + -20.0f * Mathf.Sin(60.0f * Time.time), 0.0f, -180.0f);

        if(Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1)){
            GameObject bullet = Object.Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.GetComponent<DamagingComponent>().creator = gameObject;
            particle.GetComponent<ParticleSystem>().Play();
            Camera.main.GetComponent<CameraShakeComponent>().AddSmallShake();
        }

        if(Input.GetMouseButton(1) && shields > 0.0f){
            overshield.GetComponent<Renderer>().enabled = true;
            GetComponent<DamageableComponent>().invincible = true;

            shields -= shieldsDrainRate * Time.deltaTime;
            shields = Mathf.Max(shields, 0.0f);
        } else {
            GetComponent<DamageableComponent>().invincible = false;
            overshield.GetComponent<Renderer>().enabled = false;
        }
	}
}
