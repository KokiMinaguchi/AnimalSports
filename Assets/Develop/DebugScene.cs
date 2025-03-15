using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SleepingAnimals
{
    [DefaultExecutionOrder(-1)]
    public class DebugScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if(!SceneManager.GetSceneByName("ResidentScene").isLoaded)
            {
                SceneManager.LoadSceneAsync("ResidentScene", LoadSceneMode.Additive);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
