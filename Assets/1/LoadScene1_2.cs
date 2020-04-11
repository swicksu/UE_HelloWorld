using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace 命名空间
{

}

public class LoadScene1_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [RuntimeInitializeOnLoadMethod]
    private static void Load()
    {
        SceneManager.LoadScene("1_2");
    }
}
