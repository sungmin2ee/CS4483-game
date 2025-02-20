using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCController : MonoBehaviour
{
    public PlayerController player;
    public GameObject chatPanel;
    public TextMeshProUGUI chatText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (chatPanel != null)
        {
            chatPanel.SetActive(false);

            // Check if there's already a Button component
            Button existingButton = chatPanel.GetComponent<Button>();
            if (existingButton == null)
            {
                existingButton = chatPanel.AddComponent<Button>();
            }
            existingButton.onClick.AddListener(CloseChatPanel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && gameObject.tag == "NPC")
        {
            if (chatPanel != null)
            {
                chatPanel.SetActive(true);
                chatText.text = "Hello!";
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (chatPanel != null)
        {
            chatPanel.SetActive(false);
        }
    }

    public void CloseChatPanel()
    {
        if (chatPanel != null)
        {
            chatPanel.SetActive(false);
        }
    }
}
