using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Player Information")]
    public Hero selectedHero;
    public Vector3 playerWorldMousePos;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            playerWorldMousePos = hit.point;

        }
        Debug.Log(playerWorldMousePos);
        //playerMousePos = mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        //Debug.Log(playerMousePos);
    }
}
