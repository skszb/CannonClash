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
        life -= d;
        life = Mathf.Max(life, 0f);
        fillImage.fillAmount = life / 100f;
        if (life <= 0) 
        {
            // game over
            Die();
        }
    }

    private void Die() { 
        Destroy(gameObject);
    }

    public void increAmmo() 
    {
        ammo++;
        if (ammo < 21) 
        {
            cannonBallList[ammo - 1].SetActive(true);
        }
    }

    public void decreAmmo() 
    {
        ammo--;
        if (ammo < 20) 
        {
            cannonBallList[Mathf.Max(ammo, 0)].SetActive(false);
        }
    }
}
