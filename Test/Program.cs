using JulMar.Atapi;
using System;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer(TimerPassed, null, 100, 5000);

            Console.WriteLine("[MAIN] start");
            Console.WriteLine("[TAPI] creation");
            TapiManager mgr = new TapiManager("EnumDevices", TapiVersion.V21);
            try
            {
                Console.WriteLine("[TAPI] initialization");
                mgr.Initialize(); // Start up Tapi
                Console.WriteLine("[TAPI] AVAILABLE LINES START");
                foreach (TapiLine line in mgr.Lines)
                {
                    Console.WriteLine("[TAPI] -----------------------------------------------");
                    Console.WriteLine("[TAPI] line: {0}", line.Name);
                    //Console.WriteLine("[TAPI] line name: {0}", line.Capabilities.LineName);
                    //Console.WriteLine("[TAPI] line can make call: {0}", line.Capabilities.SupportsMakeCall);
                    //Console.WriteLine("[TAPI] line has media provider: {0}", line.Capabilities.HasMediaServiceProvider);
                    //Console.WriteLine("[TAPI] line supports voice call: {0}", line.Capabilities.SupportsVoiceCalls);
                    foreach (string device in line.Capabilities.AvailableDeviceClasses)
                    {
                        Console.WriteLine("[TAPI] line available device: {0}", device);
                    }
                    if (line.Name.Contains("SEP001319785BBF"))
                    {
                        Console.WriteLine("---!!!LINE   SEP001319785BBF   FOUND!!!---");
                        Console.WriteLine("[LINE] EVENTS START");
                        Console.WriteLine("[LINE] NewCall");
                        line.NewCall += Line_NewCall;
                        Console.WriteLine("[LINE] CallStateChanged");
                        line.CallStateChanged += Line_CallStateChanged;
                        Console.WriteLine("[LINE] CallInfoChanged");
                        line.CallInfoChanged += Line_CallInfoChanged;
                        Console.WriteLine("[LINE] Changed");
                        line.Changed += Line_Changed;
                        Console.WriteLine("[LINE] Ringing");
                        line.Ringing += Line_Ringing;
                        Console.WriteLine("[LINE] EVENTS END");

                        line.Monitor();
                        Console.WriteLine("[LINE] monitoring set");

                        Console.WriteLine("---!!!LINE   SEP001319785BBF   ATTACHED!!!---");
                    }

                    Console.WriteLine("[TAPI] -----------------------------------------------");
                }
                Console.WriteLine("[TAPI] AVAILABLE LINES END");

                Console.WriteLine("[TAPI] PHONES START");
                if (mgr.Phones.Length < 1) Console.WriteLine("[TAPI] NO available phones");
                else
                {
                    foreach (var phone in mgr.Phones)
                    {
                        phone.PhoneStateChanged += Phone_PhoneStateChanged;
                        Console.WriteLine("[TAPI] available phone: {0} - {1}", phone.Name, phone.Id);
                    }
                }
                Console.WriteLine("[TAPI] PHONES END");

                Console.WriteLine("[TAPI] PROVIDERS START");
                if (mgr.Providers.Length < 1) Console.WriteLine("[TAPI] NO available providers");
                else
                {
                    foreach (var provider in mgr.Providers)
                    {
                        Console.WriteLine("[TAPI] available provider: {0} - {1}", provider.Name, provider.Id);
                    }
                }
                Console.WriteLine("[TAPI] PROVIDERS END");

                Console.WriteLine("[TAPI] EVENTS START");
                Console.WriteLine("[TAPI] AddressChanged");
                mgr.AddressChanged += Mgr_AddressChanged;
                Console.WriteLine("[TAPI] CallInfoChanged");
                mgr.CallInfoChanged += Mgr_CallInfoChanged;
                Console.WriteLine("[TAPI] CallStateChanged");
                mgr.CallStateChanged += Mgr_CallStateChanged;
                Console.WriteLine("[TAPI] LineAdded");
                mgr.LineAdded += Mgr_LineAdded;
                Console.WriteLine("[TAPI] LineChanged");
                mgr.LineChanged += Mgr_LineChanged;
                Console.WriteLine("[TAPI] LineRemoved");
                mgr.LineRemoved += Mgr_LineRemoved;
                Console.WriteLine("[TAPI] LineRinging");
                mgr.LineRinging += Mgr_LineRinging;
                Console.WriteLine("[TAPI] NewCall");
                mgr.NewCall += Mgr_NewCall;
                Console.WriteLine("[TAPI] PhoneAdded");
                mgr.PhoneAdded += Mgr_PhoneAdded;
                Console.WriteLine("[TAPI] PhoneRemoved");
                mgr.PhoneRemoved += Mgr_PhoneRemoved;
                Console.WriteLine("[TAPI] ReinitRequired");
                mgr.ReinitRequired += Mgr_ReinitRequired;
                Console.WriteLine("[TAPI] all events initialized");
                Console.WriteLine("[TAPI] EVENTS END");

            }
            catch (Exception ex)
            {
                Console.WriteLine("[MAIN] exception triggered");
                Console.WriteLine(ex);
            }
            Console.WriteLine("[MAIN] devices enumered");

            Console.ReadLine();
            Console.WriteLine("[TAPI] shutdown");
            mgr.Shutdown();
            Console.WriteLine("[MAIN] end");
            Console.ReadLine();
        }

        private static void TimerPassed(object state)
        {
            Console.WriteLine("[TIMER] {0}", DateTime.Now);
        }

        private static void Line_Ringing(object sender, RingEventArgs e)
        {
            Console.WriteLine("[LINE Ringing] invoked");
        }

        private static void Line_Changed(object sender, LineInfoChangeEventArgs e)
        {
            Console.WriteLine("[LINE Changed] Changed START");
            Console.WriteLine("[LINE Changed] Change {0}", e.Change);
            Console.WriteLine("[LINE Changed] Changed END");
        }

        private static void Line_CallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            try
            {
                Console.WriteLine("[LINE CallInfoChanged] CallInfoChanged START");
                Console.WriteLine("[LINE CallInfoChanged] CalledId {0}", e.Call.CalledId);
                Console.WriteLine("[LINE CallInfoChanged] CalledName {0}", e.Call.CalledName);
                Console.WriteLine("[LINE CallInfoChanged] CallerId {0}", e.Call.CallerId);
                Console.WriteLine("[LINE CallInfoChanged] CallerName {0}", e.Call.CallerName);
                Console.WriteLine("[LINE CallInfoChanged] CallState {0}", e.Call.CallState.ToString());
                Console.WriteLine("[LINE CallInfoChanged] ConnectedId {0}", e.Call.ConnectedId);
                Console.WriteLine("[LINE CallInfoChanged] ConnectedName {0}", e.Call.ConnectedName);
                Console.WriteLine("[LINE CallInfoChanged] call Id {0}", e.Call.Id);
                Console.WriteLine("[LINE CallInfoChanged] call line Id {0}", e.Call.Line.Id);
                Console.WriteLine("[LINE CallInfoChanged] call line Name {0}", e.Call.Line.Name);
                Console.WriteLine("[LINE CallInfoChanged] call line PermanentId {0}", e.Call.Line.PermanentId);
                Console.WriteLine("[LINE CallInfoChanged] call Privilege {0}", e.Call.Privilege.ToString());
                Console.WriteLine("[LINE CallInfoChanged] call RedirectingId {0}", e.Call.RedirectingId);
                Console.WriteLine("[LINE CallInfoChanged] call RedirectingName {0}", e.Call.RedirectingName);
                Console.WriteLine("[LINE CallInfoChanged] call RedirectionId {0}", e.Call.RedirectionId);
                Console.WriteLine("[LINE CallInfoChanged] call RedirectionName {0}", e.Call.RedirectionName);
                Console.WriteLine("[LINE CallInfoChanged] call RelatedId {0}", e.Call.RelatedId);
                Console.WriteLine("[LINE CallInfoChanged] call UserUserInfo {0}", e.Call.UserUserInfo);
                Console.WriteLine("[LINE CallInfoChanged] Change {0}", e.Change.ToString());
                Console.WriteLine("[LINE CallInfoChanged] CallStateChanged END");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[LINE CallInfoChanged] exception START");
                Console.WriteLine(ex);
                Console.WriteLine("[LINE CallInfoChanged] exception END");
            }
        }

        private static void Line_CallStateChanged(object sender, CallStateEventArgs e)
        {
            try
            {
                Console.WriteLine("[LINE CallStateChanged] CallStateChanged START");
                Console.WriteLine("[LINE CallStateChanged] CalledId {0}", e.Call.CalledId);
                Console.WriteLine("[LINE CallStateChanged] CalledName {0}", e.Call.CalledName);
                Console.WriteLine("[LINE CallStateChanged] CallerId {0}", e.Call.CallerId);
                Console.WriteLine("[LINE CallStateChanged] CallerName {0}", e.Call.CallerName);
                Console.WriteLine("[LINE CallStateChanged] CallState {0}", e.Call.CallState.ToString());
                Console.WriteLine("[LINE CallStateChanged] ConnectedId {0}", e.Call.ConnectedId);
                Console.WriteLine("[LINE CallStateChanged] ConnectedName {0}", e.Call.ConnectedName);
                Console.WriteLine("[LINE CallStateChanged] call Id {0}", e.Call.Id);
                Console.WriteLine("[LINE CallStateChanged] call line Id {0}", e.Call.Line.Id);
                Console.WriteLine("[LINE CallStateChanged] call line Name {0}", e.Call.Line.Name);
                Console.WriteLine("[LINE CallStateChanged] call line PermanentId {0}", e.Call.Line.PermanentId);
                Console.WriteLine("[LINE CallStateChanged] call Privilege {0}", e.Call.Privilege.ToString());
                Console.WriteLine("[LINE CallStateChanged] call RedirectingId {0}", e.Call.RedirectingId);
                Console.WriteLine("[LINE CallStateChanged] call RedirectingName {0}", e.Call.RedirectingName);
                Console.WriteLine("[LINE CallStateChanged] call RedirectionId {0}", e.Call.RedirectionId);
                Console.WriteLine("[LINE CallStateChanged] call RedirectionName {0}", e.Call.RedirectionName);
                Console.WriteLine("[LINE CallStateChanged] call RelatedId {0}", e.Call.RelatedId);
                Console.WriteLine("[LINE CallStateChanged] call UserUserInfo {0}", e.Call.UserUserInfo);
                Console.WriteLine("[LINE CallStateChanged] CallState {0}", e.CallState.ToString());
                Console.WriteLine("[LINE CallStateChanged] OldCallState {0}", e.OldCallState.ToString());
                Console.WriteLine("[LINE CallStateChanged] CallStateChanged END");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[LINE CallStateChanged] exception START");
                Console.WriteLine(ex);
                Console.WriteLine("[LINE CallStateChanged] exception END");
            }
        }

        private static void Line_NewCall(object sender, NewCallEventArgs e)
        {
            try
            {
                Console.WriteLine("[LINE NewCall] NewCall START");
                Console.WriteLine("[LINE NewCall] CalledId {0}", e.Call.CalledId);
                Console.WriteLine("[LINE NewCall] CalledName {0}", e.Call.CalledName);
                Console.WriteLine("[LINE NewCall] CallerId {0}", e.Call.CallerId);
                Console.WriteLine("[LINE NewCall] CallerName {0}", e.Call.CallerName);
                Console.WriteLine("[LINE NewCall] CallState {0}", e.Call.CallState.ToString());
                Console.WriteLine("[LINE NewCall] ConnectedId {0}", e.Call.ConnectedId);
                Console.WriteLine("[LINE NewCall] ConnectedName {0}", e.Call.ConnectedName);
                Console.WriteLine("[LINE NewCall] call Id {0}", e.Call.Id);
                Console.WriteLine("[LINE NewCall] call line Id {0}", e.Call.Line.Id);
                Console.WriteLine("[LINE NewCall] call line Name {0}", e.Call.Line.Name);
                Console.WriteLine("[LINE NewCall] call line PermanentId {0}", e.Call.Line.PermanentId);
                Console.WriteLine("[LINE NewCall] call Privilege {0}", e.Call.Privilege.ToString());
                Console.WriteLine("[LINE NewCall] call RedirectingId {0}", e.Call.RedirectingId);
                Console.WriteLine("[LINE NewCall] call RedirectingName {0}", e.Call.RedirectingName);
                Console.WriteLine("[LINE NewCall] call RedirectionId {0}", e.Call.RedirectionId);
                Console.WriteLine("[LINE NewCall] call RedirectionName {0}", e.Call.RedirectionName);
                Console.WriteLine("[LINE NewCall] call RelatedId {0}", e.Call.RelatedId);
                Console.WriteLine("[LINE NewCall] call UserUserInfo {0}", e.Call.UserUserInfo);
                Console.WriteLine("[LINE NewCall] Privilege {0}", e.Privilege.ToString());
                Console.WriteLine("[LINE NewCall] NewCall END");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[LINE NewCall] exception START");
                Console.WriteLine(ex);
                Console.WriteLine("[LINE NewCall] exception END");
            }
        }

        private static void Phone_PhoneStateChanged(object sender, PhoneStateEventArgs e)
        {
            try
            {
                Console.WriteLine("[PHONE PhoneStateChanged] PhoneStateChanged START");
                Console.WriteLine("[PHONE PhoneStateChanged] id {0}", e.Phone.Id);
                Console.WriteLine("[PHONE PhoneStateChanged] name {0}", e.Phone.Name);
                Console.WriteLine("[PHONE PhoneStateChanged] change {0}", e.Change.ToString());
                Console.WriteLine("[PHONE PhoneStateChanged] PhoneStateChanged END");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PHONE PhoneStateChanged] exception START");
                Console.WriteLine(ex);
                Console.WriteLine("[PHONE PhoneStateChanged] exception END");
            }
        }

        private static void Mgr_ReinitRequired(object sender, EventArgs e)
        {
            Console.WriteLine("[REINITREQUIRED] invoked");
        }

        private static void Mgr_PhoneRemoved(object sender, PhoneRemovedEventArgs e)
        {
            Console.WriteLine("[PHONEREMOVED] invoked");
        }

        private static void Mgr_PhoneAdded(object sender, PhoneAddedEventArgs e)
        {
            Console.WriteLine("[PHONEADDED] invoked");
        }

        private static void Mgr_LineRinging(object sender, RingEventArgs e)
        {
            Console.WriteLine("[LINERINGING] invoked");
        }

        private static void Mgr_LineRemoved(object sender, LineRemovedEventArgs e)
        {
            Console.WriteLine("[LINEREMOVED] invoked");
        }

        private static void Mgr_LineChanged(object sender, LineInfoChangeEventArgs e)
        {
            Console.WriteLine("[LINECHANGED] invoked");
        }

        private static void Mgr_LineAdded(object sender, LineAddedEventArgs e)
        {
            Console.WriteLine("[LINEADDED] invoked");
        }

        private static void Mgr_CallStateChanged(object sender, CallStateEventArgs e)
        {
            Console.WriteLine("[CALLSTATECHANGED] invoked");
        }

        private static void Mgr_CallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            Console.WriteLine("[CALLINFOCHANGED] invoked");
        }

        private static void Mgr_AddressChanged(object sender, AddressInfoChangeEventArgs e)
        {
            Console.WriteLine("[ADDRESSCHANGED] invoked");
        }

        private static void Mgr_NewCall(object sender, NewCallEventArgs e)
        {
            Console.WriteLine("[NEWCALL] invoked");
        }
    }
}
