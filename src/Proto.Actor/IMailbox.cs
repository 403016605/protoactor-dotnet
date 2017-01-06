﻿// -----------------------------------------------------------------------
//  <copyright file="IMailbox.cs" company="Asynkron HB">
//      Copyright (C) 2015-2016 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Proto
{
    internal static class MailboxStatus
    {
        public const int Idle = 0;
        public const int Busy = 1;
    }

    public interface IMailbox
    {
        void PostUserMessage(object msg);
        void PostSystemMessage(SystemMessage sys);
        void RegisterHandlers(IMessageInvoker invoker, IDispatcher dispatcher);
    }

    public class DefaultMailbox : IMailbox
    {
        private readonly ConcurrentQueue<SystemMessage> _systemMessages = new ConcurrentQueue<SystemMessage>();
        private readonly ConcurrentQueue<object> _userMessages = new ConcurrentQueue<object>();
        private IDispatcher _dispatcher;
        private IMessageInvoker _invoker;

        private int _status = MailboxStatus.Idle;
        private bool _suspended;

        public void PostUserMessage(object msg)
        {
            _userMessages.Enqueue(msg);
            Schedule();
        }

        public void PostSystemMessage(SystemMessage sys)
        {
            _systemMessages.Enqueue(sys);
            Schedule();
        }

        public void RegisterHandlers(IMessageInvoker invoker, IDispatcher dispatcher)
        {
            _invoker = invoker;
            _dispatcher = dispatcher;
        }

        private async Task RunAsync()
        {
            var t = _dispatcher.Throughput;

            for (var i = 0; i < t; i++)
            {
                SystemMessage sys;
                if (_systemMessages.TryDequeue(out sys))
                {
                    if (sys is SuspendMailbox)
                        _suspended = true;
                    if (sys is ResumeMailbox)
                        _suspended = false;
                    _invoker.InvokeSystemMessage(sys);
                    continue;
                }
                if (_suspended)
                    break;
                object msg;
                if (_userMessages.TryDequeue(out msg))
                    await _invoker.InvokeUserMessageAsync(msg);
                else
                    break;
            }

            Interlocked.Exchange(ref _status, MailboxStatus.Idle);

            if (_userMessages.Count > 0 || _systemMessages.Count > 0)
                Schedule();
        }

        protected void Schedule()
        {
            if (Interlocked.Exchange(ref _status, MailboxStatus.Busy) == MailboxStatus.Idle)
                _dispatcher.Schedule(RunAsync);
        }
    }
}