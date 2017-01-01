// -----------------------------------------------------------------------
//  <copyright file="ProtosGrpc.cs" company="Asynkron HB">
//      Copyright (C) 2015-2016 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

#region Designer generated code

using System;
using System.Threading;
using Grpc.Core;

namespace GAM.Remoting
{
    public static class Remoting
    {
        static readonly string __ServiceName = "remoting.Remoting";

        static readonly Marshaller<global::GAM.Remoting.MessageBatch> __Marshaller_MessageBatch =
            Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg),
                global::GAM.Remoting.MessageBatch.Parser.ParseFrom);

        static readonly Marshaller<global::GAM.Remoting.Unit> __Marshaller_Unit =
            Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg),
                global::GAM.Remoting.Unit.Parser.ParseFrom);

        static readonly Method<global::GAM.Remoting.MessageBatch, global::GAM.Remoting.Unit> __Method_Receive = new Method
            <global::GAM.Remoting.MessageBatch, global::GAM.Remoting.Unit>(
            MethodType.ClientStreaming,
            __ServiceName,
            "Receive",
            __Marshaller_MessageBatch,
            __Marshaller_Unit);

        /// <summary>Service descriptor</summary>
        public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
        {
            get { return global::GAM.Remoting.ProtosReflection.Descriptor.Services[0]; }
        }

        /// <summary>Creates service definition that can be registered with a server</summary>
        public static ServerServiceDefinition BindService(RemotingBase serviceImpl)
        {
            return ServerServiceDefinition.CreateBuilder()
                .AddMethod(__Method_Receive, serviceImpl.Receive).Build();
        }

        /// <summary>Base class for server-side implementations of Remoting</summary>
        public abstract class RemotingBase
        {
            public virtual global::System.Threading.Tasks.Task<global::GAM.Remoting.Unit> Receive(
                IAsyncStreamReader<global::GAM.Remoting.MessageBatch> requestStream, ServerCallContext context)
            {
                throw new RpcException(new Status(StatusCode.Unimplemented, ""));
            }
        }

        /// <summary>Client for Remoting</summary>
        public class RemotingClient : ClientBase<RemotingClient>
        {
            /// <summary>Creates a new client for Remoting</summary>
            /// <param name="channel">The channel to use to make remote calls.</param>
            public RemotingClient(Channel channel) : base(channel)
            {
            }

            /// <summary>Creates a new client for Remoting that uses a custom <c>CallInvoker</c>.</summary>
            /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
            public RemotingClient(CallInvoker callInvoker) : base(callInvoker)
            {
            }

            /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
            protected RemotingClient() : base()
            {
            }

            /// <summary>Protected constructor to allow creation of configured clients.</summary>
            /// <param name="configuration">The client configuration.</param>
            protected RemotingClient(ClientBaseConfiguration configuration) : base(configuration)
            {
            }

            public virtual AsyncClientStreamingCall<global::GAM.Remoting.MessageBatch, global::GAM.Remoting.Unit>
                Receive(Metadata headers = null, DateTime? deadline = null,
                    CancellationToken cancellationToken = default(CancellationToken))
            {
                return Receive(new CallOptions(headers, deadline, cancellationToken));
            }

            public virtual AsyncClientStreamingCall<global::GAM.Remoting.MessageBatch, global::GAM.Remoting.Unit>
                Receive(CallOptions options)
            {
                return CallInvoker.AsyncClientStreamingCall(__Method_Receive, null, options);
            }

            protected override RemotingClient NewInstance(ClientBaseConfiguration configuration)
            {
                return new RemotingClient(configuration);
            }
        }
    }
}

#endregion