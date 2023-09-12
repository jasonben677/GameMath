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

public class Loader : MonoBehaviour
{
    public AssetReference circlePrefab;

    public RawImage rawImage;

    private void Start()
    {
        this._LoadImg();

        //this._InstantiateCircle();


        StartCoroutine(this._CheckUpdate());
    }


    private async void _LoadImg()
    {
        Texture2D texture2D = await Addressables.LoadAssetAsync<Texture2D>("Assets/AddressableProject/Sprite/08.jpg").Task;

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

        AsyncOperationHandle<IResourceLocator> initHandle = Addressables.InitializeAsync(false);
        yield return initHandle;

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

            var updateHandle = Addressables.UpdateCatalogs(checkHandle.Result, false);
            yield return updateHandle;

            if (updateHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning("更新失敗");
                yield break;
            }

            List<IResourceLocator> locators = updateHandle.Result;

            foreach (var locator in locators)
            {
                List<object> keys = new List<object>();
                keys.AddRange(locator.Keys);

                var downloadHandle = Addressables.DownloadDependenciesAsync(keys, false);

                while (!downloadHandle.IsDone)
                {
                    if (downloadHandle.Status == AsyncOperationStatus.Failed)
                    {
                        Debug.LogWarning("下載失敗");
                        yield break;
                    }

                    float percentage = downloadHandle.PercentComplete;

                    Debug.LogWarning("已下載 : " + percentage);

                    yield return null;
                }

                if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.LogWarning("下載完畢");

                }

            }

        }
        else
        {
            Debug.LogWarning("沒有更新");
        }

    }

}
