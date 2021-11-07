using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace ServiceLocatorPath
{
    public class PlayFabCustom : IPlayFabCustom
    {
        private SystemInfoCustom systemInfoCustom;
        private IsCreated isCreatedPlayer;
        private string _playerId;
        private List<Personaje> inventaryPersonajes;
        private bool resultPlayfab;

        public PlayFabCustom()
        {
            inventaryPersonajes = new List<Personaje>();
            Login(OnLoginSuccess, OnLoginFailure);
        }

        private void Login(Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback)
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
                /*
                Please change the titleId below to your own titleId from PlayFab Game Manager.
                If you have already set the value in the Editor Extensions, this can be skipped.
                */
                PlayFabSettings.staticSettings.TitleId = "42";
            }
            Debug.Log($"SystemInfo.deviceUniqueIdentifier {SystemInfo.deviceUniqueIdentifier}");
            var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true};
            PlayFabClientAPI.LoginWithCustomID(request, resultCallback, errorCallback);
        }

        private void CreatedPlayer()
        {
        
            GetTitleDataRequest request = new GetTitleDataRequest()
            {
                Keys = new List<string>(){"InitialUserData"}
            };
            PlayFabClientAPI.GetTitleData(request, (defaultData) =>
            {
                var initialUserData = JsonUtility.FromJson<InitialUserData>(defaultData.Data["InitialUserData"]);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string>()
                    {
                        {"isCreated",JsonUtility.ToJson(new IsCreated(){isCreated = true})},
                        {"SystemInfo",JsonUtility.ToJson(new SystemInfoCustom()
                        {
                            model = SystemInfo.deviceModel,
                            name = SystemInfo.deviceName,
                            os = SystemInfo.operatingSystem,
                            processor = SystemInfo.processorType,
                            graphicsDeviceName = SystemInfo.graphicsDeviceName
                        })}
                    }
                }, requestCreate =>
                {
                    AddUserVirtualCurrencyRequest reqCurrenci = new AddUserVirtualCurrencyRequest()
                    {
                        Amount = 0,
                        VirtualCurrency = "CL"
                    };
                    PlayFabClientAPI.AddUserVirtualCurrency(reqCurrenci, result =>{},OnLoginFailure);
                },OnLoginFailure);
            },OnLoginFailure);
        }

        private void OnLoginFailure(PlayFabError error)
        {
            Debug.LogWarning("Something went wrong with your first API call.  :(");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
        }

        private void OnLoginSuccess(LoginResult result)
        {
            GetUserDataRequest requestCreated = new GetUserDataRequest(){Keys = new List<string>(){"isCreated","SystemInfo"}};
            _playerId = result.PlayFabId;
            PlayFabClientAPI.GetUserData(requestCreated, defaultResult =>
            {
                if (!defaultResult.Data.ContainsKey("isCreated"))
                {
                    CreatedPlayer();
                }
                else
                {
                    isCreatedPlayer = JsonUtility.FromJson<IsCreated>(defaultResult.Data["isCreated"].Value);
                    systemInfoCustom = JsonUtility.FromJson<SystemInfoCustom>(defaultResult.Data["SystemInfo"].Value);
                }

                GetInventory().WrapErrors();
            },OnLoginFailure);
        }
        private async Task GetInventory()
        {
            //Debug.Log("get inventory");
            resultPlayfab = false;
            GetCatalogItemsResult itemsResultLocal = null;
            var catalog = new GetCatalogItemsRequest()
            {
                CatalogVersion = "Personajes"
            };
            PlayFabClientAPI.GetCatalogItems(catalog, itemsResult =>
            {
                Personaje extra = null;
                //Debug.Log($"resuylts {itemsResult.Catalog.Count}");
                foreach (var item in itemsResult.Catalog)
                {
                    inventaryPersonajes.Add(JsonUtility.FromJson<Personaje>(item.CustomData));
                    Debug.Log(item.CustomData);
                }
                resultPlayfab = true;
            }, error =>
            {
                OnLoginFailure(error);
                resultPlayfab = true;
            });
            while (!resultPlayfab)
            {
                await Task.Delay(TimeSpan.FromSeconds(.3f));
            }
        }

        public bool IsAllCompleted()
        {
            return resultPlayfab;
        }

        public List<Personaje> GetPjs()
        {
            return inventaryPersonajes;
        }
    }
}