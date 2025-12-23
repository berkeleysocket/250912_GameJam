using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace AJ._01.Scripts
{
    public class TimeToUp : MonoBehaviour
    {
        [SerializeField] private GameObject upperGo;
        [TextArea] [SerializeField] private string texting;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float time;
        private bool isTextingEnd;
        [SerializeField] private Transform movePos;

        [SerializeField] AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        
        private void Start()
        {
            audioSource.clip = clip;
            isTextingEnd = false;
            StartCoroutine(TextCo());
        }

        private IEnumerator TextCo()
        {
            text.text = "";
            for (int i = 0; i < texting.Length; i++)
            {
                audioSource.PlayOneShot(clip);
                text.text += texting[i].ToString();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.3f);
            for (int i = texting.Length - 1; i >= 0; i--)
            {
                text.text = texting.Substring(0, i);
                yield return new WaitForSeconds(0.1f);
            }
            isTextingEnd = true;
        }

        private void Update()
        {
            if (isTextingEnd)
            {
                isTextingEnd = false;
                upperGo.transform.DOMove(movePos.position, time).SetEase(Ease.OutQuad).OnComplete(Application.Quit);
            }
        }
    }
}
