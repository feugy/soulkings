using UnityEngine;
using System.Collections;

public class PlayMapMusic : MonoBehaviour
{

    private static PlayMapMusic instance = null;
    public static PlayMapMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Application.loadedLevelName == "Map")
            gameObject.GetComponent<AudioSource>().volume = 1;
        else
            gameObject.GetComponent<AudioSource>().volume = 0;
    }
    // any other methods you need
}
