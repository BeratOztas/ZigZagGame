using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Rigidbody myBody;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject gem;

    [SerializeField]
    private float chanceForCollectable;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
     void Start()
    {
        if (Random.value < chanceForCollectable) {
            Vector3 temp = transform.position;
            temp.y += 2f;
          //  Instantiate(gem, temp, Quaternion.identity);
        }
        
    }

    IEnumerator TriggerFallDown() {
        yield return new WaitForSeconds(0.3f);
        myBody.isKinematic = false;
        audioSource.Play();
        StartCoroutine(TurnOffGameObject());
    }
    IEnumerator TurnOffGameObject() {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball")) {
            StartCoroutine(TriggerFallDown());
        }
        
    }
}//class




