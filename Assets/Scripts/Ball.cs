using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private AudioClip beep;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private Material penActiveMaterial;

    private Rigidbody _rb;
    private bool _hasPenetration;
    public bool HasPenetration => _hasPenetration;
    private bool _hasHitWithPen;

    private Material _originalMaterial;
    private Renderer _renderer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.MovePosition(transform.position + velocity * Time.deltaTime);
        if (transform.position.z < 0)
        {
            BallHandler.Instance.BallDestroyed(this);
            AudioSource.PlayClipAtPoint(explosion, transform.position);
            Destroy(gameObject);
        }
    }

    public void ActivatePenetration()
    {
        _renderer.material = penActiveMaterial;
        _hasPenetration = true;
        _hasHitWithPen = false;
    }

    private void DisablePenetration()
    {
        _renderer.material = _originalMaterial;
        _hasPenetration = false;
    }

    private void ReflectOnHit(Vector3 normal)
    {
        if(Vector3.Angle(velocity, normal) < 90) return;
        velocity = Vector3.Reflect(velocity, normal);
    }

    public void ReverseVelocity()
    {
        velocity *= -1;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 normal = other.contacts[0].normal;
        
        switch (other.gameObject.layer)
        {
            case 9: // Target
                if (other.gameObject.GetComponent<IHitable>() is { } hitable)
                {
                    hitable.Hit();
                }
                _hasHitWithPen = true;
                if (!_hasPenetration)
                    ReflectOnHit(normal);
                AudioSource.PlayClipAtPoint(beep, transform.position);
                break;
            case 10: // Paddle
                Vector3 paddlePos = other.transform.position;
                Vector3 ballPos = transform.position;
                Vector3 dir = ballPos - paddlePos; // Direction away from ball
                dir.y = 0; // Cutting y to not accidentally introduce a 3rd dimension
                velocity = dir.normalized * velocity.magnitude;
                AudioSource.PlayClipAtPoint(beep, ballPos);
                if (_hasPenetration && _hasHitWithPen)
                    DisablePenetration();
                break;
            default: // Anything else
                ReflectOnHit(normal);
                AudioSource.PlayClipAtPoint(beep, transform.position);
                break;
        }
    }
}
