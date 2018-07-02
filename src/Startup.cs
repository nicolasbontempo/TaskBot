using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lime.Messaging.Resources;
using Lime.Protocol.Server;
using Serilog;
using Serilog.Core;
using Take.Blip.Client;

namespace TaskBot
{
    /// <summary>
    /// Defines a type that is called once during the application initialization.
    /// </summary>
    public class Startup : IStartable
    {
        private readonly ISender _sender;
        private readonly Settings _settings;

        public Startup(ISender sender, Settings settings)
        {
            _sender = sender;
            _settings = settings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            Mapper.Initialize(cfg => cfg.CreateMap<Account, Contact>()
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.FullName)));
            return Task.CompletedTask;
        }
    }
}
