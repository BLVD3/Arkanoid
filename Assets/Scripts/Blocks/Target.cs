using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Target : MonoBehaviour, IHitable
{
    private static Color[] _colors =
    {
        new(1f, 0f, 0f), 
        new(1f, 0.3f, 0f), 
        new(1f, 1f, 0f), 
        new(0f, 1f, 0f), 
        new(0f, 1f, 1f), 
        new(0f, 0f, 1f),
        new(0.5f, 0f, 1f),
        new(1f, 0f, 1f),
        new(0.2f, 0.2f, 0.2f)
    };
    
    [SerializeField] private int hp = 1;
    private Renderer _renderer;
    
    void Start()
    {
        GameHandler.Instance.AddTarget();
        _renderer = GetComponent<Renderer>();
        ChangeColor();
    }

    public void Hit()
    {
        hp--;
        GameHandler.Instance.TargetHit();
        if (hp <= 0)
        {
            GameHandler.Instance.TargetDestroyed();
            Destroy(gameObject);
            return;
        }
        ChangeColor();
    }

    private void ChangeColor()
    {
        Color color = _colors[hp - 1];
        _renderer.material.color = color;
        _renderer.material.SetColor("_EmissionColor", color);
    }
}
