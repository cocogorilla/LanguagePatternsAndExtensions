﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public class LifeTimeManager<T>
    {
        private readonly Func<Task<T>> _receiverAsync;
        private readonly Func<T, bool> _expirationDecider;
        private T _instance;
        private bool _initialized = false;
        private readonly SemaphoreSlim _semaphoreSlim;

        public LifeTimeManager(Func<Task<T>> receiverAsync, Func<T, bool> expirationDecider)
        {
            if (receiverAsync == null) throw new ArgumentNullException(nameof(receiverAsync));
            if (expirationDecider == null) throw new ArgumentNullException(nameof(expirationDecider));
            _receiverAsync = receiverAsync;
            _expirationDecider = expirationDecider;
            _semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        public async Task<T> ReceiveMessage()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                if (!_initialized || _expirationDecider(_instance))
                {
                    if (!_initialized)
                        _initialized = true;
                    _instance = await _receiverAsync();
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
            return await Task.FromResult(_instance);
        }
    }
}
