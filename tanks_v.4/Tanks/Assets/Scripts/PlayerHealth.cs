using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Image currentHealthbar;
    public Text healthText;
    public Image showHB;
    public Text showHT;

    private float hitpoint = 100;
    private float maxHitpoint = 100;

    private float gameOverDelay = 5f;
  

    public GameManager1 gameManager;
    public Transform startLocation;
    public Transform waitLocation;
    private PlayerShoot ps;
    public bool death;
    public GameObject player;


    // Use this for initialization
    void Start () {
        UpdateHealth();
        death = false;
        ps = GetComponent<PlayerShoot>();

    }

    public float GetHitPoints()
    {
        return hitpoint;

    }

    void UpdateHealth()
    {
        float ratio = hitpoint / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        healthText.text = (ratio * 100).ToString("0") + '%';
        showHB.rectTransform.localScale = new Vector3(ratio, 1, 1);
        showHT.text = (ratio * 100).ToString("0") + '%';
    }

   public void TakeDamage(float damage)
    {

        hitpoint -= damage;
        if (hitpoint <= 0)
        {
            hitpoint = 0;
            Death();

        }
        UpdateHealth();
    }

    public void Death()
    {
        

        Debug.Log("Dead!");
        
        death = true;

        transform.position = waitLocation.position;
        StartCoroutine(GameOver());
        
        
        
    }

    private IEnumerator GameOver()
    {
        ps.readyToShoot = false;
        yield return new WaitForSeconds(gameOverDelay);     // Delay before resetting
        //gameObject.SetActive(true);
        ps.readyToShoot = true;
        transform.position = startLocation.position;


        hitpoint = 100;
        float ratio = hitpoint / maxHitpoint;
        //currentHealthbar.fillAmount = ratio;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        healthText.text = (ratio * 100).ToString("0") + '%';
        showHB.rectTransform.localScale = new Vector3(ratio, 1, 1);
        showHT.text = (ratio * 100).ToString("0") + '%';
    }
   
}
