using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour {

    private Plane plane;

    public float shields;
    public float maxShields;
    public float shieldsDrainRate;

    public GameObject offset;

    public GameObject wing1;
    public GameObject wing2;

    public GameObject particle;

    public GameObject bulletPrefab;

    public GameObject overshield;

    public GameObject titleScreen;
    private TitleScreenComponent titleScreenComp;

    public Vector3 titlePosition;

    public AudioSource shotAudioSource;
    public AudioSource shieldPickupAudioSource;

    private float prevhealth;

    public GameObject jukebox;

	void Start(){
        Cursor.visible = false;

        plane = new Plane(Vector3.up, 0.0f);

        titleScreenComp = titleScreen.GetComponent<TitleScreenComponent>();
	}

	void Update(){
        if(prevhealth > 0.0f && GetComponent<DamageableComponent>().currentHealth <= 0.0f){
            offset.transform.position = new Vector3(0.0f, 0.0f, 9000.0f);
            jukebox.GetComponent<JukeboxComponent>().PlayerDied();
        }

        if(GetComponent<DamageableComponent>().currentHealth <= 0.0f){
            return;
        }

        Vector3 prevposition = transform.position;

        Ray ray;
        PixelCameraComponent pixelcam = Camera.main.GetComponent<PixelCameraComponent>();
        ray = Camera.main.ScreenPointToRay(pixelcam.enabled ? pixelcam.MouseToScreen() : Input.mousePosition);


        Vector3 mouseDrivenPosition = Vector3.zero;
        Quaternion mouseDrivenRotation;

        float enter = 0.0f;
        if(plane.Raycast(ray, out enter)){
            mouseDrivenPosition = ray.GetPoint(enter);
        }

        offset.transform.localPosition = new Vector3(0.1f * Mathf.Sin(10.0f * Time.time), 0.0f, 0.1f * Mathf.Cos(15.0f * Time.time)) * titleScreenComp.tPlayerDriver;

        float zdeltascale = (transform.position.z - prevposition.z);
        float xdeltascale = (transform.position.x - prevposition.x);

        zdeltascale = Mathf.Clamp(zdeltascale, -2.0f, 2.0f);
        xdeltascale = Mathf.Clamp(xdeltascale, -2.0f, 2.0f);

        mouseDrivenRotation = Quaternion.Euler(-30.0f * zdeltascale, 180.0f, 20.0f * xdeltascale);

        wing1.transform.localRotation = Quaternion.Euler(-90.0f + 20.0f * Mathf.Sin(60.0f * Time.time), 0.0f, -180.0f);
        wing2.transform.localRotation = Quaternion.Euler(-90.0f + -20.0f * Mathf.Sin(60.0f * Time.time), 0.0f, -180.0f);

        Quaternion spinRotation = Quaternion.Euler(Time.time * -80.0f, 90.0f, 90.0f);

        transform.position = Vector3.Lerp(titlePosition, mouseDrivenPosition, titleScreenComp.tPlayerDriver);
        transform.rotation = Quaternion.Slerp(spinRotation, mouseDrivenRotation, titleScreenComp.tPlayerDriver);

        if(Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && titleScreenComp.tPlayerDriver > 0.95f){
            GameObject bullet = Object.Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.GetComponent<DamagingComponent>().creator = gameObject;
            particle.GetComponent<ParticleSystem>().Play();
            shotAudioSource.Play();
            Camera.main.GetComponent<CameraShakeComponent>().AddSmallShake();
        }

        if(Input.GetMouseButton(1) && shields > 0.0f && titleScreenComp.tPlayerDriver > 0.95f){
            overshield.GetComponent<Renderer>().enabled = true;
            GetComponent<DamageableComponent>().invincible = true;

            shields -= shieldsDrainRate * Time.deltaTime;
            shields = Mathf.Max(shields, 0.0f);
        } else {
            GetComponent<DamageableComponent>().invincible = false;
            overshield.GetComponent<Renderer>().enabled = false;
        }

        prevhealth = GetComponent<DamageableComponent>().currentHealth;
	}

    public void PlayShieldPickupSound(){
        shieldPickupAudioSource.Play();
    }
}
