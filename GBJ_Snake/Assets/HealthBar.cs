using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public float health = 1f;

	// Use this for initialization
	void Start ()
    {       
        switch (this.gameObject.tag)
        {
            case "Player":
                slider = GameObject.FindGameObjectWithTag("Blue_Slider").GetComponent<Slider>();
                break;
            case "Enemy1":
                slider = GameObject.FindGameObjectWithTag("Red_Slider").GetComponent<Slider>();
                break;
            case "Enemy2":
                slider = GameObject.FindGameObjectWithTag("Green_Slider").GetComponent<Slider>();
                break;
        }
        health = 1f;
        slider.value = health;
    }

    bool invulnerable = false;
    private float invulnerableTime=1f;

    public void TakeDamage(float amount)
    {
        if (!invulnerable)
        {
            health -= amount;
            slider.value = Mathf.Clamp(health, 0f, 1f);
            if (health > 0)
            {
                invulnerable = true;
                Invoke("MakeVulnerable", invulnerableTime);
            }
            else
            {
                TrailRenderer[] trails = this.gameObject.GetComponentsInChildren<TrailRenderer>();
                foreach (TrailRenderer t in trails)
                {
                    t.gameObject.transform.parent = null;
                }

                Destroy(this.gameObject);
            }
        }
    }
    private void MakeVulnerable()
    {
        invulnerable = false;
    }
    
}
