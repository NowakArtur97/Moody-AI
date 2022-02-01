using System;
using static AmmoRestorationManager;

[Serializable]
public struct AmmoRecovery
{
    public float RecoveryTime;
    public float RecoveryValue;
    public AmmoRestorationType RestorationType;

    public AmmoRecovery(float recoveryTime, float recoveryValue, AmmoRestorationType restorationType)
    {
        RecoveryTime = recoveryTime;
        RecoveryValue = recoveryValue;
        RestorationType = restorationType;
    }
}
