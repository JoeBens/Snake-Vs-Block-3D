using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelParameters lp;

    public GameObject killerCube;
    public GameObject powerup;
    public GameObject obstacle;
    public GameObject finishLine;

    private int numberOfChunks;

    public Renderer planeRenderer;


    // Start is called before the first frame update
    void Start()
    {
        planeRenderer.material.SetColor("_Color", lp.levelColor);


        numberOfChunks = (int)lp.levelLength / lp.chunkSize;


        //first line
        SpawnPowerUp(-10f, lp.foodValue[0], lp.foodValue[1]);
        SpawnKillerCubes(5f, lp.startingKillerCubeHP[0], lp.startingKillerCubeHP[1]);

        float distanceBetweenChunks = 10f;
        for (int i = 0; i < numberOfChunks; i++)
        {
            float z = (i * lp.chunkSize) + distanceBetweenChunks;
            Debug.Log("first z: " + z);
            for (int j = 0; j < 4; j++)
            {
                z += lp.unit;
                GenerateRandomLine(z);
                
            }
            z -= distanceBetweenChunks;
            SpawnKillerCubes(z, lp.killerCubeHP[0], lp.killerCubeHP[1]);
            Debug.Log("Killer Cube Z: " + z);

        }
        Vector3 finishLinePos = new Vector3(0f, 0.6f, lp.levelLength +25f);
        Instantiate(finishLine, finishLinePos, Quaternion.identity);


    }
    public void GenerateRandomLine( float z)
    {
        int nt = Random.Range(1, 4);
        int r = Random.Range(0, 5);
        for (int i = 0; i < nt; i++)
        {
            if(r < 2)
            {
                SpawnPowerUp(z, lp.foodValue[0], lp.foodValue[1]);
            }
            else if(r >= 2 && r < 4)
            {
                SpawnWall(z);
            }else if (r == 4)
            {
                SpawnKillerCube(z, lp.killerCubeHP[0], lp.killerCubeHP[1]);
            }
        }

    }


    public void SpawnKillerCubes(float z, int rangeX, int rangeY)
    {
        int lowHPCubePos = Random.Range(0,5);
        Vector3 pos = new Vector3(lp.killerCubesX[lowHPCubePos], 1.5f, z);
        SpawnKillerCubeV2(pos, 1, 6);

        int moderateHPCubePos = Random.Range(0, 5);

        do
        {
            moderateHPCubePos = Random.Range(0, 5);

        } while (moderateHPCubePos == lowHPCubePos);

        pos = new Vector3(lp.killerCubesX[moderateHPCubePos], 1.5f, z);
        SpawnKillerCubeV2(pos, 6, 15);

        for (int i = 0; i < 5; i++)
        {
            if(i==moderateHPCubePos || i == lowHPCubePos)
            {
                continue;
            }
            pos = new Vector3(lp.killerCubesX[i], 1.5f, z);
            SpawnKillerCubeV2(pos, rangeX, rangeY);

        }
    }
    public void SpawnKillerCube(float z, int rangeX, int rangeY)
    {


        int i = Random.Range(0, 5);
        Vector3 pos = new Vector3(lp.killerCubesX[i], 1.5f, z);
        GameObject kc = Instantiate(killerCube, pos, Quaternion.identity);
        int killerCubeHP = Random.Range(rangeX, rangeY);

        if (killerCubeHP < 10)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[0], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 20)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[1], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 30)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[2], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 40)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[3], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 50)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[4], killerCubeHP, false);
            return;
        }
    }

    public void SpawnKillerCubeV2(Vector3 pos, int rangeX, int rangeY)
    {
        GameObject kc = Instantiate(killerCube, pos, Quaternion.identity);
        int luck = Random.Range(0, 100);
        int killerCubeHP = Random.Range(rangeX, rangeY);



        if (luck <= lp.specialCubeChance)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.specialCubeColor, killerCubeHP, true);
            return;
        }
        if (killerCubeHP < 10)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[0], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 20)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[1], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 30)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[2], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 40)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[3], killerCubeHP, false);
            return;
        }
        else if (killerCubeHP < 50)
        {
            kc.gameObject.GetComponent<KillingCube>().SetParameters(lp.killerCubesColors[4], killerCubeHP, false);
            return;
        }
    }


    public void SpawnPowerUp(float z, int rangeX, int rangeY)
    {
        int i = Random.Range(0, 5);
        Vector3 pos = new Vector3(lp.killerCubesX[i], 0.98f, z);
        GameObject pp = Instantiate(powerup, pos, Quaternion.identity);
        int numberOfSpheres = Random.Range(rangeX, rangeY);
        pp.GetComponent<Food>().SetParameters(lp.foodColor, numberOfSpheres);

    }
    public void SpawnWall(float z)
    {
        int i = Random.Range(0, 4);
        Vector3 pos = new Vector3(lp.powerX[i], 0.98f, z);
        GameObject wall = Instantiate(obstacle, pos, Quaternion.identity);
    }

}
