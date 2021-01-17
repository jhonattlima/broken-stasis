using System.Collections;
using System.Collections.Generic;
using GameManagers;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GameplayManager.instance.ChangePlayerSuit(PlayerSuitEnum.SUIT1);
    }
}
