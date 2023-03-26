using System;
using Unity.MobaRPG.ApplicationLifecycle.Messages;
using Unity.MobaRPG.ConnectionManagement;
using Unity.MobaRPG.Infrastructure;
using UnityEngine;
using VContainer;


namespace Unity.MobaRPG.Gameplay.UI
{
    public class UIQuitPanel : MonoBehaviour
    {
        enum QuitMode
        {
            ReturnToMenu,
            QuitApplication,
        }

        [SerializeField]
        QuitMode m_QuitMode = QuitMode.ReturnToMenu;

        [Inject]
        ConnectionManager m_ConnectionManager;

        [Inject]
        IPublisher<QuitApplicationMessage> m_QuitApplicationPub;

        public void Quit()
        {
            switch (m_QuitMode)
            {
                case QuitMode.ReturnToMenu:
                    m_ConnectionManager.RequestShutdown();
                    break;
                case QuitMode.QuitApplication:
                    m_QuitApplicationPub.Publish(new QuitApplicationMessage());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            gameObject.SetActive(false);
        }

    }
}