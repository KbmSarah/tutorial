using System;
using DevExpress.Mvvm;
using JsonExam.Model;
using System.Collections.ObjectModel;
using System.IO;
using JsonExam.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonExam.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            string jsonFilePath = Directory.GetCurrentDirectory() + "\\test.json";
            string json = CommonFunction.ReadJsonString(jsonFilePath);

            JObject jObject = JObject.Parse(json);
            JToken jToken = jObject["list"];
            List<InOutModel> modelList = JsonConvert.DeserializeObject<List<InOutModel>>(jToken.ToString());
            Members = new ObservableCollection<InOutModel>(modelList);
        }

        private ObservableCollection<InOutModel> _members;

        public ObservableCollection<InOutModel> Members
        {
            get { return _members; }
            set { base.SetValue(ref _members, value); }
        }
    }
}