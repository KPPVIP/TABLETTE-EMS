using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Phone.Models;

namespace Phone
{
    public class NuiController : BaseController
    {

        public NuiController()
        {
            RegisterNuiCallbacks();
        }



        public void RegisterNuiCallbacks()
        {
            RegisterNuiCallbackType("ChangeVehicleStatus", (data, cb) =>
            {
                
                TriggerServerEvent("ECLIPSE_CAD:ChangeVehicleStatus", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("ChangeShift", (data, cb) =>
            {
                Debug.WriteLine($"change shift");
                TriggerServerEvent("ECLIPSE_CAD:ChangeShift", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("LoadCadData", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:LoadPlayerData");
                TriggerServerEvent("ECLIPSE_CAD:LoadVehiclesData");
                TriggerServerEvent("ECLIPSE_CAD:GetCitizens");
                TriggerServerEvent("ECLIPSE_CAD:GetPoliceCalls");
                TriggerServerEvent("ECLIPSE_CAD:GetActivePolice");

            });
            RegisterNuiCallbackType("SetMark", (data, cb) =>
            {

                TriggerServerEvent("ECLIPSE_CAD:SetMark", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("AddInsurance", (data, cb) =>
            {
                var id = Convert.ToString(data["id"]);
                //id = id.Insert(3, "-");
                TriggerServerEvent("ECLIPSE_CAD:AddInsurance", id);
            });
            RegisterNuiCallbackType("RemoveInsurance", (data, cb) =>
            {
                var id = Convert.ToString(data["id"]);
                //id = id.Insert(3, "-");
                TriggerServerEvent("ECLIPSE_CAD:RemoveInsurance", id);
            });
            RegisterNuiCallbackType("PanicOn", (data, cb) =>
            {
                var position = GetEntityCoords(PlayerPedId(), true);
                TriggerServerEvent("ECLIPSE_CAD:PanicOn", JsonConvert.SerializeObject(data), position);
            });
            RegisterNuiCallbackType("PanicOff", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:PanicOff", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("DeleteTicket", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:DeleteTicket", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("NewTicket", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:NewTicket", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("ChangePatrolStatus", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:ChangePatrolStatus", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("Crew", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:Crew", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("ChangePlayerStatus", (data, cb) =>
            {
              
                TriggerServerEvent("ECLIPSE_CAD:ChangePlayerStatus", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("AddImageToPlayer", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:AddImageToPlayer", JsonConvert.SerializeObject(data));
            });
            RegisterNuiCallbackType("SaveNewCallsign", (data, cb) =>
            {
                var id = Convert.ToString(data["medicId"]);
                id = id.Insert(3, "-");
                var list = new { id = id, callsign = Convert.ToString(data["callsign"]) };

                TriggerServerEvent("ECLIPSE_CAD:SaveNewCallsign", JsonConvert.SerializeObject(list));
            });
            RegisterNuiCallbackType("FireCop", (data, cb) =>
            {
                var id = Convert.ToString(data["medicId"]);
                id = id.Insert(3, "-");
                var list = new { id = id};

                TriggerServerEvent("ECLIPSE_CAD:FireCop", JsonConvert.SerializeObject(list));
            });
            RegisterNuiCallbackType("ChangePlayerRank", (data, cb) =>
            {
                var id = Convert.ToString(data["medicId"]);
                id = id.Insert(3, "-");
                var list = new {id = id, rank = Convert.ToString(data["rank"])};

                TriggerServerEvent("ECLIPSE_CAD:ChangePlayerRank", JsonConvert.SerializeObject(list));
            });
            RegisterNuiCallbackType("ChangeCallStatus", (data, cb) =>
            {
                TriggerServerEvent("ECLIPSE_CAD:ChangeCallStatus", JsonConvert.SerializeObject(data));
            });
        }
    }
}
