using DaliyAPP.WPF.DTOs;
using DaliyAPP.WPF.HttpsCliens;
using DaliyAPP.WPF.MsgEvents;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;

namespace DaliyAPP.WPF.ViewModels
{
    public class LoginUCModel :BindableBase, IDialogAware
    {


        public string Title  {get;set;}="丁真的日常";

        public event Action<IDialogResult> RequestClose;

        private readonly HttpClient httpsclient;

        public IEventAggregator Aggregator;

        
        #region 构造函数
        public LoginUCModel(HttpClient _httpsclient, IEventAggregator _Aggregator)
        {
           
            //RegIfon = new DelegateCommand(RegIfonClick); // 可删除或重命名
            ToRegisterCommand = new DelegateCommand(() => CurrentViewIndex = 1);//注册界面
            ToLoginCommand = new DelegateCommand(() =>
            {
                CurrentViewIndex = 0;//登录界面
                accountInfoDTO = new AccountInfoDTO();//清空注册信息
            });

            Logincmm = new DelegateCommand(Login);
            RegCommand = new DelegateCommand((Reg));
            //实例化注册信息

            accountInfoDTO = new AccountInfoDTO();

            httpsclient = _httpsclient;
            //事件订阅及发布
            Aggregator = _Aggregator;
                


        }

        #endregion


        #region 登录命令
        /// <summary>
        /// 后端登录接口使用了一个包含 AccountName 的 DTO（AccountDTO），但是前端并没有传这个字段，因此触发了 ASP.NET Core 的模型验证机制，返回了 字段缺失 的 400 错误,解决办法；重新定义一个 DTO 类，包含 Account 和 Password 字段，并在登录时使用这个 DTO 进行验证，舍弃了AccountName这个字段。
        /// </summary>
        public DelegateCommand Logincmm { get; set; }
        public void Login()
        {
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Password))
            {
                Aggregator.GetEvent<MsgEvent>().Publish("登录信息不全");
                return;
            }

            ApiRequest apiRequest = new ApiRequest
            {
                Method = RestSharp.Method.Post,
                Route = "Account/Login",
                Partement = new AccountInfoDTO { Account = this.Account, Password = this.Password } 
            };

            ApiResponse apiResponse = httpsclient.Execute(apiRequest);
            if (apiResponse.ResultCode == 666)
            {

                //反序列化
                AccountInfoDTO accountInfo= JsonConvert.DeserializeObject<AccountInfoDTO>(apiResponse.ResultData.ToString());
                //通过对话框参数传递登录成功的账号信息
                DialogParameters parameters = new DialogParameters();
                parameters.Add("LoginName", accountInfo.AccountName);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK,parameters));
             

            }
            else
            {
                Aggregator.GetEvent<MsgEvent>().Publish(apiResponse.Msg);
            }
        }


        #endregion

        #region 注册命令

        public DelegateCommand RegCommand {get;set;}

        public void Reg()
        {
            //Aggregator.GetEvent<MsgEvent>().Publish("注册");
            // 数据基本验证
            if (string.IsNullOrEmpty(accountInfoDTO .AccountName) ||
                string.IsNullOrEmpty(accountInfoDTO .Account) ||
                string.IsNullOrEmpty(accountInfoDTO .Password) ||
                string.IsNullOrEmpty(accountInfoDTO.ConfirmPassword))
            {
                Aggregator.GetEvent<MsgEvent>().Publish("注册信息不全");//发布消息
               // MessageBox.Show("注册信息不全,请输入完整账号密码");
                return;
            }

            if (accountInfoDTO.Password!= accountInfoDTO.ConfirmPassword)
            {
                Aggregator.GetEvent<MsgEvent>().Publish("两次密码输入不一致");//发布消息
               // MessageBox.Show("两次密码输入不一致");
                return;
            }
            // 调用API
            ApiRequest apiRequest = new ApiRequest();

            apiRequest.Method=RestSharp.Method.Post;
            apiRequest.Route = "Account/Reg";
            apiRequest.Partement = accountInfoDTO;

          ApiResponse apiResponse= httpsclient.Execute(apiRequest);

            if(apiResponse.ResultCode==1)
            {
                Aggregator.GetEvent<MsgEvent>().Publish(apiResponse.Msg);//发布消息
               // MessageBox.Show(apiResponse.Msg);
                CurrentViewIndex = 0;

                accountInfoDTO = new AccountInfoDTO();//清空注册信息


            }
            else
            {
                Aggregator.GetEvent<MsgEvent>().Publish(apiResponse.Msg);//发布消息
                //MessageBox.Show(apiResponse.Msg);
            }



        }
        //注册信息
        private AccountInfoDTO _accountInfoDTO;

        public AccountInfoDTO accountInfoDTO
        {
            get { return _accountInfoDTO; }
            set
            {
                _accountInfoDTO = value;
                RaisePropertyChanged();
            }
        }



        #endregion

        #region
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
          
        }

        #endregion


        #region 视图切换
        private int _currentViewIndex;
        public int CurrentViewIndex//未设置值，默认值为0，会选择登录界面
        {
            get
            { return _currentViewIndex; }
            set
            {
                _currentViewIndex = value;
                RaisePropertyChanged();
            }
        }

        //public DelegateCommand RegIfon;//

        //public void RegIfonClick()
        //{
        //    if (CurrentViewIndex == 0)
        //    {
        //        CurrentViewIndex = 1;
        //    }
        //    else
        //    {
        //        CurrentViewIndex = 0;
        //    }
        //}

        public DelegateCommand ToRegisterCommand { get; set; }//注册命令
        public DelegateCommand ToLoginCommand { get; set; }//登录命令



        #endregion

        #region 账号密码

        protected string _account;//账号

        public string Account
        {
            get { return _account; }
            set
            {
                if (_account != value)
                {
                    _account = value;
                    RaisePropertyChanged();
                }
            }
        }


        protected string _password;//密码

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion
    }
}
