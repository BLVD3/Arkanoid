using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] private int maxGrowUps = 6;

    [SerializeField] private GameObject growUpTemplate;
    [SerializeField] private GameObject penTemplate;
    [SerializeField] private GameObject duplicateTemplate;

    private int _existingGrowUps = 0;
    public static PowerUpHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public GameObject GetPowerUp()
    {
        switch (Random.Range(_existingGrowUps >= maxGrowUps ? 1 : 0, 3))
        {
            case 0:
                _existingGrowUps++;
                GameObject growUp = Instantiate(growUpTemplate);
                growUp.GetComponent<SizeUpPowerUp>().onFellOffMap.AddListener(GrowUpDestroyed);
                return growUp;
            case 1:
                return Instantiate(penTemplate);
            default:
                return Instantiate(duplicateTemplate);
        }
    }

    private void GrowUpDestroyed()
    {
        _existingGrowUps--;
    }
}
