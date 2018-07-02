using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Serilog;
using Take.Blip.Client;
using Take.Blip.Client.Extensions.Bucket;

namespace TaskBot.MessageReceivers
{
    public class StartTaskMessageReceiver : IMessageReceiver
    {
        private readonly ISender _sender;
        private readonly IBucketExtension _bucketExtension;

        public StartTaskMessageReceiver(ISender sender, IBucketExtension bucketExtension)
        {
            _sender = sender;
            _bucketExtension = bucketExtension;
        }

        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Log.Debug($"Start task menu option selected. From: {message.From} \tContent: {message.Content}");


            await _sender.SendMessageAsync("Option 1!", message.From, cancellationToken);
        }
    }
}
