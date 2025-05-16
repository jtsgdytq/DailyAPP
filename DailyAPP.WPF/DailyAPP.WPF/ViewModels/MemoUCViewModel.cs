using DaliyAPP.WPF.DTOs;
using DaliyAPP.WPF.HttpsCliens;
using MaterialDesignColors;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DaliyAPP.WPF.ViewModels
{
    class MemoUCViewModel : BindableBase
    {
        private List<MemoInfoDTO> _MemoInfoList;

        private readonly HttpClient httpClient;

        public List<MemoInfoDTO> MemoInfoList
        {
            get { return _MemoInfoList; }
            set
            {
                _MemoInfoList = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 待办清单查询
        /// </summary>
        public void createMemo()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Get;
            apiRequest.Route = "Memo/QueryMemo";

            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                MemoInfoList = JsonConvert.DeserializeObject<List<MemoInfoDTO>>(apiResponse.ResultData.ToString());
                Visibility = (MemoInfoList.Count > 0) ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                MemoInfoList = new List<MemoInfoDTO>();
            }


        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_httpClient"></param>
        public MemoUCViewModel(HttpClient _httpClient)
        {
            httpClient = _httpClient;
            createMemo();


            ShowAddMemoCommand = new DelegateCommand(() =>
            {
                memoInfoDTO = new MemoInfoDTO();
                RaisePropertyChanged(nameof(memoInfoDTO));//初始化添加内容

                IsShowAddMemo = !IsShowAddMemo;
            });
            //搜索待办清单
            SearchCommand = new DelegateCommand<string>(Search);
            //添加待办清单
            AddMemoCommand = new DelegateCommand(AddMemo);
            //删除备忘录
            DelectCommand = new DelegateCommand<MemoInfoDTO>(Delect);
        }


        #region 添加待办清单
        private bool _IsShowAddMemo;

        public bool IsShowAddMemo
        {
            get { return _IsShowAddMemo; }
            set
            {
                _IsShowAddMemo = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand ShowAddMemoCommand { get; set; }

        private string _searchTitle;

        public string SearchTitle
        {
            get => _searchTitle;
            set
            {
                _searchTitle = value;
                RaisePropertyChanged(); // 通知界面绑定的值发生了变化
            }
        }

        /// <summary>
        /// 搜索待办清单
        /// </summary>
        public DelegateCommand<string> SearchCommand { get; set; }
        private void Search(string? SearchTitle)
        {

            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Get;
            apiRequest.Route = $"Memo/QueryMemo?Title={SearchTitle}";
            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                MemoInfoList = JsonConvert.DeserializeObject<List<MemoInfoDTO>>(apiResponse.ResultData.ToString());
                Visibility = (MemoInfoList.Count > 0) ? Visibility.Hidden : Visibility.Visible;
            }

            else
            {
                MemoInfoList = new List<MemoInfoDTO>();
            }

        }

        #endregion
        /// <summary>
        /// 搜索结果为空时的提示
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

        public MemoInfoDTO memoInfoDTO { get; set; } = new MemoInfoDTO();
        /// <summary>
        /// 添加备忘录
        /// </summary>
        public DelegateCommand AddMemoCommand { get; set; }
        private void AddMemo()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = Method.Post;
            apiRequest.Route = "Memo/AddMemo";
            apiRequest.Partement = memoInfoDTO;
            ApiResponse apiResponse = httpClient.Execute(apiRequest);
            if (apiResponse.ResultCode == 1)
            {
                MessageBox.Show("添加成功");
                createMemo();
                //Search(null);

                //HomeUCViewModel().Refresh();//刷新统计面板数据
                //Refresh();//刷新统计面板数据
                IsShowAddMemo = false;
            }
            else
            {
                MessageBox.Show("添加失败");
            }

        }



        public DelegateCommand <MemoInfoDTO> DelectCommand { get; set; }

        private void Delect(MemoInfoDTO memoInfoDTO)
        {
            if (memoInfoDTO != null)
            {
                var result = MessageBox.Show("您确定要删除这条备忘录吗？删除后将无法恢复哦～",
                                      "温馨提示",
                                      MessageBoxButton.OKCancel,
                                      MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                { 
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method = Method.Delete;
                apiRequest.Route = "Memo/DeleteMemo";
                apiRequest.Partement = memoInfoDTO;
                ApiResponse apiResponse = httpClient.Execute(apiRequest);
                if (apiResponse.ResultCode == 1)
                {
                    MessageBox.Show(apiResponse.Msg);
                        //刷新
                    createMemo();
                   
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
                }
            }
        }
    }
}
