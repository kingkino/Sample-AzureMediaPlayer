using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sample_AzureMediaPlayer.Models
{
    public class amsModel
    {
        [DisplayName("ID")]
        public string AssetId {get;set;}
        [DisplayName("名称")]
        public string Name { get; set; }
        [DisplayName("サムネイル")]
        public string ThumbNailUri { get; set; }
        [DisplayName("再生URL")]
        public string LocatorUri { get; set; }
        [DisplayName("容量")]
        public long TotalSize { get; set; }
        [DisplayName("作成日付")]
        public string RegistDate { get; set; }
        [DisplayName("更新日付")]
        public string UpdateDate { get; set; }
    }
}