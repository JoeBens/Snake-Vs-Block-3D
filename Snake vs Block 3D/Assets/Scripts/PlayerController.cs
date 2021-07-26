using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spheres = new List<Transform>();

    public float minDistance = 0.25f;

    public float speed;
    public float steerForce;
    public float rotationSpeed = 50;
    public GameObject spherePrefab;

    private float dis;
    private Transform leadingSphere;
    private Transform previousSphere;


    public CinemachineVirtualCamera vcam;

    public SphereColors sc;
    public PlayerStats ps;


    public TextMesh tm;


    public Transform powerup_Checker;



    public bool isStopped = false;

    public float barrierX = 10f;

    public bool isCollidedWithCube = false;

    Vector3 noObstacle = new Vector3(0f, 0f, 0f);

    [SerializeField]
    private Vector3 obstacleKick;


    public GameManager gm;


    private Vector3 textPos = new Vector3(0f, 0f, 1.5f);

    // Start is called before the first frame update
    void Start()
    {
        speed = ps.speed;
        gm = FindObjectOfType<GameManager>();

        for (int i = 0; i < ps.playerSize - 1; i++)
        {

            AddSphere();

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isStopped == false)
        {
            Move(noObstacle);
        }
        else
        {
            obstacleKick = -obstacleKick;
            Move(obstacleKick);
            isStopped = false;
        }
        
        

        if (Input.GetKey(KeyCode.Q))
            AddSphere();

        if (Input.GetKeyDown(KeyCode.Space))
            RemoveSphere();


        powerup_Checker.position = spheres[0].position;

        Barriers();
    }


    public void Barriers()
    {
        if(spheres[0].position.x >= barrierX)
        {
            spheres[0].position = new Vector3(barrierX, spheres[0].position.y, spheres[0].position.z);
        }
        else if(spheres[0].position.x <= -barrierX)
        {
            spheres[0].position = new Vector3(-barrierX, spheres[0].position.y, spheres[0].position.z);
        }
    }

    public void UpdateText()
    {
        tm.text = spheres.Count.ToString();
        
        tm.gameObject.transform.SetParent(spheres[0]);
        tm.gameObject.transform.localPosition = textPos;
    }


    public void Move(Vector3 obstacleKick)
    {

        float curspeed = speed;

        if (Input.GetKey(KeyCode.W))
            curspeed *= 2;

        if(isCollidedWithCube == false)
            spheres[0].Translate(spheres[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

        spheres[0].position = Vector3.Lerp(spheres[0].position, spheres[0].position + obstacleKick, Time.deltaTime * rotationSpeed);




        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 newpose = new Vector3(steerForce * Input.GetAxis("Horizontal"), 0f, 0f) + spheres[0].position;

            spheres[0].position = Vector3.Lerp(spheres[0].position, newpose, Time.deltaTime * rotationSpeed);

        }
            

        for (int i = 1; i < spheres.Count; i++)
        {

            leadingSphere = spheres[i];
            previousSphere = spheres[i - 1];

            dis = Vector3.Distance(previousSphere.position, leadingSphere.position);

            Vector3 newpos = previousSphere.position;

            newpos.y = spheres[0].position.y;

            float T = Time.deltaTime * dis / minDistance * curspeed;

            if (T > 0.5f)
                T = 0.5f;
            leadingSphere.position = Vector3.Slerp(leadingSphere.position, newpos, T);
            leadingSphere.rotation = Quaternion.Slerp(leadingSphere.rotation, previousSphere.rotation, T);

        }
    }


    public void AddSphere()
    {

        Transform newpart = (Instantiate(spherePrefab, spheres[spheres.Count - 1].position, spheres[spheres.Count - 1].rotation) as GameObject).transform;

        var renderer = newpart.gameObject.GetComponent<Renderer>();

        Color sphereColor = sc.colors[Random.Range(0, sc.colors.Count)];

        renderer.material.SetColor("_Color", sphereColor);

        newpart.SetParent(transform);

        spheres.Add(newpart);
        UpdateText();

    }

    public void RemoveSphere()
    {
        GameObject sphereToDestroy = spheres[0].gameObject;
        spheres.RemoveAt(0);
        if (spheres.Count <= 0)
        {
            gm.GameOver();
            this.enabled = false;
        }
        Destroy(sphereToDestroy);
        vcam.Follow = spheres[0];
        UpdateText();
    }

}


