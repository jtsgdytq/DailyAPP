using DaliyAPP.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DaliyAPP.WPF.ViewModels
{
    class SettingUCViewModel:BindableBase
    {
        public SettingUCViewModel(IRegionManager regionManager)
        {
            createSettingList();

            NavigateCommand = new DelegateCommand<SettingModel>(Navigate);
            _regionManager = regionManager;
        }
        private List<SettingModel> _SettingList;

        public List<SettingModel> SettingList

        {
            get { return _SettingList; }
            set { 
                _SettingList = value;
               RaisePropertyChanged(nameof(SettingList));
            }
        }

        private void createSettingList()
        {
            SettingList = new List<SettingModel>();
            SettingList.Add(new SettingModel { Icon = "Palette", MenuName = "个性化", ViewName = "PersonalUC" });
            SettingList.Add(new SettingModel { Icon = "Cog", MenuName = "系统设置", ViewName = "SysSetUC" });
            SettingList.Add(new SettingModel { Icon = "Information", MenuName = "关于我们", ViewName = "AboutUsUC" });
        }

        private readonly IRegionManager _regionManager;

        public DelegateCommand <SettingModel> NavigateCommand { get; set; }

        private void Navigate(SettingModel settingModel)
        {
            _regionManager.Regions["SettingRegion"].RequestNavigate(settingModel.ViewName);
        }
    }
}
