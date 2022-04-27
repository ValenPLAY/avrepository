using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Player Information")]
    public Hero selectedHero;
    public Vector3 playerWorldMousePos;
    public Camera mainCamera;

    [Header("Global Variables")]
    public float gravity = -9.8f;
    public float difficulty = 1.0f;

    [Header("Selection Variables")]
    public Unit targetUnit;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;

        if (selectedHero == null)
        {
            selectedHero = FindObjectOfType<Hero>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetUnit = hit.collider.gameObject.GetComponent<Unit>();
            if (targetUnit != null)
            {
                playerWorldMousePos = targetUnit.transform.position;
            } else
            {
                playerWorldMousePos = hit.point;
            }

        }
        //Debug.Log(playerWorldMousePos);
        //playerMousePos = mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        //Debug.Log(playerMousePos);
    }
}
