﻿using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using JT808.Gateway.Abstractions;

namespace JT808.Gateway.MsgIdHandler
{
    public class JT808MsgIdHandlerHostedService : IHostedService
    {
        private readonly IJT808MsgConsumer jT808MsgConsumer;

        private readonly IJT808MsgIdHandler jT808MsgIdHandler;
        public JT808MsgIdHandlerHostedService(
            IJT808MsgIdHandler jT808MsgIdHandler,
            IJT808MsgConsumer jT808MsgConsumer)
        {
            this.jT808MsgIdHandler = jT808MsgIdHandler;
            this.jT808MsgConsumer = jT808MsgConsumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            jT808MsgConsumer.Subscribe();
            jT808MsgConsumer.OnMessage(jT808MsgIdHandler.Processor);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            jT808MsgConsumer.Unsubscribe();
            return Task.CompletedTask;
        }
    }
}
