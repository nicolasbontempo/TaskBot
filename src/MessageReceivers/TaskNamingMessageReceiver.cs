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
    public class TaskNamingMessageReceiver : IMessageReceiver
    {
        private readonly ISender _sender;
        private readonly IBucketExtension _bucketExtension;
        public TaskNamingMessageReceiver(ISender sender, IBucketExtension bucketExtension)
        {
            _sender = sender;
            _bucketExtension = bucketExtension;
        }
        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Log.Debug($"Naming task. From: {message.From} \tContent: {message.Content}");

            var todayDate = DateTime.Now;
            var docJson = new JsonDocument
            {
                { "x", "Tasks" }
            };
            await _bucketExtension.SetAsync(todayDate.ToString("yyyy-MM-dd"), docJson);
            await _sender.SendMessageAsync("", message.From, cancellationToken);
        }
    }
}
