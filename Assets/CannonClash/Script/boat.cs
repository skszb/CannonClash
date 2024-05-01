using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boat : MonoBehaviour
{
    private float life;
    private int ammo;
    private Image fillImage;
    public GameObject cannonBalls;
    private List<GameObject> cannonBallList;
    public AudioClip playerShootSound;

    // Start is called before the first frame update
    void Start()
    {
        life = 100f;
        ammo = 0;
        fillImage = GameObject.Find("ProgressBarFiller").GetComponent<Image>();
        fillImage.fillAmount = 1f;

        cannonBallList = new List<GameObject>();
        foreach (Transform child in cannonBalls.transform)
        {
            cannonBallList.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setLife(float l) {
        life = l;
    }

    public void damage(float d) {
        life = Mathf.Max(life - d, 0f);
        fillImage.fillAmount = life / 100f;
        if (life <= 0) 
        {
            // game over
            GameObject.Find("Global").GetComponent<Global>().gameOver();
            Die();
        }
        for (int i = 0; i < 6; i++)
        {
            increAmmo();
        }
    }

    private void Die() { 
        Destroy(gameObject);
    }

    public void increAmmo() 
    {
        ammo = Mathf.Min(ammo + 1, 20);
        cannonBallList[ammo - 1].SetActive(true);
    }

    public void decreAmmo() 
    {
        ammo = Mathf.Max(ammo - 1, 0);
        cannonBallList[Mathf.Max(ammo, 0)].SetActive(false);
    }

    public void playerShoot(Vector3 dir) 
    {
        AudioSource.PlayClipAtPoint(playerShootSound, new Vector3(0f, 5f, 5f));
        /// shoot along dir
    }

    public int AmmoCount()
    {
        return ammo;
    }
}
