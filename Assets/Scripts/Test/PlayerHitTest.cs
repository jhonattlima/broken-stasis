using GameManagers;
using UnityEngine;
using Utilities;

public class PlayerHitTest : TriggerColliderController
{
    private bool _isIncollider = false;

    private void Awake()
    {
        onTriggerEnter = HandlePlayerOnTriggerEnter;
        onTriggerExit = HandlePlayerOnTriggerExit;
    }

    private void HandlePlayerOnTriggerExit(Collider obj)
    {
        if (!obj.CompareTag(GameInternalTags.PLAYER)) return;
        _isIncollider = false;
    }

    private void HandlePlayerOnTriggerEnter(Collider obj)
    {
        if (!obj.CompareTag(GameInternalTags.PLAYER)) return;
        _isIncollider = true;
        GameplayManager.instance.onPlayerDamaged?.Invoke(1);
    }
}
