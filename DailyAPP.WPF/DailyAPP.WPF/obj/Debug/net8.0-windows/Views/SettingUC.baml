   M S B A M L   `   `   `  ����G  CDailyAPP.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DaliyAPP.WPF.ViewsDaliyAPP.WPF.Views  5      "    DaliyAPP.WPF.Views.SettingUC   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DaliyAPP.WPF.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5	      .��5      �    MaterialDesignPaper#���   6     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��设置��6   $��30(�6   $
��Bold"�6,   $
��5,10q�6>   �� 5   
   $
��5,10q�6   ���#DDDDDD$��0,0,0,3q�65   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $��4* �6#   .+�5      �� -   5         ��Name$ lbMenu��5        LeftMenuStyle#��[ 5   :   ��6   �� *SettingList+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5         	EventName$ SelectionChanged��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 60   �� *NavigateCommand+   CommandParameter 6T   ��  �� ElementName$ lbMenu��	 �� Path$	 SelectedItem�5       5!      ��5$      �� 5%      .R�5&      �� 
 �� Orientation$
 
Horizontal=�6%   
������ .+�5'      '  !MaterialDesignThemes.Wpf.PackIcon  $
��10,0q�6L      Kind 66   �� *Icon+6L   �� 5(      ��6(   �� *
MenuName+6D   5)      5*      5,      �� 5.      $��1��6   ! 	 Prism.Regions.RegionManager   
RegionName$ SettingRegion��6-   5/      51      52      