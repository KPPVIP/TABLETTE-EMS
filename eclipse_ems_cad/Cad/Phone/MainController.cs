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
    public class MainController : BaseController
    {
        private int currentTemperature = 0;
        private Random rnd = new Random();

        private int blipRobbery = 0;

        private int gameToggle = 0;
        private int openCad = 0;
        private bool cadActive = false;
        private bool controlActive = false;
        private string playerJob = "unmep";

        public bool tablet = false;
        public string tabletDict = "amb@code_human_in_bus_passenger_idles@female@tablet@base";
        public string tabletAnim = "base";
        public string tabletProp = "prop_cs_tablet";
        public int tabletBone = 60309;
        public Vector3 tabletOffset = new Vector3(0.03f, 0.002f, -0.0f);
        public Vector3 tabletRot = new Vector3(10.0f, 160.0f, 0.0f);
        public MainController()
        {

            EventHandlers["ECLIPSE:UpdatePlayerData"] += new Action<string, string, string, string, string>(UpdatePlayerData);
            EventHandlers["ECLIPSE:LoadVehiclesData"] += new Action(LoadVehiclesData);
            EventHandlers["ECLIPSE:LoadAmbulanceCalls"] += new Action(LoadAmbulanceCalls);
            EventHandlers["ECLIPSE:LoadCitizensData"] += new Action(LoadCitizensData);
            EventHandlers["ECLIPSE:LoadTicketsData"] += new Action(LoadTicketsData);
            EventHandlers["ECLIPSE:LoadVehiclesStatus"] += new Action(LoadVehiclesStatus);  
            EventHandlers["ECLIPSE:LoadCitizensStatus"] += new Action(LoadCitizensStatus);
            EventHandlers["ECLIPSE:GetVehicleData"] += new Action<string>(GetVehicleData);
            EventHandlers["ECLIPSE:GetVehiclesStatus"] += new Action<string>(GetVehiclesStatus);
            EventHandlers["ECLIPSE:GetCitizensStatus"] += new Action<string>(GetCitizensStatus);
            EventHandlers["ECLIPSE:GetTickets"] += new Action<string>(GetTickets);
            EventHandlers["ECLIPSE:LoadCitizens"] += new Action<string>(LoadCitizens);
            EventHandlers["ECLIPSE:GetAmbulanceCalls"] += new Action<string>(GetAmbulanceCalls);
            EventHandlers["ECLIPSE:GetAmbulanceOnDuty"] += new Action<string>(GetAmbulanceOnDuty);
            EventHandlers["ECLIPSE:GetActiveAmbulance"] += new Action(GetActiveAmbulance);
            EventHandlers["ECLIPSE:SetWayPoint"] += new Action<string>(SetWayPoint);
            EventHandlers["ECLIPSE:SetBlip"] += new Action<Vector3>(SetBlip);
            EventHandlers["ECLIPSE:KillBlip"] += new Action(KillBlip);
            EventHandlers["ECLIPSE:KeyBind"] += new Action(KeyBind);
            RegisterCommand("get", new Action(Get), false);
            RegisterCommand("cursor", new Action(Cursor), false);


            RegisterNuiCallbackType("CloseCad", (data, cb) =>
            {
                if (cadActive)
                {
                    SetNuiFocus(false, false);
                    SetNuiFocusKeepInput(false);
                    SendMessageToCEF("CloseMedicalCad");
                    cadActive = false;
                    ToggleTabbet(false);
                }
            });

            RegisterNuiCallbackType("Tilda", (data, cb) =>
            {
                controlActive = true;
                SetNuiFocus(true, true);
                SetNuiFocusKeepInput(true);
            });

            Tick += OnTick;
        }

        public void KillBlip()
        {
            RemoveBlip(ref blipRobbery);
        }
        public void SetBlip(Vector3 position)
        {
            //var position = JsonConvert.DeserializeObject<PositionModel>(positionData);
            Debug.WriteLine($"pos: {position.X}, {position.Y}, {position.Z}");
            blipRobbery = AddBlipForCoord(position.X, position.Y, position.Z);

            SetBlipSprite(blipRobbery, 161);
            SetBlipScale(blipRobbery, 2.0f);
            SetBlipColour(blipRobbery, 3);
            PulseBlip(blipRobbery);
        }
        public void KeyBind()
        {
            var json = LoadResourceFile($"{GetCurrentResourceName()}", "config.json");
            var keyBinds = JsonConvert.DeserializeObject<KeysModel>(json);
            openCad = keyBinds.OpenCad;
            gameToggle = keyBinds.GameToggle;
        }
        public float GetDistanceToSquared2D(Vector3 pos1, Vector3 pos2)
        {
            return (float)Math.Sqrt(pos1.DistanceToSquared2D(pos2));
        }
        public async Task OnTick()
        {

            if (IsControlJustPressed(0, openCad))
            {
                if (playerJob == "ambulance")
                {
                    if (!cadActive)
                    {
                        cadActive = true;
                        SetNuiFocus(true, true);
                        SendMessageToCEF("OpenMedicalCad");
                        Get();
                        ToggleTabbet(true);
                    }
                }
            }

            if (IsControlJustPressed(0, gameToggle))
            {
                if(controlActive)
                {
                    controlActive = false;
                    SetNuiFocus(false, false);
                    SetNuiFocusKeepInput(false);
                }
            }

            if (currentBlip != null)
            {
                if (GetDistanceToSquared2D(GetEntityCoords(GetPlayerPed(-1), true), currentBlipPosition) < 15f)
                {
                    SetBlipRoute(currentBlip.Handle, false);
                    SetBlipDisplay(currentBlip.Handle, 0);
                }
            }
        }

        public static Blip currentBlip;
        public static Vector3 currentBlipPosition;

        public void SetWayPoint(string position)
        {
            var waypointPosition = JsonConvert.DeserializeObject<RouteModel>(position);
            if (currentBlip != null)
            {
                SetBlipRoute(currentBlip.Handle, false);
                SetBlipDisplay(currentBlip.Handle, 0);
            }
            currentBlip = World.CreateBlip(new Vector3(waypointPosition.x, waypointPosition.y, 0));
            currentBlipPosition = new Vector3(waypointPosition.x, waypointPosition.y, 0);
            currentBlip.Sprite = (BlipSprite)1;
            currentBlip.Color = BlipColor.Blue;
            currentBlip.ShowRoute = true;
            currentBlip.Scale = 1.0f;
            SetBlipDisplay(currentBlip.Handle, 2);
            SetBlipAsShortRange(currentBlip.Handle, false);
        }
        public void UpdatePlayerData(string phone, string job, string name, string jobGrade, string callsign)
        {
            playerJob = job;
            //if(playerJob == "offAmbulance")
            //{
            //    playerJob = "Ambulance";
            //}
            Debug.WriteLine($"{phone}, {playerJob}, {name}, {phone.Replace("-", "")}, {jobGrade}, {callsign}");
            var model = new { type = "UpdatePlayerData", phone = phone, job = playerJob, name = name, medicId = phone.Replace("-", ""), rank = jobGrade, callsign = callsign };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
        }

        public void GetAmbulanceOnDuty(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<List<UserESXDataModel>>(data);
            var list = new List<AmbulanceOnDutyModel>();
            int count = 1;
            foreach (var userData in jsonData)
            {
                list.Add(new AmbulanceOnDutyModel { adam = userData.adamMedic, callsign = userData.callsignMedic, onPanic = userData.onPanicMedic, onDuty = userData.onDutyMedic, rank = userData.job_grade, name = userData.firstname + " " + userData.lastname, medicId = Convert.ToInt32(userData.phone_number.Replace("-", ""))});
            }
            var model = new { type = "GetAmbulanceOnDuty", data = JsonConvert.SerializeObject(list) };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
        }
        public void Cursor()
        {
            StartRecording(1);
            //SetNuiFocus(false, false);
        }
        public void GetTickets(string data)
        {
            var model = new { type = "GetTickets", data = data };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
        }
        public void GetAmbulanceCalls(string data)
        {
            Debug.WriteLine($"{data}");
            var callsList = new List<AmbulanceCallsModel>();
            var jsonData = JsonConvert.DeserializeObject<List<AmbulanceCallsModel>>(data);
            //foreach (var call in jsonData)
            //{
            //    var crew = JsonConvert.DeserializeObject<List<string>>(call.crew);
            //    call.crew = crew;
            //}

            var model = new { type = "GetAmbulanceCalls", data = JsonConvert.SerializeObject(jsonData) };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
        }
        public void GetCitizensStatus(string data)
        {
            var model = new { type = "CitizensStatus", data = data };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
        }
        public void GetVehiclesStatus(string data)
        {
            var model = new { type = "VehiclesStatus", data = data };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
        }
        public void LoadCitizens(string data)
        {
            Debug.WriteLine($"data: {data}");
            var jsonESXdata = JsonConvert.DeserializeObject<List<UserESXDataModel>>(data);
            var userList = new List<UserDataModel>();
            var id = 1;
            foreach(var user in jsonESXdata)
            {
                if(user.sex == "m")
                {
                    user.sex = "male";
                }
                else
                {
                    user.sex = "female";
                }
                userList.Add(new UserDataModel { name = user.firstname + " " + user.lastname, insurance = user.insurance, picture = user.picture, age = user.dateofbirth, registered = user.dateofbirth, balance = user.bank.ToString(), gender = user.sex, id = id++, isActive = true, phone = user.phone_number, status = 1,});
            }
            var model = new { type = "UserData", data = JsonConvert.SerializeObject(userList) };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
            LoadCitizensStatus();
        }
        public void GetVehicleData(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<List<VehicleDataModel>>(data);
            var carId = 0;
            var vehiclieList = new List<VehicleCadModel>();
            foreach(var car in jsonData)
            {
                var carInfo = JsonConvert.DeserializeObject<VehicleModel>(car.vehicle);
                var carName = GetDisplayNameFromVehicleModel((uint)carInfo.model);
                var actuallyCarName = GetLabelText(carName);
                var vehType = GetVehicleClassFromName((uint)carInfo.model);
                var actuallyVehType = GetVehicleType(vehType);
                vehiclieList.Add(new VehicleCadModel { carId = carId++, color = "undefined", name = carName, number = car.plate, ownerId = car.owner, sellerId = 0, status = 1, type = actuallyVehType });

            }
            var model = new { type = "VehicleData", data = JsonConvert.SerializeObject(vehiclieList) };
            var nuiData = JsonConvert.SerializeObject(model);
            SendNuiMessage(nuiData);
        }
        public async void Get()
        {
            LoadVehiclesData();
            LoadCitizensData();
            LoadAmbulanceCalls();
            GetActiveAmbulance();
            LoadPlayerData();

        }
        public async void LoadCitizensData()
        {
            TriggerServerEvent("ECLIPSE_CAD:GetCitizens");
        }

        public void LoadPlayerData()
        {
            TriggerServerEvent("ECLIPSE_CAD:LoadPlayerData");
        }
        public async void LoadTicketsData()
        {
            TriggerServerEvent("ECLIPSE_CAD:GetTickets");
        }
        public void GetActiveAmbulance()
        {
            TriggerServerEvent("ECLIPSE_CAD:GetActiveAmbulance");
        }
        public async void LoadAmbulanceCalls()
        {
            TriggerServerEvent("ECLIPSE_CAD:GetAmbulanceCalls");
        }
        public async void LoadVehiclesData()
        {
            TriggerServerEvent("ECLIPSE_CAD:LoadVehiclesData");
            
        }
        public async void LoadCitizensStatus()
        {
            TriggerServerEvent("ECLIPSE_CAD:GetCitizensStatus");
        }
        public async void LoadVehiclesStatus()
        {
            TriggerServerEvent("ECLIPSE_CAD:GetVehiclesStatus");
        }

        public async void ToggleTabbet(bool toogle)
        {
            if (toogle)
            {
                tablet = true;
                RequestAnimDict(tabletDict);
                while (!HasAnimDictLoaded(tabletDict))
                {
                    await Delay(150);
                }

                RequestModel((uint)GetHashKey(tabletProp));

                while (!HasModelLoaded((uint) GetHashKey(tabletProp)))
                {
                    await Delay(150);
                }

                var playerPed = PlayerPedId();
                var tableObj = CreateObject(GetHashKey(tabletProp), 0.0f, 0.0f, 0.0f, true, true, false);
                var tabletBoneIndex = GetPedBoneIndex(playerPed, tabletBone);

                AttachEntityToEntity(tableObj, playerPed, tabletBoneIndex, tabletOffset.X, tabletOffset.Y,
                    tabletOffset.Z, tabletRot.X, tabletRot.Y, tabletRot.Z, true, false, false, false, 2, true);
                SetModelAsNoLongerNeeded((uint) GetHashKey(tabletProp));

                while (tablet)
                {
                    await Delay(100);
                    playerPed = PlayerPedId();

                    if (!IsEntityPlayingAnim(playerPed, tabletDict, tabletAnim, 3))
                    {
                        TaskPlayAnim(playerPed, tabletDict, tabletAnim, 3.0f, 3.0f, -1, 49, 0, false, false, false);
                    }
                }

                ClearPedSecondaryTask(playerPed);

                await Delay(450);

                DetachEntity(tableObj, true, false);
                DeleteEntity(ref tableObj);
            }
            else
            {
                tablet = false;
            }
        }
    }
}
