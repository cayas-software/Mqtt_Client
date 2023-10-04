using System;
using MQTTClient.Models;

namespace MQTTClient.DataTemplateSelectors
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OutgoingMessageTemplate { get; set; }
        public DataTemplate IncomingMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = (MessageModel)item;

            switch (message.MessageType)
            {
                case MessageType.IncomingMessage:
                    return IncomingMessageTemplate;
                case MessageType.OutGoingMessage:
                    return OutgoingMessageTemplate;
                default:
                    return OutgoingMessageTemplate;
            }
        }
    }
}

