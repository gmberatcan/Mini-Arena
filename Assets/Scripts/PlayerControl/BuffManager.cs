using System.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    GunSystem gunSystem;
    PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gunSystem = GetComponentInChildren<GunSystem>();
    }
    public void ApplyBuff(BuffData buffData)
    {
        if (playerController != null)
        {
            playerController.Speed += buffData.bonusSpeed;    
        }
        if(gunSystem != null)
        {
            gunSystem.Damage += buffData.bonusDamage;
        }
        StartCoroutine(BuffDuration(buffData));
    }
    IEnumerator BuffDuration(BuffData buffData)
    {
        yield return new WaitForSeconds(buffData.Time);
        if (playerController != null)
        {
            playerController.Speed -= buffData.bonusSpeed;
        }
        if(gunSystem != null)
        {
            gunSystem.Damage -= buffData.bonusDamage;
        }
    }
}
