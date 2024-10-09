using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUpPowerUp : PowerUp
{
    protected override void TriggerEffect(Paddle paddle)
    {
        paddle.AddWidth();
    }
}
