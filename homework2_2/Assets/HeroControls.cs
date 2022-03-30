using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroControls : MonoBehaviour
{
    public Camera mainCamera;
    private NavMeshAgent unit;

    private AudioSource unitTalk;
    public List<AudioClip> quotes = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        unitTalk = GetComponent<AudioSource>();
        unit = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                unit.SetDestination(hit.point);

                SayPhrase(quotes[Random.Range(0, quotes.Count)]);
            }
        }
    }

    void SayPhrase(AudioClip newPhrase)
    {
        if (unitTalk.isPlaying == false)
        {
            unitTalk.clip = newPhrase;
            unitTalk.Play();
        }
    }
}
