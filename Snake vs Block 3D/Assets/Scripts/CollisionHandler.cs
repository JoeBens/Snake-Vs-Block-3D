using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private PlayerController playerController;



    public bool superPower = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        playerController = GameObject.FindObjectOfType<PlayerController>();

    }


    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {

        

        if (other.gameObject.CompareTag("powerup"))
        {

            Debug.Log("DingDooooooooong");
            int num_spheres = other.gameObject.GetComponent<Food>().num_spheres;

            for (int i = 0; i < num_spheres; i++)
            {
                playerController.AddSphere();
            }
            
            
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("FinishLine"))
        {
            Debug.Log("LEVELLLLLLLLLLLLLLLLL");  
            gm.CompleteLevel();
        }

        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("KillerCube"))
        {
            //playerController.isCollidedWithCube = true;
            if(superPower == true)
            {
                Destroy(collision.gameObject);
                gm.AddScore();
                playerController.isCollidedWithCube = false;
            }
            else if(collision.gameObject.GetComponent<KillingCube>() != null)
            {
                KillingCube kc = collision.gameObject.GetComponent<KillingCube>();
                int num_spheres = kc.num_spheres_to_kill;

                playerController.RemoveSphere();
                kc.DecrementNumber();
                gm.AddScore();
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {

                playerController.isStopped = true;
        }
    }


    public void ActivateSuperPowerMode()
    {
        StartCoroutine("SuperPowerMode");
    }

    IEnumerator SuperPowerMode()
    {
        superPower = true;
        playerController.speed *= 2f;
        Debug.Log("hehehehehehheeeeeeeeeeee");
        yield return new WaitForSeconds(5f);
        Debug.Log("hehehehehehheeeeeeeeeeee");
        playerController.speed /= 2f;
        superPower = false;

    }
    
}
