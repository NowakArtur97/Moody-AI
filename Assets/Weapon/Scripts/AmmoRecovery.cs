using System;
using static AmmoRestorationManager;

[Serializable]
public struct AmmoRecovery
{
    public float RecoveryValue;
    public AmmoRestorationType RestorationType;

    public AmmoRecovery(float recoveryValue, AmmoRestorationType restorationType)
    {
        RecoveryValue = recoveryValue;
        RestorationType = restorationType;
    }
}
