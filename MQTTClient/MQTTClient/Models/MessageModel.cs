using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MQTTClient.Models
{
    public partial class MessageModel : ObservableObject
    {
        [ObservableProperty]
        string _text;

        [ObservableProperty]
        MessageType _messageType;
    }

    public enum MessageType
    {
        OutGoingMessage,
        IncomingMessage,
    }
}

