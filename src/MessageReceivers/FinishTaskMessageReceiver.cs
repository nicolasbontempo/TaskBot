using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Serilog;
using Take.Blip.Client;

namespace TaskBot.MessageReceivers
{
    public class FinishTaskMessageReceiver : IMessageReceiver
    {
        private readonly ISender _sender;
        public FinishTaskMessageReceiver(ISender sender)
        {
            _sender = sender;
        }

        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Log.Debug($"Finish task menu option selected. From: {message.From} \tContent: {message.Content}");
            await _sender.SendMessageAsync("Option 2!", message.From, cancellationToken);
        }
    }
}
