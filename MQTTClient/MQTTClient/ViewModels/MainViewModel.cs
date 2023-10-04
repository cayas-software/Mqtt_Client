using System;
using MQTTnet;
using MQTTnet.Client;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using MQTTClient.Models;
using System.Collections.ObjectModel;

namespace MQTTClient.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        IMqttClient _mqttClient;
        string _currentUser = "user1";
        string _chatPartner = "user2";

        [ObservableProperty]
        ObservableCollection<MessageModel> _messages;

        [ObservableProperty]
        string _message;

        [RelayCommand]
        async void Send()
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic($"chatChannel/{_chatPartner}")
                .WithPayload(Message)
                .Build();

            await _mqttClient.PublishAsync(applicationMessage);

            Messages.Add(new MessageModel() { Text = $"You: \n{Message}", MessageType = MessageType.OutGoingMessage });
            Message = string.Empty;
        }

        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
        }

        public async Task CreateClient()
        {
            // Create the client object
            _mqttClient = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                                    .WithClientId(_currentUser)
                                    .WithTcpServer("YOUT IP", 1884)
                                    .Build();

            // Set up handlers
            _mqttClient.ConnectedAsync += MqttClient_ConnectedAsync;
            _mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;

            // Connect to broker
            await _mqttClient.ConnectAsync(options);
            await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter($"chatChannel/{_currentUser}").Build());
        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Messages.Add(new MessageModel() { Text = $"{arg.ClientId}: \n{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}", MessageType = MessageType.IncomingMessage });
            return Task.CompletedTask;
        }

        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("DISCONNECTED");
            return Task.CompletedTask;
        }

        private Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine("CONNECTED");
            return Task.CompletedTask;
        }
    }
}

