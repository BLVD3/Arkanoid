using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PowerUpTarget : MonoBehaviour, IHitable
{
    private Renderer _renderer;
    
    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    private void Start()
    {
        GameHandler.Instance.AddTarget();
        _renderer = GetComponent<Renderer>();
    }

    public void Hit()
    {
        GameObject powerUp = PowerUpHandler.Instance.GetPowerUp();
        powerUp.transform.position = transform.position;
        GameHandler.Instance.TargetHit();
        GameHandler.Instance.TargetDestroyed();
        Destroy(gameObject);
    }

    private void ChangeColor()
    {
        Color color = new Color(
            Mathf.Sin(Time.time),
            Mathf.Sin(Time.time + (Mathf.PI * 2 / 3)),
            Mathf.Sin(Time.time + (Mathf.PI * 4 / 3))
        );
        _renderer.material.color = color;
        _renderer.material.SetColor("_EmissionColor", color);
    }
}