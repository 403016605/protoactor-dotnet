﻿// -----------------------------------------------------------------------
//  <copyright file="Props.cs" company="Asynkron HB">
//      Copyright (C) 2015-2016 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

using System;

namespace GAM
{
    public sealed class Props
    {
        private Func<IActor> _actorProducer;

        private IDispatcher _dispatcher;
        private Func<IMailbox> _mailboxProducer;

        public Func<IActor> Producer => _actorProducer;
        public Func<IMailbox> MailboxProducer => _mailboxProducer ?? (() => new DefaultMailbox());

        public IDispatcher Dispatcher => _dispatcher ?? new ThreadPoolDispatcher();

        public Props WithDispatcher(IDispatcher dispatcher)
        {
            return Copy(dispatcher: dispatcher);
        }


        public Props Copy(Func<IActor> producer = null, IDispatcher dispatcher = null,
            Func<IMailbox> mailboxProducer = null)
        {
            return new Props()
            {
                _actorProducer = producer ?? _actorProducer,
                _dispatcher = dispatcher ?? Dispatcher,
                _mailboxProducer = mailboxProducer ?? _mailboxProducer,
            };
        }

        public Props WithMailbox(Func<IMailbox> mailboxProducer)
        {
            return Copy(mailboxProducer: mailboxProducer);
        }
    }
}