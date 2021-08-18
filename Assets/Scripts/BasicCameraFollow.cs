using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BasicCameraFollow : MonoBehaviour 
{
    private GameObject player;
    public Transform followTarget;
    //public PlayerCombatHandler playerCombatHandler;

   // private Vector3 startingPosition;
	public float moveSpeed;

    public Vector3 gameWorldMinCameraPosition = new Vector3(-100, -100, -100);
    public Vector3 gameWorldMaxCameraPosition = new Vector3(100, 100, 100);
	
	void Start()
	{
        //SceneManager.activeSceneChanged += OnSceneChange;

        //SetupCamera();

        Camera mainCamera = Camera.main;
        mainCamera.transform.position = followTarget.position;
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        if (!next.name.Contains("Assets"))
        {
            SetupCamera();   
        }
        
    }

    void SetupCamera()
    {
        //player = GameObject.FindGameObjectWithTag("Player");

        //if (player != null)
        //{
        //    followTarget = player.transform;
        //    playerCombatHandler = player.GetComponent<PlayerCombatHandler>();
        //    //startingPosition = transform.position;
        //    Camera mainCamera = Camera.main;
        //    mainCamera.transparencySortMode = TransparencySortMode.CustomAxis;
        //    mainCamera.transparencySortAxis = new Vector3(0, 1, 0);
        //    mainCamera.transform.position = followTarget.position;

        //    //var sceneName = SceneManager.GetActiveScene().name;
        //    var sceneMinClamp = GameObject.Find("CameraMinClamp");
        //    var sceneMaxClamp = GameObject.Find("CameraMaxClamp");

        //    //gameWorldMinCameraPosition = new Vector3(-40, -71, 0);
        //    //gameWorldMaxCameraPosition = new Vector3(8, 49, 0);
        //    gameWorldMinCameraPosition = sceneMinClamp.transform.position;
        //    gameWorldMaxCameraPosition = sceneMaxClamp.transform.position;
        //}
        
    }

	void Update () 
	{
		if(followTarget != null)
		{
            Vector3 targetPos = new Vector3();
            Vector3 velocity = new Vector3();

            //Debug.Log("In camera following1: " + followTarget.position);


            targetPos = new Vector3(
                Mathf.Clamp(followTarget.position.x, gameWorldMinCameraPosition.x, gameWorldMaxCameraPosition.x),
                Mathf.Clamp(followTarget.position.y, gameWorldMinCameraPosition.y, gameWorldMaxCameraPosition.y),
                -5);

            //Debug.Log("In camera following2: " + targetPos);

            velocity = (targetPos - transform.position) * moveSpeed;

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, Time.deltaTime);
        }
    }
}

