using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : GameElement
{
    public int num_spheres = 1;

    public override void Start()
    {
        base.Start();


    }

    public override void SetParameters(Color color, int num_sphere)
    {
        num_spheres = num_sphere;

        var renderer = this.gameObject.GetComponentInChildren<Renderer>();

        renderer.material.SetColor("_Color", color);

        tm.text = num_sphere.ToString();

    }


}
