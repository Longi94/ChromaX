using System.Collections.Generic;
using System.Timers;
using log4net;
using RestSharp;
using RestSharp.Deserializers;

namespace ChromaX.Service
{
    public class ChromaService
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string MainUrl = "http://localhost:54235/razer/chromasdk";

        public static readonly Dictionary<string, object> AppInfo = new Dictionary<string, object>
        {
            {"title", "ChromaX"},
            {"description", "Chroma animation studio"},
            {
                "author", new Dictionary<string, string>
                {
                    {"name", "Long Tran"},
                    {"contact", "https://github.com/Longi94/ChromaX/"}
                }
            },
            {"device_supported", new[] {"keyboard"}},
            {"category", "application"}
        };

        public bool Initialized { get; private set; }

        private readonly Timer _heartbeatTimer = new Timer {Interval = 10000};

        private readonly RestClient _mainClient = new RestClient(MainUrl);

        public string Uri { get; private set; }

        private RestClient _client;

        public ChromaService()
        {
            _heartbeatTimer.Elapsed += Heartbeat;
        }

        /// <summary>
        /// Initialized the Chroma SDK if not initialized already.
        /// </summary>
        public void Initialize()
        {
            if (Initialized) return;

            Log.Info("Initializing Chroma SDK...");

            var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(AppInfo);

            _mainClient.ExecuteAsync<InitResponse>(request, response =>
            {
                if (response.IsSuccessful && response.Data.Uri != null)
                {
                    Uri = response.Data.Uri;
                    _client = new RestClient(Uri);
                    Initialized = true;
                    _heartbeatTimer.Start();
                    Log.Info("Initialized Chroma SDK");
                }
                else
                {
                    Log.Error("Failed to initialize SDK");
                    Log.Error(response.ErrorMessage);
                }
            });
        }

        /// <summary>
        /// Uninitialized the Chroma SDK. Synapse (or other apps) will assume control of the lights.
        /// </summary>
        public void UnInitialize()
        {
            if (!Initialized) return;

            Log.Info("Uninitializing Chroma SDK...");
            _heartbeatTimer.Stop();

            var request = new RestRequest(Method.DELETE);

            _client.ExecuteAsync<UnInitResponse>(request, response =>
            {
                Initialized = false;
                if (response.IsSuccessful && response.Data.Result == 0)
                {
                    Log.Info("Uninitialized Chroma SDK");
                }
                else
                {
                    Log.Error("Failed to uninitialize SDK");
                    Log.Error(response.IsSuccessful ? $"Returned code: {response.Data.Result}" : response.ErrorMessage);
                }
            });
        }

        /// <summary>
        /// Send a heartbeat message to the chroma sdk to keep the connection alive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Heartbeat(object sender, ElapsedEventArgs e)
        {
            if (!Initialized) return;

            var request = new RestRequest("/heartbeat", Method.PUT);
            var response = _client.Execute<HeartbeatResponse>(request);

            if (response.IsSuccessful)
            {
                Log.Debug($"Chroma SDK heartbeat {response.Data.Tick}");
            }
            else
            {
                Log.Error("Heartbeat failed");
                Log.Error(response.ErrorMessage);
            }
        }
    }

    internal class InitResponse
    {
        [DeserializeAs(Name = "session")]
        public int Session { get; set; }

        [DeserializeAs(Name = "uri")]
        public string Uri { get; set; }
    }

    internal class UnInitResponse
    {
        [DeserializeAs(Name = "result")]
        public int Result { get; set; }
    }

    internal class HeartbeatResponse
    {
        [DeserializeAs(Name = "tick")]
        public int Tick { get; set; }
    }
}
