using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject levelCompleteLabelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            ShowLevelCompleteLabel();
        }
    }

    void ShowLevelCompleteLabel()
    {   //у main камеры z == 10, чтобы сделать его 0, создаем векор3
        Instantiate(levelCompleteLabelPrefab, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Camera.main.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
