using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string level) {
        SceneManager.LoadScene(level);
        Time.timeScale = 1; 
    }

    public void quitGame() {
        Application.Quit();
    }
}
