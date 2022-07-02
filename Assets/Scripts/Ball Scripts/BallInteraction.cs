using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteraction : MonoBehaviour
{

    private int diamondAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndTile"))
        {
            Debug.Log("Finished");
            FinishedAction();
        }
        if (other.CompareTag("Gem")) {
            GamePlayController.instance.addDiamond(diamondAmount);
        }

    }
    void FinishedAction()
    {
        GamePlayController.instance.LevelCompleted();


    }
}
