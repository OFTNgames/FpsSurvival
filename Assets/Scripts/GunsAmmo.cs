using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GunsAmmo : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float damage2 = 30f;
    public float range2 = 35f;
    public float fireRate1 = 2f;
    public float fireRate2 = .5f;
    public float hitforce = 100f;
    public int pellets;
    public float bloomFire1;
    public float bloomShotgun;
    public bool recovery;

    public LayerMask canBeShot;
    public LayerMask canHaveBulletHole;
    public Camera fpsCam;
    public int ammo;
    public int ammoMax;
    public bool isFiring;
    public Text ammoDisplay;
    public ParticleSystem muzzleFlash;
    public ParticleSystem shotgunFlash;
    public GameObject impactEffect;
    public GameObject shotgunBulletHole;
    
    public float fovMax = 88f;
    public float fovMin = 35f;
    private bool zoomingIn = false;
    public float zoomSpeed = 1f;
    
    private float nextTimeToFire1 = 0f;
    private float nextTimeToFire2 = 0f;
    private AudioSource gunSound;

    private void Awake() => gunSound = GetComponent<AudioSource>();

    void Update()
    {
        if (GameManager.instance.gamePlaying && PauseMenu.GameIsPaused == false)        
        {
            FireInput();
            AimDownSights();
        }
    }

    private void FireInput() 
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire1)
        {
            nextTimeToFire1 = Time.time + 1f / fireRate1;
            gunSound.Play();
            Shoot1();
        }

        if (Input.GetMouseButtonDown(2) && Time.time >= nextTimeToFire2 && ammo > 0)
        {
            nextTimeToFire2 = Time.time + 1f / fireRate2;
            Shoot2();
        }
        ammoDisplay.text = ammo.ToString();
    }

    private void AimDownSights()
    {
        if (Input.GetButton("Fire2"))
        {
            if (zoomingIn == false)
            {
                fireRate1 = 10f;
                zoomingIn = true;
            }
        }
        else
        {
            fireRate1 = 5f;
            zoomingIn = false;
        }
        if (zoomingIn)
        {
            fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, fovMin, zoomSpeed * Time.deltaTime);
        }
        else if (!zoomingIn)
        {
            fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, fovMax, zoomSpeed * Time.deltaTime);
        }
    }

    void Shoot1 ()
    {
        muzzleFlash.Play();
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        Vector3 tBloomN = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        tBloomN += Random.Range(-bloomFire1, bloomFire1) * fpsCam.transform.up;
        tBloomN += Random.Range(-bloomFire1, bloomFire1) * fpsCam.transform.right;
        tBloomN -= fpsCam.transform.position;
        tBloomN.Normalize();

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, tBloomN, out hit, range, canBeShot))
        {
           var target = hit.transform.GetComponent<ICanTakeDamage>();
           if(target != null) 
           {
                target.TakeDamage(damage);
           }

           if (hit.rigidbody != null) 
           {
               hit.rigidbody.AddForce(-hit.normal * hitforce);
           }
            
           GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
           impactGo.GetComponent<ParticleSystem>().Play();
           Destroy(impactGo, 2f);
           
           if (hit.collider.tag == "Ground")
           {
                GameObject bulletHole = Instantiate(shotgunBulletHole, hit.point, Quaternion.LookRotation(hit.normal * 0.001f));
                Destroy(bulletHole, 4f);
           }
        }
    }

    void Shoot2() 
    {
        ammo--;
        AudioManager.instance.Play("Shotgun");
        shotgunFlash.Play();
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        //Loop fire script up to pellet count
        for (int i = 0; i < Mathf.Max(1, pellets); i++)
        {        
            //Randomize accuracy
            Vector3 tBloom = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
            tBloom += Random.Range(-bloomShotgun, bloomShotgun) * fpsCam.transform.up;
            tBloom += Random.Range(-bloomShotgun, bloomShotgun) * fpsCam.transform.right;
            tBloom -= fpsCam.transform.position;
            tBloom.Normalize();

            RaycastHit[] hits;
            hits = Physics.RaycastAll(fpsCam.transform.position, tBloom, range, canBeShot);

            for (int x = 0; x < hits.Length; x++)
            {
                RaycastHit hit = hits[x];

                var target = hit.transform.GetComponent<ICanTakeDamage>();
                if (target != null)
                {
                    target.TakeDamage(damage2);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitforce);
                }

                //create bullet holes
                if (hit.collider.tag == "Ground")
                {
                    GameObject bulletHole = Instantiate(shotgunBulletHole, hit.point, Quaternion.LookRotation(hit.normal * 0.001f));
                    Destroy(bulletHole, 4f);
                }
            }

            /*
            //Rays to detect hit
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, tBloom, out hit, range, canBeShot))
            {
                var target = hit.transform.GetComponent<ICanTakeDamage>();
                if (target != null)
                {
                    target.TakeDamage(damage2);
                }
                
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitforce);
                }

                //create bullet holes
                if (hit.collider.tag == "Ground")
                {
                    GameObject bulletHole = Instantiate(shotgunBulletHole, hit.point, Quaternion.LookRotation(hit.normal * 0.001f));
                    Destroy(bulletHole, 4f);
                }
            }
            */
        }
    }
}

