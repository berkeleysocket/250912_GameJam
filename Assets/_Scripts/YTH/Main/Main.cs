using _Scripts.Core.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.YTH.Main
{    
    public class Main : MonoBehaviour
    {
        [SerializeField] private InputSO inputSO;
        [SerializeField] private GameObject translation;
        [SerializeField] private string sceneName;

        public void StartButton()
        {
            translation.SetActive(true);
        }

        public void StartScene()
        {
            inputSO.Controls.Enable();
            SceneManager.LoadScene(sceneName);
        }
    }
}
