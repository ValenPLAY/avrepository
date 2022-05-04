using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public TextMeshPro unitNameTMP;
    public SpriteRenderer unitHealthSprite;
    private Unit infoUnit;
    [SerializeField] Vector3 infoOffset;
    Vector3 defaultRotation;


    private void Awake()
    {
        UpdateInfo();
        transform.position += infoOffset;
        //if (Camera.main != null) transform.LookAt(Camera.main.transform);
    }

    private void Update()
    {
        //if (Camera.main != null) transform.LookAt(Camera.main.transform);
    }

    public void UpdateInfo()
    {
        if (infoUnit == null)
        {
            infoUnit = transform.parent.GetComponent<Unit>();
        }

        if (infoUnit != null)
        {
            unitNameTMP.text = infoUnit.name;

            if (infoOffset == Vector3.zero)
            {
                infoOffset.y = infoUnit.transform.localScale.y;
            }
        }

    }
}
