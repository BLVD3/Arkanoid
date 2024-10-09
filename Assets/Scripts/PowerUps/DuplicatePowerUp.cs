using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatePowerUp : PowerUp
{
    protected override void TriggerEffect(Paddle paddle)
    {
        BallHandler.Instance.DuplicateBalls();
    }
}
