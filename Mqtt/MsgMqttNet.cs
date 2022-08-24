using Mqtt.DataMqtt;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mqtt
{
    public static  class MsgMqttNet
    {
        private static IMqttClient mqttClient = null;
       
        static string Cliente ="";
        static string Server = "";
        static string Usuario = "";
        static string Password = "";
        static int Puerto = 1883;

        public static async Task ConnectMqttServerAsync(string clienteId ,string server,string usu, string pass,int puerto )
        {
            // Create a new MQTT client.
            Cliente = clienteId;
            Server = server;
            Usuario = usu;
            Password = pass;
            Puerto = puerto;
            if (mqttClient == null)
            {
                var factory = new MqttFactory();
                mqttClient = factory.CreateMqttClient();

                
                mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
                mqttClient.Connected += MqttClient_Connected;
                mqttClient.Disconnected += MqttClient_Disconnected;
            }

            //非托管客户端
            try
            {
                ////Create TCP based options using the builder.
                //var options1 = new MqttClientOptionsBuilder()
                //    .WithClientId("client001")
                //    .WithTcpServer("192.168.88.3")
                //    .WithCredentials("bud", "%spencer%")
                //    .WithTls()
                //    .WithCleanSession()
                //    .Build();

                //// Use TCP connection.
                //var options2 = new MqttClientOptionsBuilder()
                //    .WithTcpServer("192.168.88.3", 8222) // Port is optional
                //    .Build();

                //// Use secure TCP connection.
                //var options3 = new MqttClientOptionsBuilder()
                //    .WithTcpServer("192.168.88.3")
                //    .WithTls()
                //    .Build();

                //Create TCP based options using the builder.
                var options = new MqttClientOptionsBuilder()
                    .WithClientId(clienteId)
                    .WithTcpServer(server,puerto)
                    .WithCredentials(usu, pass)
                    //.WithTls()//服务器端没有启用加密协议，这里用tls的会提示协议异常
                    .WithCleanSession()
                    .Build();

                //// For .NET Framwork & netstandard apps:
                //MqttTcpChannel.CustomCertificateValidationCallback = (x509Certificate, x509Chain, sslPolicyErrors, mqttClientTcpOptions) =>
                //{
                //    if (mqttClientTcpOptions.Server == "server_with_revoked_cert")
                //    {
                //        return true;
                //    }

                //    return false;
                //};

                //2.4.0版本的
                //var options0 = new MqttClientTcpOptions
                //{
                //    Server = "127.0.0.1",
                //    ClientId = Guid.NewGuid().ToString().Substring(0, 5),
                //    UserName = "u001",
                //    Password = "p001",
                //    CleanSession = true
                //};

                await mqttClient.ConnectAsync(options);

                await TopicoSuscriptions();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message  );
                //Invoke((new Action(() =>
                //{
                //    txtReceiveMessage.AppendText($"连接到MQTT服务器失败！" + Environment.NewLine + ex.Message + Environment.NewLine);
                //})));
            }

            //托管客户端
            try
            {
                //// Setup and start a managed MQTT client.
                //var options = new ManagedMqttClientOptionsBuilder()
                //    .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                //    .WithClientOptions(new MqttClientOptionsBuilder()
                //        .WithClientId("Client_managed")
                //        .WithTcpServer("192.168.88.3", 8223)
                //        .WithTls()
                //        .Build())
                //    .Build();

                //var mqttClient = new MqttFactory().CreateManagedMqttClient();
                //await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("my/topic").Build());
                //await mqttClient.StartAsync(options);
            }
            catch (Exception)
            {

            }
        }

        public static async Task DisconnectAsync()
        {
            await mqttClient.DisconnectAsync();
        }

        public static async Task<object> UnsubscribeAsync(List<string> topics)
        {
            try
            {
                string Str = "";
                await mqttClient.UnsubscribeAsync(topics);
                
                foreach (var item in topics)
                {
                    Str += "Topic : " + item + "   ";
                }
                return "OK : " + Str;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public static async Task<object> Subscribe(string topic)
        {
            try
            {
                if (string.IsNullOrEmpty(topic))
                {
                    Debug.WriteLine("el topico no puede estar vacio");
                    return "el topico no puede estar vacio";
                }

                if (!mqttClient.IsConnected)
                {
                    Debug.WriteLine("No hay coneccion a el sevidor Mqtt");
                    return "No hay coneccion a el sevidor Mqtt";
                }

                var res = await mqttClient.SubscribeAsync(new TopicFilterBuilder()
                   .WithTopic(topic)
                   .WithAtMostOnceQoS()
                   .Build()
                   );
                if (res.GetType().Name == "List`1")
                {
                    Debug.WriteLine("Se a suscribido a " + topic + " con exito");
                }
                // Subscribe to a topic
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return ex;
            }
        }
        public static async Task<object> SubscribeList(List<string> topics)
        {

           
            if (topics.Count() == 0)
            {
                Debug.WriteLine("Los topicoa no pueden estar vacio");
                return "Los topicoa no pueden estar vacio";
            }

            if (!mqttClient.IsConnected)
            {
                Debug.WriteLine("No hay coneccion a el sevidor Mqtt");
                return "No hay coneccion a el sevidor Mqtt";
            }

            List<TopicFilter> ListTopics = new List<TopicFilter>();
            foreach (var item in topics)
            {
                ListTopics.Add(new TopicFilterBuilder().WithTopic(item).WithAtMostOnceQoS().Build());
            }



           IList<MqttSubscribeResult> ListSub =  await mqttClient.SubscribeAsync(ListTopics);
            string d = ListSub.GetType().Name;
            ListSub = ListSub.Where(x => x.ReturnCode == MqttSubscribeReturnCode.Failure).ToList();

          
            while (ListSub.Count() != 0)
            {
                ListSub = await mqttClient.SubscribeAsync(ListTopics);
                ListSub = ListSub.Where(x => x.ReturnCode == MqttSubscribeReturnCode.Failure).ToList();

            }
            
             if (ListSub.GetType().Name == "List`1")
             {
                Debug.WriteLine("la lista de topicos fue subcrita con exito");
             }

            return ListSub;
           
        }


        public static async Task<int> Publish(string topic,string msg)
        {


            if (string.IsNullOrEmpty(topic))
            {
                Debug.WriteLine("el topico no puede estar vacio");
                return 1;
            }

            if (!mqttClient.IsConnected)
            {
                Debug.WriteLine("No hay coneccion a el sevidor Mqtt");
                return 2;
            }

            
           
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(msg)
                .WithAtMostOnceQoS()
                .WithRetainFlag(true)
                .Build();

            try
            {
                await mqttClient.PublishAsync(message);
                return 3;
            }
            catch (Exception)
            {

                return 4;
            } 
        }

        private static  void MqttClient_Connected(object sender, EventArgs e)
        {
            Debug.WriteLine("Conectado a la servier = " + Server + " cone el cliente = " + Cliente);
        }

        private static void MqttClient_Disconnected(object sender, EventArgs e)
        {
            Debug.WriteLine("ReConectando");
            var options = new MqttClientOptionsBuilder()
                .WithClientId(Cliente)
                .WithTcpServer(Server, Puerto)
                .WithCredentials(Usuario, Password)
                //.WithTls()
                .WithCleanSession()
                .Build();
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await mqttClient.ConnectAsync(options);
                    await TopicoSuscriptions();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Reconeccion fallida  " + ex.Message);
                }
            }); 
        }

        private static async void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            Debug.WriteLine($">> Topic esta = {e.ApplicationMessage.Topic} {msg}");


            if (e.ApplicationMessage.Topic == Topics.TopicRaiz + "LoginDevice")
            {
                var res = await DispositivoMqtt.LoginDevice(msg);

                if(res.GetType().Name == "Dispositivo")
                {
                    Debug.WriteLine("Se a agergado un dispositivo");
                }
            }
        }
        static async Task TopicoSuscriptions()
        {
            var LoginDevice = await MsgMqttNet.Subscribe(Topics.TopicRaiz + "LoginDevice");

            await DispositivoMqtt.SubcripcionAllDevice();

        }
    }
}
