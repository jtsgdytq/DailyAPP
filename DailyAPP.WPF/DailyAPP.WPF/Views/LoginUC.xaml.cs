using DaliyAPP.WPF.MsgEvents;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DaliyAPP.WPF.Views
{
    /// <summary>
    /// LoginUC.xaml 的交互逻辑
    /// </summary>
    public partial class LoginUC : UserControl
    {
        //订阅消息
        private readonly IEventAggregator Aggregator;//EventAggregator是Prism中专门处理ViewModel与ViewModel之间事件传递的类对象，它提供了针对事件的发布方法和订阅方法，所以可以非常方便的来管理事件。
        public LoginUC(IEventAggregator _Aggregator)
        {
            InitializeComponent();
            Aggregator = _Aggregator;
            //订阅
            Aggregator.GetEvent<MsgEvent>().Subscribe(sub);

            
        }
        //执行业务
        private void sub(string obj)
        {
            RegLoginBar.MessageQueue.Enqueue(obj);
        }
    }
}
