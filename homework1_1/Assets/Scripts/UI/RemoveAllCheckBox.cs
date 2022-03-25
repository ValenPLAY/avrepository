using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAllCheckBox : MonoBehaviour
{
    private Toggle removeAllToggle;
    // Start is called before the first frame update
    void Start()
    {
        removeAllToggle = GetComponent<Toggle>();
        //removeAllToggle.onValueChanged.RemoveListener
        removeAllToggle.onValueChanged.AddListener(onCheckBoxChange);

        //removeAllToggle.onValueChanged.RemoveListener(onCheckBoxChange);
    }

    void onCheckBoxChange (bool value)
    {
        ObjectSpawn.removeOnSpawn = value;
        //checkboxRemoveAll = GetComponent<RemoveAllCheckBox>;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
