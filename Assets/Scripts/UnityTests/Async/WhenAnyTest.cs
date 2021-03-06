﻿#if CSHARP_7_OR_LATER
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Scripting;
using UniRx;
using UniRx.Async;
using Unity.Collections;
using System.Threading;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace UniRx.AsyncTests
{
    public class WhenAnyTest
    {
        [UnityTest]
        public IEnumerator WhenAnyCanceled() => UniTask.ToCoroutine(async () =>
        {
            var cts = new CancellationTokenSource();
            var successDelayTask = UniTask.Delay(TimeSpan.FromSeconds(1));
            var cancelTask = UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cts.Token);
            cts.CancelAfterSlim(200);

            try
            {
                var r = await UniTask.WhenAny(new[] { successDelayTask, cancelTask });
            }
            catch (Exception ex)
            {
                ex.IsInstanceOf<OperationCanceledException>();
            }
        });
    }
}

#endif