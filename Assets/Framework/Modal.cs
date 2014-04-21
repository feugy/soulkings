using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Modal : MonoBehaviour {

    // Destination object to receive clicks
    public GameObject receiver;

    // Set z-index
    public float z = 0f;
    // Modal content
    public string title;
    public string text;
    // Button labels
    public List<string> buttons = new List<string>();

    GameObject modal;

    void Start()
    {
        transform.position += new Vector3(0, 0, z);
        Debug.Log("opens modal title " + title);
        // Set title and text
        Transform titleRender = transform.Find("Title");
        if (titleRender != null)
            titleRender.GetComponent<TextMesh>().text = title;

        Transform textRender = transform.Find("Text");
        if (textRender != null)
        {
            string current = "";
            int j = 0;
            int max = 25;
            foreach(char c in text) 
            {
                if (j > max || c == '|')
                {
                    current += '\n';
                    j = 0;
                }
                if (c != '|')
                {
                    current += c;
                }
                j++;
            }
            textRender.GetComponent<TextMesh>().text = current;
        }

        int i = 0;
        foreach (Transform render in transform.FindAll("Button"))
        {
            Transform label = render.Find("Label");
            // update button label or hide it
            if (label != null)
            {
                label.GetComponent<TextMesh>().text = buttons[i];
                // update also message sender string
                MessageSender sender = render.GetComponent<MessageSender>();
                if (sender != null)
                {
                    sender.messageName = "OnModalButton";
                    sender.paramString = buttons[i];
                    sender.paramObject = receiver;
                }
            }
            else
                render.gameObject.SetActive(false);
            i++;
        }
    }
}
