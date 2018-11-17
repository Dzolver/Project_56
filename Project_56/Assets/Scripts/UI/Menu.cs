using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Menu : MonoBehaviour
    {
        public bool IsModal = false;
        public CanvasGroup m_CanvasGroup;

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
            Initialize();
        }

        private void Initialize()
        {
            HideMenu();
        }

        public void ShowMenu()
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            m_CanvasGroup.alpha = 1;
            m_CanvasGroup.interactable = m_CanvasGroup.blocksRaycasts = true;
        }

        public void HideMenu()
        {
            m_CanvasGroup.alpha = 0;
            m_CanvasGroup.interactable = m_CanvasGroup.blocksRaycasts = false;
        }

        public void BlockMenu()
        {
            m_CanvasGroup.interactable = m_CanvasGroup.blocksRaycasts = false;
        }

        internal void UnBlockMenu()
        {
            m_CanvasGroup.interactable = m_CanvasGroup.blocksRaycasts = true;
        }
    }
}