   M S B A M L   `   `   `  ����G  CDailyAPP.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DaliyAPP.WPF.ViewsDaliyAPP.WPF.Views  5      ;"clr-namespace:DaliyAPP.WPF.ConversDaliyAPP.WPF.Convers  5      [ WMaterialDesignColors, Version=2.1.4.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aY@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColorsMaterialDesignColors 5	   5   #    DaliyAPP.WPF.Views.PersonalUC   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DaliyAPP.WPF.Views     5      9
converters"clr-namespace:DaliyAPP.WPF.Convers     5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5	      ]materialDesignColors@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColors   65   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5
       � 	Resources 5      0   *DaliyAPP.WPF.Convers.ColorToBrushConverter   ColorToBrushConverter  SwatchColorTemplate �� DataType �� 	Converter �� RelativeSource �� AncestorType�� 5   
   %   &	        &	      7     5      6/   �� 5      " ��67   .R�5      �� $��40��6   $��40��5      ��5      �� 8   ��5      �� *DataContext.ChangeHueCommand+ ��  local:PersonalUC��5      �� 5      5      5      .��5      �  � RowDefinitions 5   
   �� 5       $
��auto �6   6,   �� 5!      6   .+�5$   
   �� $
��0,10q�6    �� Orientation$ 
Horizontal=�6#   .+�5%      �� $��Center=�6   $��浅色��63   d� 5'      $��8,0,16,0q�6    d� 	IsChecked 6-   �� *IsDarkTheme+�� 5)      $��Center=�6   $��深色��63   5*      �� 5-   
   $��1��6   $��Hidden=�5.      $��Hidden=�5/      	 �� PanningMode$
	 Both=�50      $
��True��51      .��52      �� ��6   �� *
Swatches+��53      �� 54      "  MaterialDesignColors.ISwatch"  6#   .R�55      �� $ 
Horizontal=�6%   .+�56      �� $��80��6(   $��Center=�57       ��58       �� *Name+
 �� Mode$
 OneTime=��� 59      #��[ 6+   ��5:   #   �� *Lookup.Values+$
 OneTime=���5;   "   �� 5<   &   .R�5=   *   $�  $� Orientation$ 
Horizontal=�6A   5>   '   5@      5A      5B      5D      5E      5G      5H      