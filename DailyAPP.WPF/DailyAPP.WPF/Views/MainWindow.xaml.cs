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
using System.Windows.Shapes;
using DaliyAPP.WPF.ViewModels;


namespace DaliyAPP.WPF.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           //this.DataContext = new MainWindowModel();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ColorZone_MouseDoubleClick(object sender, MouseButtonEventArgs e)//双击放大
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        /// <summary>
        /// 左侧菜单选中事件后关闭抽屉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            drawerHost.IsLeftDrawerOpen = false;
        }

        
    }
}
