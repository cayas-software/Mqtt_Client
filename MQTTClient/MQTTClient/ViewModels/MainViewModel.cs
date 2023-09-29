using System;
using MQTTnet;
using MQTTnet.Client;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;

namespace MQTTClient.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        IMqttClient _mqttClient;
        string _currentUser = "user1";
        string _chatPartner = "user2";

        public MainViewModel()
        {
        }

        public async Task CreateClient()
        {
            // Create the client object
            _mqttClient = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                                    .WithClientId(_currentUser)
                                    .WithTcpServer("YOUR IP", 1884)
                                    .Build();
        }
    }
}

