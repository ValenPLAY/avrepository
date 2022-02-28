using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // Camera Variables
    private float cameraSpeed = 2.0f;
    private float cameraMovementCounter;
    private Quaternion cameraStartPoint;
    private Quaternion cameraEndPoint;
    private Vector3 rotationVector = new Vector3();
    public Camera mainCamera;
    private bool reachedNext = true;

    // Cell Variables
    public List<GameObject> cellPrefabs = new List<GameObject>();
    private GameObject currentCell;
    private GameObject prevCell;

    // Character Variables
    public GameObject mainCharacter;
    public List<GameObject> characters = new List<GameObject>();
    private int currentCharacterID;
    private GameObject currentCharacter;
    private GameObject previousCharacter;
    private Character charScript;

    // UI variables
    public Text txtCharName;
    public Text txtCharDescription;

    // Char Rotation
    private Ray ray;

    // Color List
    public List<CharacterColor> colorArray = new List<CharacterColor>();
    public class CharacterColor
    {
        public int CharacterID { get; set; }
        public Color Color { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {

        cameraStartPoint = mainCamera.transform.rotation;
        cameraEndPoint = cameraStartPoint;
        CellCreate();
        CharacterSpawn();
        UIUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedNext)
        {
            CameraMove();
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            
            NextCell(Input.GetAxis("Horizontal")>0);
            
        }
        

        CharRotation();

    }

    public void NextCell(bool isRightRotation)
    {
        //Debug.Log(currentCharacterID);

        if (isRightRotation)
        {
            if (currentCharacterID >= characters.Count - 1)
            {
                currentCharacterID = 0;
            }
            else
            {
                currentCharacterID++;
            }
        } else
        {
            if (currentCharacterID <= 0)
            {
                currentCharacterID = characters.Count - 1;
            }
            else
            {
                currentCharacterID--;
            }
        }

        if (reachedNext == true)
        {
            cameraStartPoint = mainCamera.transform.rotation;
            rotationVector = cameraStartPoint.eulerAngles;
            if (isRightRotation)
            {
                rotationVector.y += 90f;
            }
            else
            {
                rotationVector.y -= 90f;
            }
            cameraEndPoint = Quaternion.Euler(rotationVector);
            reachedNext = false;

            CellCreate();
            CharacterSpawn();
            UIUpdate();
        }
    }

    void CameraMove()
    {
        cameraMovementCounter += cameraSpeed * Time.deltaTime;
        mainCamera.transform.rotation = Quaternion.Lerp(cameraStartPoint, cameraEndPoint, cameraMovementCounter);
        if (cameraMovementCounter >= 1)
        {
            reachedNext = true;
            cameraMovementCounter = 0.0f;
            Destroy(prevCell);
            Destroy(previousCharacter);
        }
    }

    void CellCreate()
    {
        prevCell = currentCell;
        var newCellRotation = cameraEndPoint.eulerAngles;
        newCellRotation.x = 0.0f;
        var newCellPosition = mainCamera.transform.position;
        var selectedCell = Random.Range(0, cellPrefabs.Count);
        currentCell = Instantiate(cellPrefabs[selectedCell], newCellPosition, Quaternion.Euler(newCellRotation));
    }

    void CharacterSpawn()
    {
        previousCharacter = currentCharacter;
        var cellClass = currentCell.GetComponent<cell>();
        var characterSpawn = cellClass.heroSpawnPoint;
        currentCharacter = Instantiate(characters[currentCharacterID], characterSpawn.transform.position, characterSpawn.transform.rotation);
        charScript = currentCharacter.GetComponent<Character>();
        //Debug.Log(colorArray[currentCharacterID]);
        //if (colorArray.Length >= currentCharacterID)
        //{
        var setColor = colorArray.FirstOrDefault(x => x.CharacterID == currentCharacterID);
        if (setColor != null)
        {
            ColorChange(setColor.Color);
        }
        //}

    }

    void UIUpdate()
    {
        txtCharName.text = charScript.charName;
        txtCharDescription.text = charScript.charDescription;
    }

    public void ColorChange(Color changedColor)
    {
        if (changedColor != new Color())
        {
            var charRenderer = charScript.characterSuit.GetComponent<Renderer>();
            charRenderer.material.color = changedColor;
            var setColor = colorArray.FirstOrDefault(x => x.CharacterID == currentCharacterID);
            if (setColor == null)
            {
                colorArray.Add(new CharacterColor
                {
                    Color = changedColor,
                    CharacterID = currentCharacterID
                });
            }
            else
            {
                setColor.Color = changedColor;
            }
            //charRenderer.material.SetColor(changedColor);
        }
    }


    private bool selectedToRotate;
    public void CharRotation()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rcHit;
        if (Physics.Raycast(ray, out rcHit))
        {
            //Debug.Log(rcHit.collider.gameObject);
            if (rcHit.collider.gameObject.tag == "Player" && Input.GetButtonDown("Fire1"))
            {
                selectedToRotate = true;

            }
            if (Input.GetButtonUp("Fire1"))
            {
                selectedToRotate = false;
            }
            if (selectedToRotate)
            {
                var rotation = Input.GetAxis("Mouse X");
                currentCharacter.transform.Rotate(Vector3.up, -rotation * Time.deltaTime * 2000);
            }
        }
    }

    public void rotationChangeX(int rotationDegreesX)
    {
        var charScript = currentCharacter.GetComponent<Character>();
        var cameraPivot = charScript.cameraPivot;
        cameraPivot.transform.rotation = charScript.defaultCameraRotation;
        cameraPivot.transform.rotation = Quaternion.Euler(rotationDegreesX, 0.0f, 0.0f);
    }

    public void rotationChangeY(int rotationDegreesY)
    {
        
        var charScript = currentCharacter.GetComponent<Character>();
        var cameraPivot = charScript.cameraPivot;
        cameraPivot.transform.rotation = charScript.defaultCameraRotation;
        cameraPivot.transform.rotation = Quaternion.Euler(0.0f, rotationDegreesY, 0.0f);
    }

    public void loadGame ()
    {
        SceneManager.LoadScene("Polygon");
    }

}
