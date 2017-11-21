using JulMar.Atapi;
using System;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    Console.WriteLine("[TAPI] line name: {0}", line.Capabilities.LineName);
                    Console.WriteLine("[TAPI] line can make call: {0}", line.Capabilities.SupportsMakeCall);
                    Console.WriteLine("[TAPI] line has media provider: {0}", line.Capabilities.HasMediaServiceProvider);
                    Console.WriteLine("[TAPI] line supports voice call: {0}", line.Capabilities.SupportsVoiceCalls);
                    foreach (string device in line.Capabilities.AvailableDeviceClasses)
                    {
                        Console.WriteLine("[TAPI] line available device: {0}", device);
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
            catch(Exception ex)
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
