﻿using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tars.Net.Clients;

namespace Tars.Net.Extensions.AspectCore
{
    public static class ClientBuilderExtensions
    {
        public static void UseAspectCore(this ITarsClientBuilder builder)
        {
            var services = builder.Services;
            services.TryAddSingleton(j => new RpcClientInvokerFactory(builder.Clients, j.GetRequiredService<IRpcClientFactory>()));
            services.ReigsterRpcDependency();
            services.AddDynamicProxy(c =>
            {
                c.ValidationHandlers.Add(new RpcAspectValidationHandler());
            });
        }
    }
}