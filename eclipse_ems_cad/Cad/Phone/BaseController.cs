using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phone.Models;

namespace Phone
{
    public class BaseController : BaseScript
    {
        public int GameControlButton = 243;
        public int OpenPhoneButton = 357;
        public BaseController()
        {

        }
        public string GetVehicleType(int id)
        {
            switch(id)
            {
                case 0:
                    return "Compacts";
                case 1:
                    return "Sedans";
                case 2:
                    return "SUVs";
                case 3:
                    return "Coupes";
                case 4:
                    return "Muscle";
                case 5:
                    return "Sports Classics";
                case 6:
                    return "Sports";
                case 7:
                    return "Super";
                case 8:
                    return "Motorcycles";
                case 9:
                    return "Off-road";
                case 10:
                    return "Industrial";
                case 11:
                    return "Utility";
                case 12:
                    return "Vans";
                case 13:
                    return "Cycles";
                case 14:
                    return "Boats";
                case 15:
                    return "Helicopters";
                case 16:
                    return "Planes";
                case 17:
                    return "Service";
                case 18:
                    return "Emergency";
                case 19:
                    return "Military";
                case 20:
                    return "Commercial";
                case 21:
                    return "Trains";
            }
            return null;
        }
        public async Task TryAsync(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception e)
            {
                HandleException(e);
            }

        }
        public void SendMessageToCEF(string type)
        {
            SendNuiMessage($"{{\"type\": \"{type}\"}}");
        }
        public void RegisterNuiCallbackType(string nuiHandler, Action<IDictionary<string, object>, CallbackDelegate> action)
        {
            Action<IDictionary<string, object>, CallbackDelegate> tryAction = (data, cb) =>
            {
                Try(() =>
                {
                    action(data, cb);
                });
            };
            API.RegisterNuiCallbackType(nuiHandler);
            EventHandlers[$"__cfx_nui:{nuiHandler}"] += tryAction;
        }
        public static void SendMessageToCEF(string type, Dictionary<object, object> args)
        {
            string msg = $"{{\"type\": \"{type}\"";
            foreach (var pair in args)
            {
                msg += $", \"{pair.Key.ToString()}\": \"{pair.Value.ToString()}\"";
            }
            msg += "}";

            SendNuiMessage(msg);
        }
        public void RegisterNuiCallbackTypeAsync(string nuiHandler, Func<IDictionary<string, object>, CallbackDelegate, Task> action)
        {
            Func<IDictionary<string, object>, CallbackDelegate, Task> tryActionAsync = async (data, cb) =>
            {
                await TryAsync(async () =>
                {
                    await action(data, cb);
                });
            };

            API.RegisterNuiCallbackType(nuiHandler);
            EventHandlers[$"__cfx_nui:{nuiHandler}"] += tryActionAsync;
        }

        public void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void HandleException(Exception e)
        {
            var exceptionModel = new ExceptionModel
            {
                Message = e.Message,
                StackTrace = e.StackTrace
            };
            Debug.WriteLine($"[ERR!] Message: {e.Message}\r\nStackTrace:{e.StackTrace}");
            if (e.InnerException != null)
            {
                Debug.WriteLine($"[ERR!] Message: {e.InnerException.Message}\r\nStackTrace:{e.StackTrace}");
                exceptionModel.InnerMessage = e.InnerException.Message;
                exceptionModel.InnerStackTrace = e.InnerException.StackTrace;
            }

            var jsonException = JsonConvert.SerializeObject(exceptionModel);
            TriggerServerEvent("ClientException", jsonException);
        }
    }
}
