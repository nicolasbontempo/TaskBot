using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lime.Messaging.Contents;
using Lime.Messaging.Resources;
using Lime.Protocol;
using Serilog;
using Take.Blip.Client;
using Take.Blip.Client.Extensions.Contacts;
using Take.Blip.Client.Extensions.Directory;

namespace TaskBot.MessageReceivers
{
    /// <summary>
    /// Defines a class for handling messages. 
    /// This type must be registered in the application.json file in the 'messageReceivers' section.
    /// </summary>
    public class GreetingsMessageReceiver : IMessageReceiver
    {
        private readonly ISender _sender;
        private readonly Settings _settings;
        private readonly IContactExtension _contactExtension;
        private readonly IDirectoryExtension _directoryExtension;


        public GreetingsMessageReceiver(ISender sender, IContactExtension contactExtension, IDirectoryExtension directoryExtension, Settings settings)
        {
            _sender = sender;
            _settings = settings;
            _contactExtension = contactExtension;
            _directoryExtension = directoryExtension;
        }

        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Log.Debug($"Bot started. From: {message.From} \tContent: {message.Content}");

            var identity = Identity.Parse(message.From);
            var account = await _directoryExtension.GetDirectoryAccountAsync(identity, cancellationToken);
            var contact = Mapper.Map<Contact>(account);
            await _contactExtension.SetAsync(identity, contact, cancellationToken);
            await _sender.SendMessageAsync("Olá! Seja bem vindo ao acompanhador de tarefas!", message.From, cancellationToken);

            await _sender.SendMessageAsync(new ChatState { State = ChatStateEvent.Composing }, message.From, cancellationToken);
            var menu = new Select
            {
                Text = "Em que posso te ajudar?",
                Options = new SelectOption[]
                {
                    new SelectOption
                    {
                        Order = 1,
                        Text = "Iniciar uma nova tarefa",
                        Value = new PlainText { Text = "Iniciar uma nova tarefa" }
                    },
                    new SelectOption
                    {
                        Order = 2,
                        Text = "Finalizar uma nova tarefa",
                        Value = new PlainText { Text = "Finalizar uma nova tarefa" }
                    },
                }
            };

            await _sender.SendMessageAsync(menu, message.From, cancellationToken);
        }
    }
}
