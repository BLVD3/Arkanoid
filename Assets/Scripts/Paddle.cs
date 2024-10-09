using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float maxCenterDeviation = 5f;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float width = 2f;

    private BoxCollider _collider;
    private InputActions _inputActions;

    private Transform _body;
    private Transform _left;
    private Transform _right;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _body = transform.GetChild(2);
        _left = transform.GetChild(0);
        _right = transform.GetChild(1);
        
        _inputActions = new InputActions();
        _inputActions.InGame.Enable();
        UpdateWidth();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = _inputActions.InGame.Move.ReadValue<float>();
        Vector3 pos = transform.position;
        var size = _collider.size;
        pos.x = Mathf.Clamp(pos.x + inputX * speed * Time.deltaTime, -maxCenterDeviation + size.x / 2, maxCenterDeviation - size.x / 2);
        transform.position = pos;
    }

    public void AddWidth()
    {
        width += 0.5f;
        UpdateWidth();
    }

    private void UpdateWidth()
    {
        StopAllCoroutines();
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float startWidth = _collider.size.x;
        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            float elapsed = Time.time - startTime;
            float currentWidth = Mathf.Lerp(startWidth, width, elapsed);
            
            SetWidth(currentWidth);

            yield return null;
        }
        SetWidth(width);
    }

    private void SetWidth(float currentWidth)
    {
        Vector3 colliderSize = _collider.size;
        colliderSize.x = currentWidth;
        _collider.size = colliderSize;

        float halfOffsetWidth = currentWidth / 2f - 0.25f;
            
        Vector3 bodyScale = _body.localScale;
        bodyScale.y = halfOffsetWidth;
        _body.localScale = bodyScale;

        Vector3 leftPos = _left.localPosition;
        leftPos.x = -halfOffsetWidth;
        _left.localPosition = leftPos;
            
        Vector3 rightPos = _right.localPosition;
        rightPos.x = halfOffsetWidth;
        _right.localPosition = rightPos;
    }
}
