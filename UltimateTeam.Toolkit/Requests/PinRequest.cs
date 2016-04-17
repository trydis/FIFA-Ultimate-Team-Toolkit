using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PinEventRequest : FutRequestBase, IFutRequest<PinResponse>
    {
        private readonly AppVersion _appVersion;
        private readonly string _nucleusId;
        private readonly string _personaId;
        private readonly string _sessionId;
        private readonly string _currentPinEventIdString;
        private readonly string _previousPinEventIdString;
        private PinEventId _currentPinEventId;
        private readonly uint _pinRequestCount;

        public PinEventRequest(AppVersion appVersion, string SessionId, string NucleusId, string PersonaId, PinEventId currentPinId, PinEventId previousPinId, uint pinRequestCount)
        {
            _appVersion = appVersion;
            _sessionId = SessionId;
            _nucleusId = NucleusId;
            _personaId = PersonaId;
            _currentPinEventIdString = ObjectExtensions.DataContractSerializeObject(currentPinId);
            _previousPinEventIdString = ObjectExtensions.DataContractSerializeObject(previousPinId);
            _currentPinEventId = currentPinId;
            _pinRequestCount = pinRequestCount;
        }

        public async Task<PinResponse> PerformRequestAsync()
        {
            if (AppVersion != AppVersion.WebApp)
            {
                AddPinHeadersMobile();
            }
            else
            {
                AddPinHeaders();
            }
            var pinResponseMessage = await HttpClient.PostAsync(string.Format(Resources.PinRiver), new StringContent(Serialize(GeneratePinData())))
                .ConfigureAwait(false);

            return await DeserializeAsync<PinResponse>(pinResponseMessage);
        }

        private ExpandoObject GeneratePinData()
        {
            List<object> pinDataEvents = new List<object>();
            dynamic pinData = new ExpandoObject();
            dynamic pinDataEventsCore = new ExpandoObject();
            dynamic pinDataEventsCorePidm = new ExpandoObject();
            dynamic pinDataCustom = new ExpandoObject();
            dynamic pinDataEvent = new ExpandoObject();

            pinData.custom = pinDataCustom;
            pinDataEventsCore.pidm = pinDataEventsCorePidm;
            pinDataEvent.core = pinDataEventsCore;
            pinDataEvents.Add(pinDataEvent);
            pinData.events = pinDataEvents;

            pinData.et = "client";
            pinData.loc = "en_GB";
            pinData.rel = "prod";
            pinData.sid = _sessionId;
            pinData.ts_post = DateTimeExtensions.ToISO8601Time(DateTime.UtcNow);

            pinDataEvent.type = "menu";
            pinDataEventsCore.en = "page_view";
            pinDataEventsCore.pid = _personaId;
            pinDataEventsCorePidm.nucleus = _nucleusId;
            pinDataEventsCore.pidt = "persona";
            pinDataEventsCore.s = _pinRequestCount;
            pinDataEventsCore.ts_event = DateTimeExtensions.ToISO8601Time(DateTime.UtcNow);

            if (_appVersion == AppVersion.CompanionApp)
            {
                pinDataCustom.networkAccess = "W";
                pinData.taxv = "1";
                pinData.tid = "859051";
                pinData.tidt = "sellid";
                pinData.plat = "android";
                pinData.v = "16.2.0.155106";

                if (_currentPinEventId == PinEventId.CompanionApp_AppOpened)
                {
                    pinData.events = pinDataEventsCore;
                    pinDataEventsCore.en = "connection";
                    pinDataEventsCore.pid = new ExpandoObject();
                    pinDataEventsCorePidm.nucleus = "0";
                    pinData.sid = new ExpandoObject();
                }
                else if (_currentPinEventId == PinEventId.CompanionApp_Connect)
                {
                    pinDataEventsCore.en = "login";
                    pinDataEventsCore.pid = new ExpandoObject();
                    pinDataEventsCorePidm.nucleus = _nucleusId;
                    pinDataEvent.status = "success";
                    pinDataEvent.type = "PAS";
                    pinData.sid = new ExpandoObject();
                }
                else if (_currentPinEventId == PinEventId.CompanionApp_Connected)
                {
                    pinDataEventsCore.en = "login";
                    pinDataEventsCore.pid = _personaId;
                    pinDataEventsCorePidm.nucleus = _nucleusId;
                    pinDataEvent.status = "success";
                    pinDataEvent.status = "UTAS";
                    pinData.sid = _sessionId;
                    pinData.userid = _personaId;
                }
                else
                {
                    pinDataEvent.pgid = _currentPinEventIdString;
                }
                return pinData;
            }
            else if (_appVersion == AppVersion.WebApp)
            {
                pinData.taxv = "1.1";
                pinData.tid = "FUT16WEB";
                pinData.tidt = "sku";
                pinData.plat = "win";
                pinData.v = "16.0.155438";
                pinDataEvent.custom = new ExpandoObject();


                if (_currentPinEventId == PinEventId.WebApp_Home)
                {
                    pinDataEventsCore.en = "login";
                    pinDataEventsCore.pid = _personaId;
                    pinDataEventsCorePidm.nucleus = _nucleusId;
                    pinDataEvent.status = "success";
                    pinDataEvent.status = "nucleus";
                    pinData.sid = _sessionId;
                    pinData.userid = _personaId;
                }
                else
                {
                    pinDataEvent.fromid = _previousPinEventIdString;
                    pinDataEvent.toid = _currentPinEventIdString;
                }

                return pinData;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", _appVersion.ToString()));
            }
        }
    }
}