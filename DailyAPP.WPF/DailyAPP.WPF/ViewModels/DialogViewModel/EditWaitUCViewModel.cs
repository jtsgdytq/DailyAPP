using DaliyAPP.WPF.DTOs;
using DaliyAPP.WPF.Service;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DaliyAPP.WPF.ViewModels.DialogViewModel
{
    class EditWaitUCViewModel: IDialogHostAware
    {
        public EditWaitUCViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public DelegateCommand SaveCommand { get; set; }
        //保存逻辑
        private void Save()
        {
            if (string.IsNullOrEmpty(WaitInfoDTO.Title) || string.IsNullOrEmpty(WaitInfoDTO.Content))
            {
                MessageBox.Show("标题和内容不能为空");
                return;
            }
            if (DialogHost.IsDialogOpen("RootDialog"))
            {
                //将数据传递给主窗口
                DialogParameters parameters = new DialogParameters();
                parameters.Add("NewWaitInfoDTO", WaitInfoDTO);
                DialogHost.Close("RootDialog", new DialogResult(ButtonResult.OK, parameters));
            }
        }
        public DelegateCommand CancelCommand { get; set; }
        //取消逻辑
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen("RootDialog"))
            {
                DialogHost.Close("RootDialog", new DialogResult(ButtonResult.Cancel));
            }
        }
        public void OnDialogOpening(IDialogParameters parameters)
        {
            WaitInfoDTO = parameters.GetValue<WaitInfoDTO>("OldWaitInfoDTO");
        }

        public WaitInfoDTO WaitInfoDTO { get; set; } = new WaitInfoDTO();
    }
}

