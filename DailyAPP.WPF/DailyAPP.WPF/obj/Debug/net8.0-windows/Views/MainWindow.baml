   M S B A M L   `   `   `  ����G  CDailyAPP.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DaliyAPP.WPF.ViewsDaliyAPP.WPF.Views  5      #    DaliyAPP.WPF.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      2local clr-namespace:DaliyAPP.WPF.Views     5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      _	 [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors 	 6Q   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5       � WindowStartupLocation$ CenterScreen=�5       � Title$ 
MainWindow��5   	   $	��450��6   $	��800��6)   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ -1q�6   62   .��5      )  #MaterialDesignThemes.Wpf.DialogHost     DialogTheme$ Inherit=�6       
Identifier$ 
RootDialog��67   .��5   
   )  #MaterialDesignThemes.Wpf.DrawerHost  -   5          Name$ 
drawerHost��5   n      IsLeftDrawerOpen 6$   �� 	 �� ElementName$	 MenuToggleButton��
 �� Path$
 	IsChecked�   LeftDrawerContent 5      ]� 5      $	��220��6   .+�5      �� $
��0,20q�6!   $	��Top=�6/   .+�5      �� $1��+/image/一眼丁真_爱给网_aigei_com.png��6    $��50��6G   $��50��6S    J� Clip 5      @� 5   "    @� Center$ 24,24&�62    @� RadiusX$ 24Q�6A    @� RadiusY$ 24Q�6N   6]   5      5      �� 5!      -   5         ��Name$ lbMenu��5"         LeftMenuStyle#��[  5!   C   ��6   �� *LeftMenuList+* 	 $Microsoft.Xaml.Behaviors.Interaction   Triggers 5#      + 	 %Microsoft.Xaml.Behaviors.EventTrigger  5$         	EventName$ SelectionChanged��6-   * 	 $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5%   "   2 	 ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *NavigateCommand+   CommandParameter 6\   �� *SelectedItem+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ListBox5'   #   5(      ��5+      �� 5,      .R�5-   "   ��  �� Orientation$ 
Horizontal=�6-   
������ .+�5.   &   '	  !MaterialDesignThemes.Wpf.PackIcon	  $
��10,0q�6T    	  Kind 6>   �� *Icon+6T   �� 5/   &   ��60   �� *
MenuName+6L   50   #   51      53      54      .��58      ]� .+�59      (
  "MaterialDesignThemes.Wpf.ColorZone
  -   5        $��16q�59   +   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp4=�5:   #   $	��Top=�5<   #    
  Mode$ 
PrimaryMid=�5=   #   .��5>      ]� $
��True��6    .+�5?      �� $��Right=�6%   $ 
Horizontal=�6<   .+�5@      �� $1��+/image/一眼丁真_爱给网_aigei_com.png��6$   $��25��6K   $��25��6W    5B   "   @� 5C   &   $ 12,12&�66   $ 12Q�6E   $ 12Q�6R   6a   5E      �� 5G      -   5         ��Name$ btnMin��5G   %   $	��—��65    ! MaterialDesignFlatMidBgButton#��[ 6A   6x   �� 5H      -   5        $ btnMax��5H   %   $	��☐��65   #��[ 6A   6x   �� 5I      -   5        $ btnClose��5I   %   $	��✕��67   #��[ 6C   6z   5J      �� 5L      $ 
Horizontal=�6%   .+�5M      d� -   5         d�Name$ MenuToggleButton��5M   +   4  .System.Windows.Automation.AutomationProperties   Name$ HamburgerToggleButton��5N        d� 	IsChecked$  FalseY�5O       ' #MaterialDesignHamburgerToggleButton#��[ 5P      �� 5R      $��24,0,0,0q�6%     MaterialDesignToolButton#��[ 5X      ! � ToolTip$! Previous Item��5Y      +  %MaterialDesignThemes.Wpf.RippleAssist"   Feedback" 5S      ��  �� *Self+$
 
Foreground�  BrushRoundConverter# �� 	Converter## [ ��5T      �� *GoBackCommand+��5U      0  *MaterialDesignThemes.Wpf.PackIconExtension  $   Kind$$ 	ArrowLeft=�%   Size% 24��5W      ��  �� " �$
 (TextElement.Foreground)�5Y      �� 5[      $��16,0,0,0q�6%   #��[ 5a      $! 	Next Item��5b      " 5\      ��  �� *Self+$
 
Foreground�## [ ��5]      �� *GoForwardCommand+��5^        $$ 
ArrowRight=�% 24��5`      ��  �� " �$
 (TextElement.Foreground)�5b      �� 5c      $
��15,0q�6(   $��Center=�5d      $��Center=�5e      $) #Mat15.0erial Design In XAML Toolkit��5f      $��22(�5g      $��我的日常��5h      5i      5j      5k      �� 5n      !  Prism.Regions.RegionManager&   
RegionName$& MainViewRegion��6!   6S   5o      5p      5q      5r      