using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        if (whatHitMe.gameObject.tag == "Player")
        {
            StartCoroutine(ExitNextLevel());
        }
    }

    private IEnumerator ExitNextLevel()
    {
        // start the particle system
        GetComponentInChildren<ParticleSystem>().Play();
        // also slow down the system using the TimeScale
        // kick off the exit to new level method as well
        yield return new WaitForSeconds(2.0f);
       // var index = SceneManager.GetActiveScene().buildIndex;
      //  SceneManager.LoadSceneAsync(index + 1);

    }
}
