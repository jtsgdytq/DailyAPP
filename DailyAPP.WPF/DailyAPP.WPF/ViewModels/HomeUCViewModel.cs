using DaliyAPP.WPF.DTOs;
using DaliyAPP.WPF.HttpsCliens;
using DaliyAPP.WPF.Models;
using DaliyAPP.WPF.MsgEvents;
using DaliyAPP.WPF.Service;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DaliyAPP.WPF.ViewModels
{
     class HomeUCViewModel : BindableBase, INavigationAware
    {
        readonly HttpClient httpClient = new HttpClient();
        readonly DialogHostService dialogHostService;


        public HomeUCViewModel(HttpClient _httpClient, DialogHostService _dialogHostService, IRegionManager _regionManager)
        {
            httpClient = _httpClient;

            create();//统计面板数据
            Refresh();//刷新统计面板数据
            GetWaitingList(); //获取待办数据清单

            creatememo();//备忘录数据


            //添加待办事项
            ShowAddWaitCommand = new DelegateCommand(ShowAddWait);
            //更新待办事项列表
            ShowUpdateWaitStatusCommand = new DelegateCommand<WaitInfoDTO>(ShowUpdateWaitStatus);
            //编辑待办事项
            ShowEditWaitCommand = new DelegateCommand<WaitInfoDTO>(ShowEditWait);
            //首页统计面板导航
            NavigateCommand = new DelegateCommand<StacInfoModel>(Navigate);
            //添加备忘录
            ShowAddMemoCommand = new DelegateCommand(ShowAddMemo);

            //编辑备忘录
            ShowEditMemoCommand = new DelegateCommand<MemoInfoDTO>(ShowEditMemo);

            dialogHostService = _dialogHostService;

            regionManager = _regionManager;


        }
        #region 统计面板数据
        private List<StacInfoModel> _StacInfoList;

        public List<StacInfoModel> StacInfoList
        {
            get { return _StacInfoList; }
            set
            {
                _StacInfoList = value;
                RaisePropertyChanged();
            }
        }

        private void create()
        {
            StacInfoList = new List<StacInfoModel>
            {
                new StacInfoModel { Icon = "ClockFast", ItemName = "汇总", BackColor = "#FF0CA0FF", Result = "9",ViewName="WaitUC"},
                new StacInfoModel { Icon = "ClockCheckOutline", ItemName = "已完成", BackColor = "#FF1ECA3A", Result = "9",ViewName="WaitUC"},
                new StacInfoModel { Icon = "ChartLineVariant", ItemName = "比例", BackColor = "#FF02C6DC", Result = "90%" },
                new StacInfoModel { Icon = "PlayListStar", ItemName = "备忘录", BackColor = "#FFFFA000", Result = "9" , ViewName = "MemoUC"}

            };


        }
        #endregion


        #region 待办数据清单API
        private List<WaitInfoDTO> _WaitInfoList;

        public List<WaitInfoDTO> WaitInfoList
        {
            get { return _WaitInfoList; }
            set
            {
                _WaitInfoList = value;
                RaisePropertyChanged();
            }
        }

        public void GetWaitingList()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Get;
            apiRequest.Route = "Wait/GetWaitingList";

            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                WaitInfoList = JsonConvert.DeserializeObject<List<WaitInfoDTO>>(apiResponse.ResultData.ToString());
            }

            //WaitInfoList = new List<WaitInfoDTO>
            //{
            //    new WaitInfoDTO { Title = "待办清单1", Content = "1216551215" },
            //    new WaitInfoDTO { Title = "待办清单2", Content = "1216551215" }
            //};


        }

        #endregion


        #region 备忘录数据清单API
        private List<MemoInfoDTO> _MemoInfoList;

        public List<MemoInfoDTO> MemoInfoList
        {
            get { return _MemoInfoList; }
            set
            {
                _MemoInfoList = value;
                RaisePropertyChanged();
            }
        }

        public void creatememo()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Get;
            apiRequest.Route = "Memo/QueryMemo";

            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                MemoInfoList = JsonConvert.DeserializeObject<List<MemoInfoDTO>>(apiResponse.ResultData.ToString());
            }
            else
            {
                MemoInfoList= new List<MemoInfoDTO>();
            }


        }



        #endregion


        #region 登录信息显示

        private string _LoginInfo;

        public string LoginInfo
        {
            get { return _LoginInfo; }
            set
            {
                _LoginInfo = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 处理主界面传入的loginName参数
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("LoginName"))
            {
                DateTime time = DateTime.Now;
                string[] weeks = { "星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                string LoginName = navigationContext.Parameters.GetValue<string>("LoginName");

                LoginInfo = $"欢迎您，{LoginName}！今天是{time.ToString("yyyy年MM月dd日")} {weeks[(int)time.DayOfWeek]}";

            }
            // 每次返回首页时刷新数据
            Refresh();         // 刷新统计面板数据
            GetWaitingList();  // 获取待办数据清单
            creatememo();      // 获取备忘录数据

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion


        #region 统计待办数据API

        private StaWaitDTO staWaitDTO { get; set; } = new StaWaitDTO();

        private void CallStaWaitAPI()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Get;
            apiRequest.Route = "Wait/StaWait";

            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                staWaitDTO = JsonConvert.DeserializeObject<StaWaitDTO>(apiResponse.ResultData.ToString());
            }


        }
        /// <summary>
        /// 刷新统计数据
        /// </summary>
        public void Refresh()
        {
            CallStaWaitAPI();
            CallStaMemoAPI();
            if (staWaitDTO != null)
            {
                StacInfoList[0].Result = staWaitDTO.TotalCount.ToString();
                StacInfoList[1].Result = staWaitDTO.FinishCount.ToString();
                StacInfoList[2].Result = staWaitDTO.FinnishingRate;
                StacInfoList[3].Result = CallStaMemoAPI();
            }
        }


        #endregion


        #region 添加待办事项处理API
        public DelegateCommand ShowAddWaitCommand { get; set; }

        private async void ShowAddWait()
        {

            var result = await dialogHostService.ShowDialog("AddWaitUC", null);
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("NewWaitInfoDTO"))
                {
                    var waitInfo = result.Parameters.GetValue<WaitInfoDTO>("NewWaitInfoDTO");
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = Method.Post;
                    apiRequest.Route = "Wait/AddWait";
                    apiRequest.Partement = waitInfo;
                    ApiResponse apiResponse = httpClient.Execute(apiRequest);
                    if (apiResponse.ResultCode == 1)
                    {
                        //添加成功
                        // WaitInfoList.Add(waitInfo);
                        MessageBox.Show(apiResponse.Msg);
                        //Aggregator.GetEvent<MsgEvent>().Publish("添加成功");
                        //刷新统计数据
                        Refresh();
                        //刷新待办数据
                        GetWaitingList();
                    }
                    else
                    {
                        //添加失败
                        MessageBox.Show(apiResponse.Msg);

                    }
                }
            }
        }

        #endregion


        #region 更新待办事项处理API
        public DelegateCommand<WaitInfoDTO> ShowUpdateWaitStatusCommand { get; set; }
        private void ShowUpdateWaitStatus(WaitInfoDTO waitInfoDTO)
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Put;
            apiRequest.Route = "Wait/UpdateStatus";
            apiRequest.Partement = waitInfoDTO;
            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                //添加成功
                //刷新统计数据
                Refresh();
                //刷新待办数据
                GetWaitingList();
            }
            else
            {
                //添加失败
                MessageBox.Show(apiResponse.Msg);

            }
        }
        #endregion

        #region 编辑待办事项API
        public DelegateCommand<WaitInfoDTO> ShowEditWaitCommand { get; set; }

        private async void ShowEditWait(WaitInfoDTO waitInfoDTO)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.Add("OldWaitInfoDTO", waitInfoDTO);
            var result = await dialogHostService.ShowDialog("EditWaitUC", parameters);
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("NewWaitInfoDTO"))
                {
                    var waitInfo = result.Parameters.GetValue<WaitInfoDTO>("NewWaitInfoDTO");
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = Method.Put;
                    apiRequest.Route = "Wait/EditWait";
                    apiRequest.Partement = waitInfo;
                    ApiResponse apiResponse = httpClient.Execute(apiRequest);
                    if (apiResponse.ResultCode == 1)
                    {
                        //添加成功
                        // WaitInfoList.Add(waitInfo);
                        MessageBox.Show(apiResponse.Msg);
                        //Aggregator.GetEvent<MsgEvent>().Publish("添加成功");
                        //刷新统计数据
                        Refresh();
                        //刷新待办数据
                        GetWaitingList();
                    }
                    else
                    {
                        //添加失败
                        MessageBox.Show(apiResponse.Msg);

                    }
                }
            }
        }
        #endregion


        #region 首页统计面板导航
        public DelegateCommand<StacInfoModel> NavigateCommand { get; set; }
        public readonly IRegionManager regionManager;
        private void Navigate(StacInfoModel stacInfoModel)
        {
            if (!string.IsNullOrEmpty(stacInfoModel.ViewName))
            {
                if (stacInfoModel.ItemName == "已完成")
                {
                    NavigationParameters navigationParameters = new NavigationParameters();
                    navigationParameters.Add("SearchIndex", 1);
                    regionManager.Regions["MainViewRegion"].RequestNavigate(stacInfoModel.ViewName, navigationParameters);
                }
                else
                {
                    regionManager.Regions["MainViewRegion"].RequestNavigate(stacInfoModel.ViewName);
                }
            }
        }

        #endregion

        #region 统计备忘录数据API

        private string CallStaMemoAPI()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Get;
            apiRequest.Route = "Memo/StaMemo";
            string Count = null;
            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                Count = apiResponse.ResultData.ToString();
            }

            return Count;
        }
        #endregion


        #region 添加备忘录事项处理API
        public DelegateCommand ShowAddMemoCommand { get; set; }

        private async void ShowAddMemo()
        {

            var result = await dialogHostService.ShowDialog("AddMemoUC", null);
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("MemoInfoDTO"))
                {
                    var memoInfo = result.Parameters.GetValue<MemoInfoDTO>("MemoInfoDTO");
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = Method.Post;
                    apiRequest.Route = "Memo/AddMemo";
                    apiRequest.Partement = memoInfo;
                    ApiResponse apiResponse = httpClient.Execute(apiRequest);
                    if (apiResponse.ResultCode == 1)
                    {
                        //添加成功
                        // WaitInfoList.Add(waitInfo);
                        MessageBox.Show(apiResponse.Msg);
                        //Aggregator.GetEvent<MsgEvent>().Publish("添加成功");
                        //刷新统计数据
                        Refresh();
                        //刷新待办数据
                        //GetWaitingList();
                    }
                    else
                    {
                        //添加失败
                        MessageBox.Show(apiResponse.Msg);

                    }
                }
            }

        }
        #endregion

        #region 编辑备忘录事项API
        public DelegateCommand<MemoInfoDTO> ShowEditMemoCommand { get; set; }
        private async void ShowEditMemo(MemoInfoDTO memoInfoDTO)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.Add("OldMemoInfoDTO", memoInfoDTO);
            var result = await dialogHostService.ShowDialog("EditMemoUC", parameters);
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("NewMemoInfoDTO"))
                {
                    var memoInfo = result.Parameters.GetValue<MemoInfoDTO>("NewMemoInfoDTO");
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = Method.Put;
                    apiRequest.Route = "Memo/EditMemo";
                    apiRequest.Partement = memoInfo;
                    ApiResponse apiResponse = httpClient.Execute(apiRequest);
                    if (apiResponse.ResultCode == 1)
                    {
                        //添加成功
                        // WaitInfoList.Add(waitInfo);
                       // MessageBox.Show(apiResponse.Msg);
                        //Aggregator.GetEvent<MsgEvent>().Publish("添加成功");
                        //刷新统计数据
                        //Refresh();
                        //刷新备忘数据
                        creatememo();
                    }
                    else
                    {
                        //添加失败
                        MessageBox.Show(apiResponse.Msg);
                    }
                }
            }
        }

        #endregion
    }
}
