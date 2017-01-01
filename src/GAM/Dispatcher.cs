﻿// -----------------------------------------------------------------------
//  <copyright file="Dispatcher.cs" company="Asynkron HB">
//      Copyright (C) 2015-2016 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GAM
{
    public interface IDispatcher
    {
        int Throughput { get; }
        void Schedule(Func<Task> runner);
    }

    public sealed class ThreadPoolDispatcher : IDispatcher
    {
        public ThreadPoolDispatcher()
        {
            Throughput = 300;
        }

        public void Schedule(Func<Task> runner)
        {
            Task.Factory.StartNew(runner, TaskCreationOptions.None);
        }

        public int Throughput { get; set; }
    }
}