using UnityEngine;

public class NPCText : MonoBehaviour
{
    /**
     * currently we don't have NPC's, so this script just hides the text box
     */

    private UnityEngine.UI.Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();

        HideImage();
    }

    public void HideImage()
    {
        gameObject.SetActive(false);
    }
}
