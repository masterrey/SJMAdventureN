using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    static string leveltoload;
    public TextMesh loadingprogress;
    static AsyncOperation op;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(leveltoload);
    }

    public static void LoadLevel(string level)
    {
        leveltoload = level;
        op = SceneManager.LoadSceneAsync("Loading");
    }
    // Update is called once per frame
    void Update()
    {
        loadingprogress.text = op.progress.ToString("00%");
    }
}
