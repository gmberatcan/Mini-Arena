using UnityEngine;

public class BuffPickUp : MonoBehaviour
{
    [SerializeField] private BuffData buffData;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BuffManager playerBuff = other.GetComponent<BuffManager>();
            if (playerBuff != null)
            {
                playerBuff.ApplyBuff(buffData);
                Destroy(gameObject);
            }
        }
    }
}
