using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private GameObject prefabBullet;

    public void Shoot(Vector3 distanse)
    {
        var rb = Instantiate(prefabBullet, bulletSpawnPosition.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb.AddForce(distanse.normalized * 20f, ForceMode.Impulse);
        
    }
    //public IEnumerator Shoot2(Vector3 distanse)
    //{
    //    var rb = Instantiate(prefabBullet, bulletSpawnPosition.position, Quaternion.identity).GetComponent<Rigidbody>();

    //    rb.AddForce(distanse.normalized * 1f,ForceMode.Impulse);
    //    yield return new WaitForSeconds(1f);
    //}
}
