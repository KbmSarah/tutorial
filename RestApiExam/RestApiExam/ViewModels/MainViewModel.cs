using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using RestApiExam.Model;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using DevExpress.Mvvm.UI;
using System.Windows.Threading;

namespace RestApiExam.ViewModels
{

    public class MainViewModel : ViewModelBase, IDisposable
    {
        #region Members
        string baseURL = "http://172.16.32.198:5000/";
        RestClient client;
        private ObservableCollection<CmtNotify> _cmtLists;
        private CmtNotify _selectedCmtList;
        private bool _isVisibleButton;
        private bool _allSelected;
        private ICommand _cmdSelectionChanged;
        private ICommand _cmdClearSelectedList;
        private ICommand _cmdMultiSelectDelete;
        private bool IsDisposed;
        #endregion

        #region Properties
        private RegistratorViewModel _registratorViewModel;
        public RegistratorViewModel RegistratorViewModel
        {
            get { return _registratorViewModel; }
            set { base.SetValue(ref _registratorViewModel, value); }
        }
        public ObservableCollection<CmtNotify> CmtLists
        {
            get { return _cmtLists; }
            set { base.SetValue(ref _cmtLists, value); }
        }
        public CmtNotify SelectedCmtList
        {
            get { return _selectedCmtList; }
            set { base.SetValue(ref _selectedCmtList, value); }
        }
        public bool IsVisibleButton
        {
            get { return _isVisibleButton; }
            set { base.SetValue(ref _isVisibleButton, value); }
        }
        public bool AllSelected
        {
            get
            {
                return _allSelected;
            }
            set
            {
                base.SetValue(ref _allSelected, value);

                foreach (var item in CmtLists)
                {
                    item.IsSelected = value;
                }
            }
        }
        public ICommand CmdClearSelectedList
        {
            get
            {
                if (_cmdClearSelectedList == null)
                    _cmdClearSelectedList = new DelegateCommand(() => ExecCmdClearSelectedList());

                return _cmdClearSelectedList;
            }
        }
        public ICommand CmdSelectionChanged
        {
            get
            {
                if (_cmdSelectionChanged == null)
                    _cmdSelectionChanged = new DelegateCommand(ExecCmdSelectionChanged);

                return _cmdSelectionChanged;
            }
        }
        public ICommand CmdMultiSelectDelete
        {
            get
            {
                if (_cmdMultiSelectDelete == null)
                    _cmdMultiSelectDelete = new DelegateCommand(ExecCmdMultiSelectDelete, CanExecuteMultiSelectDelete);

                return _cmdMultiSelectDelete;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get Data from Rest API server & Update Datagrid with the data
        /// </summary>
        /// <returns></returns>
        public async Task RequestCommuterList()
        {
            JObject param = JObject.FromObject(new
            {
                park_id = "1",
                page = 1,
                plate_num = string.Empty,
            });

            RestRequest request = new RestRequest("client/commuter/select", Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", param, ParameterType.RequestBody);

            IRestResponse response = await client.ExecutePostAsync(request);

            JObject jObject = JObject.Parse(response.Content);

            JToken jToken = jObject["cmt_list"];
            if (jToken != null)
            {
                var jsonConvertList = JsonConvert.DeserializeObject<List<CmtList>>(jToken.ToString());

                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.CmtLists.Clear();

                    foreach (var item in jsonConvertList)
                    {
                        CmtNotify cmtNotify = new CmtNotify(item);
                        CmtLists.Add(cmtNotify);
                    }
                });
                
            }
        }

        /// <summary>
        /// Request Task for data modification to Rest API server 
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        public async Task UpdateCmtList(JObject jsonParam)
        {
            RestRequest request = new RestRequest("client/commuter/update", Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", jsonParam, ParameterType.RequestBody);

            IRestResponse response = await client.ExecutePostAsync(request);

            JObject jObject = JObject.Parse(response.Content);

            var result = jObject["result_msg"];

            if (result == null)
            {
                MessageBox.Show("Request Fail", "Response is null", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Console.WriteLine(result);

            if (jObject["result_msg"].ToString() != "success")
            {
                MessageBox.Show("Request Fail", "Request result", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await Task.Factory.StartNew(() => RequestCommuterList());
            ExecCmdClearSelectedList();
        }

        /// <summary>
        /// SelectionChanged Event Handler
        /// </summary>
        private void ExecCmdSelectionChanged()
        {
            this.RegistratorViewModel.SetList(SelectedCmtList);
        }

        /// <summary>
        /// Method which Execute Command that clear contents in textbox display
        /// </summary>
        private void ExecCmdClearSelectedList()
        {
            CmtNotify ClearSelectedItem = new CmtNotify()
            {
                cmt_id = string.Empty,
                cmt_code = string.Empty,
                start_date = string.Empty,
                end_date = string.Empty,
                plate_num = string.Empty,
                user_name = string.Empty,
                phone = string.Empty,
                fee = null,
                memo = string.Empty
            };
            RegistratorViewModel.SetList(ClearSelectedItem);
            SelectedCmtList = null;
        }

        /// <summary>
        /// Method which Request for Data modification to RestApi server
        /// </summary>
        /// <param name="command"></param>
        private void RestApiModifyRequest(string command)
        {
            SelectedCmtList = this.RegistratorViewModel.GetList();

            JObject jsonParam = JObject.FromObject(new
            {
                park_id = "1",
                dml = command,
                cmt_id = SelectedCmtList.cmt_id,
                cmt_code = SelectedCmtList.cmt_code,
                start_date = SelectedCmtList.start_date,
                end_date = SelectedCmtList.end_date,
                plate_num = SelectedCmtList.plate_num,
                user_name = SelectedCmtList.user_name,
                phone = SelectedCmtList.phone,
                fee = SelectedCmtList.fee,
                memo = SelectedCmtList.memo
            });

            Task.Factory.StartNew(() => UpdateCmtList(jsonParam));
        }

        /// <summary>
        /// Command which Delete Multi Selected Items 
        /// </summary>
        private void ExecCmdMultiSelectDelete()
        {
            foreach (var item in CmtLists)
            {
                if(item.IsSelected)
                {
                    JObject jsonParam = JObject.FromObject(new
                    {
                        park_id = "1",
                        dml = "delete",
                        cmt_id = item.cmt_id,
                        cmt_code = item.cmt_code,
                        start_date = item.start_date,
                        end_date = item.end_date,
                        plate_num = item.plate_num,
                        user_name = item.user_name,
                        phone = item.phone,
                        fee = item.fee,
                        memo = item.memo
                    });

                    // 서버에 다중삭제 요청하면 에러날 수 있어서 실제 삭제 Task 돌리는 대신 출력만...
                    Console.WriteLine(jsonParam.ToString());
                    // Task.Factory.StartNew(() => UpdateCmtList(jsonParam));
                }
            }

        }

        /// <summary>
        /// At least if one item is selected, then multi delete button will be abled.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanExecuteMultiSelectDelete()
        {
            foreach (var item in CmtLists)
            {
                if (item.IsSelected == true)
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                this.IsDisposed = true;

                if (CmtLists != null && CmtLists.Count != 0)
                {
                    foreach (var item in CmtLists)
                    {
                        item.Dispose();
                    }

                    CmtLists.Clear();
                    CmtLists = null;
                }

                RegistratorViewModel.ButtonClickedEvent -= RestApiModifyRequest;
                RegistratorViewModel?.Dispose();

                //CmtLists를 참조하기 때문에 이미 해제됨
                // SelectedCmtList?.Dispose(); 
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            this.RegistratorViewModel = new RegistratorViewModel();
            RegistratorViewModel.ButtonClickedEvent += RestApiModifyRequest;

            client = new RestClient(baseURL);
            SelectedCmtList = new CmtNotify();
            CmtLists = new ObservableCollection<CmtNotify>();

            Task.Factory.StartNew(() => RequestCommuterList());
        }
        #endregion

    }

    /// <summary>
    /// For DataGrid Visibility Binding
    /// </summary>
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object),
            typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}