using DaliyAPP.WPF.HttpsCliens;
using DaliyAPP.WPF.Service;
using DaliyAPP.WPF.ViewModels;
using DaliyAPP.WPF.ViewModels.DialogViewModel;
using DaliyAPP.WPF.Views;
using DaliyAPP.WPF.Views.Dialog;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RestSharp;
using System.Configuration;
using System.Data;
using System.Windows;



namespace DaliyAPP.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    /// <summary>
    /// 应用程序的主入口点，设置主界面
    /// </summary>
    /// <returns></returns>
    protected override Window CreateShell()
    {
        return Container.Resolve<Views.MainWindow>();
    }
    /// <summary>
    /// 依赖注入容器注册
    /// </summary>
    /// <param name="containerRegistry"></param>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        //登录
        containerRegistry.RegisterForNavigation<Views.LoginUC, ViewModels.LoginUCModel>();
        //请求

        containerRegistry.GetContainer().Register<HttpClient>(made: Parameters.Of.Type<string>(serviceKey: "wbUrl"));

        ViewModelLocationProvider.Register<MainWindow,MainWindowModel>();

        //导航页
        containerRegistry.RegisterForNavigation<HomeUC, HomeUCViewModel>();//首页
        containerRegistry.RegisterForNavigation<MemoUC, MemoUCViewModel>();//备忘录
        containerRegistry.RegisterForNavigation<SettingUC, SettingUCViewModel>();//设置
        containerRegistry.RegisterForNavigation<WaitUC, WaitUCViewModel>();//待办事项
        //设置
        containerRegistry.RegisterForNavigation<PersonalUC, PersonalUCViewModel>();//个性化
        containerRegistry.RegisterForNavigation<SysSetUC>();//系统设置
        containerRegistry.RegisterForNavigation<AboutUsUC>();//关于我们
        //添加待办事项对话框
        containerRegistry.RegisterForNavigation<AddWaitUC, AddWaitUCViewModel>();
        //编辑待办事项对话框
        containerRegistry.RegisterForNavigation<EditWaitUC, EditWaitUCViewModel>();
        //自定义对话框服务
        containerRegistry.Register<DialogHostService>();
        //添加备忘录对话框
        containerRegistry.RegisterForNavigation<AddMemoUC, AddMemoUCViewModel>();
        //编辑备忘录对话框
        containerRegistry.RegisterForNavigation<EditMemoUC, EditMemoUCViewModel>();
    }
    /// <summary>
    /// 应用程序启动时的初始化
    /// </summary>
    protected override void OnInitialized()
    {
        var dialog = Container.Resolve<IDialogService>();
        dialog.ShowDialog("LoginUC", callback =>
        {
            if (callback.Result != ButtonResult.OK)
            {
                Environment.Exit(0);
                return;
            }

            var mainVM = Current.MainWindow.DataContext as MainWindowModel;//主界面数据上下文
            if (mainVM != null)
            {
                if (callback.Parameters.ContainsKey("LoginName"))
                {
                    string LoginName = callback.Parameters.GetValue<string>("LoginName");

                    mainVM.SetDefualNavigation(LoginName);
                }
            }

            base.OnInitialized();
        });

    }


}

