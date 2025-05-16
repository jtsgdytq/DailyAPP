using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DaliyAPP.WPF.Extensions
{
    public class ExtensionPwd
    {

        /// <summary>
        /// 附加属性：密码PasswordBox
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>


        public static string GetPwd(DependencyObject obj)
        {
            return (string)obj.GetValue(PwdProperty);
        }

        public static void SetPwd(DependencyObject obj, string value)
        {
            obj.SetValue(PwdProperty, value);
        }

        // Using a DependencyProperty as the backing store for Pwd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PwdProperty =
            DependencyProperty.RegisterAttached("Pwd", typeof(string), typeof(ExtensionPwd), new PropertyMetadata("", OnChangePwd));
        /// <summary>
        /// 附加属性发生变化，更新密码值
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnChangePwd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = d as PasswordBox;
            string NewPwd = e.NewValue as string;

            if (passwordBox != null && passwordBox.Password != NewPwd)//密码值发生变化，重新赋值
            {
                passwordBox.Password = NewPwd;
            }
        }
    }

    /// <summary>
    /// 密码框行为类，当密码框的密码发生变化时，更新附加属性的值
    /// </summary>
    public class PwdBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()//注入事件
        {
             AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;

        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)//事件方法
        {
            PasswordBox passwordBox = sender as PasswordBox;// passwordBox.Password为密码框的值

            string password = ExtensionPwd.GetPwd(passwordBox);//获取附加属性的密码值
            if (passwordBox != null&& password!= passwordBox.Password)//附加属性的值和密码框的值不相等时，更新附加属性的值
            {
               ExtensionPwd.SetPwd(passwordBox, passwordBox.Password);//更新附加属性的值
            }
        }

        protected override void OnDetaching()//撤销事件
        {
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
            
        }
    }
}