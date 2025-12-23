using _Scripts.Core.Events;
using _Scripts.Core.Structs;
using _Scripts.Core.Utility;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.YTH.GameOver
{    
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private EmptyEventChannel gameOverEventChannel;
        [SerializeField] private CanvasGroup panel;
        [SerializeField] private string sceneName;


        private void Awake()
        {
            panel.gameObject.SetActive(false);
            gameOverEventChannel.OnEvent += ToggleGameOver;
        }

        private void OnDestroy() 
        {
            gameOverEventChannel.OnEvent -= ToggleGameOver;
        }

        public void OnButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneName);
        }

        public void ToggleGameOver(Empty empty)
        {
            Logging.Log("Die");
            if (panel != null)
            {    
                Sequence sequence = DOTween.Sequence().SetUpdate(true);
                panel.DOKill();
                sequence.AppendCallback(() => panel.gameObject.SetActive(true)).SetUpdate(true);
                sequence.Append(panel.DOFade(1, 0.25f).SetEase(Ease.OutCubic)).SetUpdate(true);
                sequence.AppendCallback(() => Time.timeScale = 0);
            }

        }

    }
}
