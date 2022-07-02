using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sparkEFX;

     void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Ball")) {
            Instantiate(sparkEFX, transform.position, Quaternion.identity);
            GamePlayController.instance.PlayCollectableSound();
            gameObject.SetActive(false);
        }
    }
}
