using Microsoft.WindowsAzure.MediaServices.Client;
using Sample_AzureMediaPlayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace Sample_AzureMediaPlayer.Azure
{
    public class AzureMediaServices
    {

        // Field for service context.
        private static CloudMediaContext _context = null;
        private static MediaServicesCredentials _cachedCredentials = null;

        /// <summary>
        /// コンストラクタ
        /// 接続文字列の生成を実施
        /// </summary>
        public AzureMediaServices()
        {
            string _mediaServicesAccountName = ConfigurationManager.AppSettings["MediaServicesAccountName"];
            string _mediaServicesAccountKey = ConfigurationManager.AppSettings["MediaServicesAccountKey"];

            _cachedCredentials = new MediaServicesCredentials(
                _mediaServicesAccountName,
                _mediaServicesAccountKey);
            _context = new CloudMediaContext(_cachedCredentials);
        }

        /// <summary>
        /// アセット情報をリストで取得する。
        /// </summary>
        /// <returns></returns>
        public List<amsModel> ListAssets()
        {
            List<amsModel> amsList = new List<amsModel>();
            
            foreach (IAsset asset in _context.Assets)
            {
                amsModel ams = new amsModel();

                ams.LocatorUri = BuildStreamingURLs(asset);

                //　Locatorがなければ表示対象としない
                if (ams.LocatorUri != null)
                {
                    ams.Name = asset.Name;
                    ams.AssetId = asset.Id;
                    ams.RegistDate = asset.Created.ToString("yyyy/MM/dd HH:mm:ss");
                    ams.UpdateDate = asset.LastModified.ToString("yyyy/MM/dd HH:mm:ss");

                    long ToatlSize = 0;

                    foreach (IAssetFile fileItem in asset.AssetFiles)
                    {
                        ToatlSize += fileItem.ContentFileSize;

                        if (fileItem.Name.Contains("png"))
                        {
                            foreach (var item in fileItem.Asset.Locators)
                            {
                                ams.ThumbNailUri = item.Path + fileItem.Name;
                            }
                        }

                    }

                    ams.TotalSize = ToatlSize;
                    amsList.Add(ams);
                }
            }
            return amsList;
        }

        /// <summary>
        /// ストリーミングエンドポイントの設定
        /// マニフェストが存在しない場合はNULLを返す
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>ストリーミングエンドポイント or NULL</returns>
        private static string BuildStreamingURLs(IAsset asset)
        {
            string path = String.Empty;
            foreach (var item in _context.Locators)
            {
                if (item.AssetId == asset.Id)
                {
                    path = item.Path;
                }
            }
            var manifestFile = asset.AssetFiles.Where(f => f.Name.ToLower().
                                        EndsWith(".ism")).
                                        FirstOrDefault();
            return (manifestFile!=null) ? (path + manifestFile.Name + "/manifest").Replace("http:","") : null ;
        }
    }
}