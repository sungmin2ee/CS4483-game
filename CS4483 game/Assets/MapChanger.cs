using UnityEngine;

public class MapChanger : MonoBehaviour
{

    // create the three maps as game objects
    public GameObject stage_1;
    public GameObject stage_2;
    public GameObject stage_3;

    public void LoadRoundMap()
    {
        switch (GameManager.Instance.round)
        {
            case 1:
                stage_1.SetActive(true);
                break;

            case 2:
                stage_1.SetActive(false);
                stage_2.SetActive(true);
                break;
            case 3:
                stage_2.SetActive(false);
                stage_3.SetActive(true);
                break;
        }

    }


}
