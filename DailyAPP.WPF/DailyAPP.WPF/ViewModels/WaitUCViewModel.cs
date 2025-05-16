using DaliyAPP.WPF.DTOs;
using DaliyAPP.WPF.HttpsCliens;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DaliyAPP.WPF.ViewModels
{
    class WaitUCViewModel:BindableBase,INavigationAware
    {
        private List<WaitInfoDTO> _WaitInfoList;

        private HttpClient httpClient;

        public List<WaitInfoDTO> WaitInfoList
        {
            get { return _WaitInfoList; }
            set { 
                _WaitInfoList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_HttpClient"></param>

        public WaitUCViewModel(HttpClient _HttpClient)
        {
            httpClient = _HttpClient;
            

            QueryCommand = new DelegateCommand(QureyWaitList);//查询待办清单
            ShowAddWaitCommand = new DelegateCommand(() =>
            {
                WaitInfoDTO = new WaitInfoDTO();
                RaisePropertyChanged(nameof(WaitInfoDTO));//初始化弹框
                IsShowAddWait = !IsShowAddWait;
            });
            //添加命令
            AddWaitCommand = new DelegateCommand(AddWait);
            //删除
            DelectCommand = new DelegateCommand<WaitInfoDTO>(Delect);
        }

        #region 查询待办清单

        public string SearchTitle { get; set; } = string.Empty;

        private int _SearchIndex;

        public int SearchIndex
        {
            get { return _SearchIndex; }
            set { 
                _SearchIndex = value;
                RaisePropertyChanged();
            }
        }

        //public int SearchIndex { get; set; } = 0;
        

        public DelegateCommand QueryCommand { get; set; }

        string?  Status=null;



        public void QureyWaitList()
        {
            // 根据选择的索引设置状态过滤
            int? status = null;
            if (SearchIndex == 1) status = 1;    // 完成
            if (SearchIndex == 2) status = 0;    // 待办

            // 构建请求地址
            string route = $"Wait/Query?Title={SearchTitle}";
            if (status != null)
                route += $"&Status={status}";

            // 发起请求
            var request = new ApiRequest
            {
                Method = Method.Get,
                Route = route
            };

            var response = httpClient.Execute(request);

            // 处理结果
            if (response.ResultCode == 1)
            {
                WaitInfoList = JsonConvert.DeserializeObject<List<WaitInfoDTO>>(response.ResultData.ToString());
                // 设置查询结果的可见性
                Visibility = (WaitInfoList.Count > 0) ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                WaitInfoList = new List<WaitInfoDTO>();
            }
        }

        /// <summary>
        /// 展示查询无结果
        /// </summary>
        private Visibility _Visibility;

        public Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                _Visibility = value;
                RaisePropertyChanged();
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if(navigationContext.Parameters.ContainsKey("SearchIndex"))
            {
                SearchIndex = navigationContext.Parameters.GetValue<int>("SearchIndex");
            }
            else
            {
                SearchIndex = 0;
            }
            QureyWaitList();//初始化查询待办清单
        }

        

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            

        }


        #endregion


        #region 添加待办清单展示
        private bool _IsShowAddWait;

        public bool IsShowAddWait
        {
            get { return _IsShowAddWait; }
            set { _IsShowAddWait = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand ShowAddWaitCommand { get; set; }

        #endregion

        #region 添加待办
        public WaitInfoDTO WaitInfoDTO { get; set; }= new WaitInfoDTO();
        public DelegateCommand AddWaitCommand { get; set; }
        private void AddWait()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Post;
            apiRequest.Route = "Wait/AddWait";
            apiRequest.Partement = WaitInfoDTO;
            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                
                MessageBox.Show(apiResponse.Msg);
                QureyWaitList();
                IsShowAddWait = false;
            }
        }

        #endregion
        public DelegateCommand<WaitInfoDTO> DelectCommand { get; set; }

        private void Delect(WaitInfoDTO waitInfoDTO )
        {
            if (waitInfoDTO != null)
            {
                var result = MessageBox.Show("您确定要删除这条备忘录吗？删除后将无法恢复哦～",
                                      "温馨提示",
                                      MessageBoxButton.OKCancel,
                                      MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = Method.Delete;
                    apiRequest.Route = "Wait/DeleteWait";
                    apiRequest.Partement = waitInfoDTO;
                    ApiResponse apiResponse = httpClient.Execute(apiRequest);
                    if (apiResponse.ResultCode == 1)
                    {
                        MessageBox.Show(apiResponse.Msg);
                        //刷新
                        //createMemo();
                        QureyWaitList();
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                }
            }
        }
    }
}
