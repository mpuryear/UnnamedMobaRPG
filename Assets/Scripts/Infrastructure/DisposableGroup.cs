using System;
using System.Collections.Generic;

namespace Unity.MobaRPG.Infrastructure
{
    public class DisposableGroup : IDisposable
    {
        readonly List<IDisposable> m_Disposables = new List<IDisposable>();

        public void Dispose()
        {
            foreach (var dispoable in m_Disposables)
            {
                dispoable.Dispose();
            }

            m_Disposables.Clear();
        }

        public void Add(IDisposable disposable)
        {
            m_Disposables.Add(disposable);
        }
    }
}
