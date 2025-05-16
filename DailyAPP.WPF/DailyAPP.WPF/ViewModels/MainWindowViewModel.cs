using DaliyAPP.WPF.Models;
using DaliyAPP.WPF.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DaliyAPP.WPF.ViewModels
{
    public class MainWindowModel : BindableBase
    {

        /// <summary>
        /// 左侧菜单列表
        /// </summary>
        private List<LeftMenuModel> _LeftMenuList;

        /// <summary>
        /// 左侧菜单集合
        /// </summary>
        public List<LeftMenuModel> LeftMenuList
        {
            get { return _LeftMenuList; }
            set
            {
                _LeftMenuList = value;
                RaisePropertyChanged();
            }
        }
        #region  构造函数
        public MainWindowModel(IRegionManager _RegionManager)
        {
            LeftMenuList = new List<LeftMenuModel>();//初始化左侧菜单集合

            NavigateCommand = new DelegateCommand<LeftMenuModel>(Navigate);//导航命令

            GoBackCommand = new DelegateCommand(GoBack);//后退命令
            GoForwardCommand = new DelegateCommand(GoForward);//前进命令


            CreateMenu();

            RegionManager = _RegionManager;//注入区域管理器

        }

        #endregion
        /// <summary>
        /// 创建左侧菜单
        /// </summary>
        private void CreateMenu()
        {
            LeftMenuList.Add(new LeftMenuModel() { Icon = "Home", MenuName = "首页", ViewName = "HomeUC" });
            LeftMenuList.Add(new LeftMenuModel() { Icon = "NotebookOutline", MenuName = "待办事项", ViewName = "WaitUC" });
            LeftMenuList.Add(new LeftMenuModel() { Icon = "NotebookPlus", MenuName = "备忘录", ViewName = "MemoUC" });
            LeftMenuList.Add(new LeftMenuModel() { Icon = "Cog", MenuName = "设置", ViewName = "SettingUC" });
        }


        #region 导航
        private readonly IRegionManager RegionManager;


        public DelegateCommand<LeftMenuModel> NavigateCommand { get; set; }


        private void Navigate(LeftMenuModel menu)
        {
            if (menu == null) return;//如果菜单为空则返回
            RegionManager.Regions["MainViewRegion"].RequestNavigate(menu.ViewName,
                callback => 
                {
                    journal = callback.Context.NavigationService.Journal;//获取导航服务的导航记录

                  
                });//导航到指定视图
        }
        #endregion
        
        private IRegionNavigationJournal journal { get; set; } = null;

        public DelegateCommand GoBackCommand { get; private set; }
        private void GoBack()
        {
            if (journal != null && journal.CanGoBack)
                journal.GoBack();
           

        }

        public DelegateCommand GoForwardCommand { get; private set; }

        private void GoForward()
        {
           if(journal != null && journal.CanGoForward)
                journal.GoForward();
           
        }

        public void SetDefualNavigation(string LoginName)
        {
            //设置导航参数，将账户名传递给首页
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("LoginName", LoginName);
            RegionManager.Regions["MainViewRegion"].RequestNavigate("HomeUC",
                 callback =>
                 {
                     journal = callback.Context.NavigationService.Journal;//获取导航服务的导航记录


                 },parameters);
        }
    }
}




