   M S B A M L   `   `   `  ����G  CDailyAPP.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DaliyAPP.WPF.Views.DialogDaliyAPP.WPF.Views.Dialog  5      )    #DaliyAPP.WPF.Views.Dialog.AddWaitUC   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:DaliyAPP.WPF.Views.Dialog     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	��280��5	      $	��450��6   .��5
      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      �� 5      $
��auto �6   .+�5   
   ��  �� Padding$ 20,10q�5   
   $��添加待办��5   
   $
��Bold"�6   $��20(�6(   ]� 5   
   $��1��6   $��False��6!   .+�5      �� $��20,10q�5      $	��Top=�5       �� Orientation$ 
Horizontal=�6#   .+�5      �� $��状态:��6   $��Center=�6'   �� 5      $��10,0,0,0q�6    �� SelectedIndex 6.   �� *WaitInfoDTO.Status+.@�5      �� .��6#   待办6'   �� 5      .��6#   	已完成6(   5      5      �� 5       $
��20,0q�6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入待办概要��6$   $	��Top=�6A   ��6V   �� *WaitInfoDTO.Title+�� 5"      $��20,10q�6   $	��100��6%   $ 请输入待办内容��66   $	��Top=�6S   ��6h   �� *WaitInfoDTO.Content+5#      �� 5%   
   $��10q�6   $ 
Horizontal=�6!   $��2��6:   $��Right=�6G   .+�5&      �� $��取消��6   $��0,0,10,0q�6"       MaterialDesignOutlinedButton#��[  64   ��6k   �� *CancelCommand+�� 5'      $��确定��6   ��6$   �� *SaveCommand+5(      5)      5*      