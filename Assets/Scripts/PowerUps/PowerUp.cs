using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PowerUp : MonoBehaviour
{

    public UnityEvent onFellOffMap;
    [SerializeField] private float dropSpeed = 2f;
    
    protected void Update()
    {
        transform.Translate(dropSpeed * Time.deltaTime * Vector3.back);
        if (transform.position.z < -5)
        {
            onFellOffMap.Invoke();
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 10) return;

        Paddle paddle = other.gameObject.GetComponent<Paddle>();
        
        if (paddle == null) return;

        TriggerEffect(paddle);
        Destroy(gameObject);
    }

    protected abstract void TriggerEffect(Paddle paddle);
}
