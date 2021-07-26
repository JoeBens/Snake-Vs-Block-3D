using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SphereColors", menuName = "Colors")]
public class SphereColors : ScriptableObject
{ 
    // Start is called before the first frame update
    public List<Color> colors;
}
