using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.Http;
using System.IO;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;
using System.Linq;

public class Loader : MonoBehaviour
{
    public AssetReference circlePrefab;

    public RawImage rawImage;

    private void Start()
    {
        this._LoadImg();

        //this._InstantiateCircle();

        //StartCoroutine(this._CheckUpdate());
    }


    private async void _LoadImg()
    {
        Texture2D texture2D = await Addressables.LoadAssetAsync<Texture2D>("Assets/AddressableProject/Sprite/10.jpg").Task;

        if (texture2D != null)
        {
            rawImage.texture = texture2D;
            rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(texture2D.width, texture2D.height);
        }
        else Debug.LogWarning("load fail");

    }

    private async void _InstantiateCircle()
    {
        //GameObject pref = await this.circlePrefab.LoadAssetAsync<GameObject>().Task;

        //GameObject circleObj = Instantiate(pref, this.transform);


        GameObject pref = await Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/Circle.prefab").Task;

        GameObject circleObj = Instantiate(pref, this.transform);

    }



    private IEnumerator _CheckUpdate()
    {
        string oldCatalogPath = Application.persistentDataPath + "/com.unity.addressables/catalog_2023.09.12.10.06.22.hash";

        AsyncOperationHandle<IResourceLocator> initHandle = Addressables.InitializeAsync(false);
        yield return initHandle;

        if (initHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning("初始化失敗");
            yield break;
        }

        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        if (checkHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("load Error");
            yield break;
        }

        if (checkHandle.Result.Count > 0)
        {
            Debug.LogWarning("有更新");

            // 舊的catalog
            var previousCatalog = Addressables.ResourceLocators.ElementAt(0);

            var updateHandle = Addressables.UpdateCatalogs(checkHandle.Result, false);
            yield return updateHandle;

            if (updateHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning("更新失敗");
                yield break;
            }

            // 更新後的catalog
            var updateCatalog = Addressables.ResourceLocators.ElementAt(0);

            // 有異動的資源表
            var addKeys = updateCatalog.Keys.Except(previousCatalog.Keys);

            //yield return this._ShowAllDownloadName(addKeys.ToList());
        }
        else
        {
            Debug.LogWarning("沒有更新");
        }

    }


    private IEnumerator _ShowAllDownloadName(List<object> keys)
    {
        foreach (var key in keys)
        {
            Debug.LogWarning("更新 : "+ key);

            var downloadHandle = Addressables.DownloadDependenciesAsync(key, false);

            while (!downloadHandle.IsDone)
            {
                if (downloadHandle.Status == AsyncOperationStatus.Failed)
                {
                    //Debug.LogWarning("下載失敗");
                    yield break;
                }

                float percentage = downloadHandle.PercentComplete;

                //Debug.LogWarning("已下載 : " + percentage);

                yield return null;
            }

            if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning("下載完畢 : " + key.ToString());

            }
        }
    }

    private IEnumerator _ShowAllDownloadNameToEnumerable(List<object> keys)
    {
        IEnumerable ss = keys;

        var downloadHandle = Addressables.DownloadDependenciesAsync(ss, Addressables.MergeMode.Union, false);

        while (!downloadHandle.IsDone)
        {
            if (downloadHandle.Status == AsyncOperationStatus.Failed)
            {
                //Debug.LogWarning("下載失敗");
                yield break;
            }

            float percentage = downloadHandle.PercentComplete;

            //Debug.LogWarning("已下載 : " + percentage);

            yield return null;
        }

        if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning("下載完畢");

        }
    }

    private IEnumerator _ClearAllAssetCoro()
    {
        foreach (var locats in Addressables.ResourceLocators)
        {
            var async = Addressables.ClearDependencyCacheAsync(locats.Keys, false);
            yield return async;
            Addressables.Release(async);
        }
        Caching.ClearCache();
    }

}
