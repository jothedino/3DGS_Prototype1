using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerShoot : MonoBehaviour
{
    public enum AttackType
    {
        Canon1 = 0,
        Canon2 = 1,

    }

    public AttackType currentWeapon = AttackType.Canon1;

    public enum PlayerNum
    {
        PlayerOne = 0,
        PlayerTwo = 1,
    }

    public PlayerNum playerNumber;

    private int maxWeapons = 2;

    public string fireButton;
 private Camera playerCamera;
    private float canSeeDistance = 10f;
    public LayerMask canSeeLayer;
    public Rigidbody projectile;
    public float projectileVelocity = 25f;
    public float projectileLifespan = .5f;
    public float weaponCooldown = .25f;
    public AudioClip shootClip;
    public int point = 1;
    public float waittime = 5f;

    public Image reticule;

    public GameManager1 gameManager;
    private Transform weapon;
    public bool readyToShoot = true;
    private RaycastHit hit;
    private bool targetActive;
    private AudioSource audioSource;

    public int GetValue()
    {
        return (int)currentWeapon;
    }

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        weapon = transform.Find("Weapon").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
       // Ray ray = playerCamera.ScreenPointToRay(centerPoint);
        Debug.DrawRay(ray.origin, ray.direction * canSeeDistance, Color.red);
        targetActive = Physics.Raycast(ray, out hit, canSeeDistance, canSeeLayer);

        if (targetActive)
        {
           
            reticule.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        }

        else
        {
            reticule.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        
        }


        if (Input.GetButton(fireButton))
        {
            if (readyToShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        
    }

    private IEnumerator Shoot()
    {
        audioSource.PlayOneShot(shootClip);
        readyToShoot = false;
        //Debug.Log("FIRE");

        if (currentWeapon == AttackType.Canon1)
        {
            canSeeDistance = 20f;
            weaponCooldown = 1.2f;

            //See Projectiles
            Rigidbody p = Instantiate(projectile, weapon.position, weapon.rotation) as Rigidbody;
            p.transform.LookAt(playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, canSeeDistance)));
            p.velocity = p.transform.forward * projectileVelocity;
            Destroy(p.gameObject, projectileLifespan);

            // If the raycast hit something
            if (targetActive)
            {
                // Deactivate it
                //hit.collider.gameObject.SetActive(false);
                PlayerHealth ph = hit.collider.gameObject.GetComponent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(10);

                    if (ph.GetHitPoints() <= 0)
                    {
                        readyToShoot = true;
                        if (hit.collider.tag == "Player 1")
                        {
                            gameManager.Kill(GetValue());
                        }
                        else
                        {
                            gameManager.Kill2(GetValue());
                        }



                        yield return new WaitForSeconds(waittime);
                    }
                    
                }
                
                
                
                            }
        }

        if (currentWeapon == AttackType.Canon2)
        {
            canSeeDistance = 20f;
            weaponCooldown = 0.2f;

            //See Projectiles
            Rigidbody p = Instantiate(projectile, weapon.position, weapon.rotation) as Rigidbody;
            p.transform.LookAt(playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, canSeeDistance)));
            p.velocity = p.transform.forward * projectileVelocity;
            Destroy(p.gameObject, projectileLifespan);

            // If the raycast hit something
            if (targetActive)
            {
                // Deactivate it
                //hit.collider.gameObject.SetActive(false);
                PlayerHealth ph = hit.collider.gameObject.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(5);
                    if (ph.GetHitPoints() <= 0)
                    {
                        if (hit.collider.tag == "Player 1")
                        {
                            gameManager.Kill(GetValue());
                        }
                        else
                        {
                            gameManager.Kill2(GetValue());
                        }
                        yield return new WaitForSeconds(waittime);
                    }
                }
            }
        }

        yield return new WaitForSeconds(weaponCooldown);
        readyToShoot = true;
    }
}
