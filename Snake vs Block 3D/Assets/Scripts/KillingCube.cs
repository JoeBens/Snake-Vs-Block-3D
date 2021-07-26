using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingCube : GameElement
{
    // Start is called before the first frame update

    public int num_spheres_to_kill = 1;


    public bool isSpecial = false;

    private CollisionHandler _ch;
    private PlayerController playerController;

    public override void Start()
    {
        
        _ch = GameObject.FindObjectOfType<CollisionHandler>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void DecrementNumber()
    {
        
        num_spheres_to_kill--;
        tm.text = num_spheres_to_kill.ToString();
        if (num_spheres_to_kill <= 0)
        {
            if(isSpecial == true)
            {
                _ch.ActivateSuperPowerMode();
            }

            playerController.isCollidedWithCube = false;

            Destroy(gameObject);
        }

    }
    public void SetParameters(Color color, int num_sphere, bool special)
    {
        isSpecial = special;
        num_spheres_to_kill = num_sphere;

        var renderer = this.gameObject.GetComponentInChildren<Renderer>();

        renderer.material.SetColor("_Color", color);

        tm.text = num_sphere.ToString();
        if (isSpecial == true)
        {
            tm.color = Color.yellow;
        }

    }

    



}
