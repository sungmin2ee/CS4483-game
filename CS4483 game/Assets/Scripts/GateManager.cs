using UnityEngine;

public class GateManager : MonoBehaviour
{
    public GameObject closedDoorObject;
    public GameObject openedDoorObject;
    public PlayerController player;

    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && gameObject.tag == "Gate")
        {
            Debug.Log("Clear");
            closedDoorObject.SetActive(false);
            openedDoorObject.SetActive(true);
        }
    }
}
