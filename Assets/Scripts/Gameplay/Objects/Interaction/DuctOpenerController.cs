using System;
using System.Collections;
using System.Collections.Generic;
using GameManagers;
using UnityEngine;
using Utilities;
using Utilities.Audio;

public class DuctOpenerController : TriggerColliderController
{

    [SerializeField] private GameObject _ductCover;
    [SerializeField] private GameObject _ductCoverFallen;
    [SerializeField] private Collider _newExitPoint;

    private bool _activated = false;

    private void Awake()
    {
        onTriggerEnter = HandlePlayerEnterArea;
    }

    private void HandlePlayerEnterArea(Collider other)
    {
        if (!other.CompareTag(GameInternalTags.PLAYER)) return;
        if (!_activated)
        {
            AudioManager.instance.PlayAtPosition(AudioNameEnum.ENVIRONMENT_DUCT_COVER_FALL, _ductCover.gameObject.transform.position, false, AudioRange.LOW);
            _ductCoverFallen.SetActive(true);
            _newExitPoint.enabled = true;
            _ductCover.SetActive(false);
            _activated = true;
        }
    }
}
