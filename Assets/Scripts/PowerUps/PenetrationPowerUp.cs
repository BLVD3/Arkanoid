using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationPowerUp : PowerUp
{
    protected override void TriggerEffect(Paddle paddle)
    {
        BallHandler.Instance.TriggerPenetration();
    }
}
