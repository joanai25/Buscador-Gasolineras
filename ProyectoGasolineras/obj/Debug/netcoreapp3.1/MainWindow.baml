   M S B A M L   `   `   `  ����d  `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF  5   	   N JProyectoGasolineras, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ProyectoGasolinerasProyectoGasolineras 5   	   $   ProyectoGasolineras.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   `mRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:ProyectoGasolineras     5   	     � Title$  
MainWindow��5	   	   $	��720��6   $
��1280��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��* �6   6)   .+�5   
   �� $��Gasolineras de España��6   $��Center=�60   $��Center=�6T   $��40(�6v   $��1��6�   $��2��6�   $��0��5      $
��Bold"�6   �� 5   
   $��	Comunidad��6   $��0��6$   $��24(�64   $
��Bold"�6B   $��118,142,51,151q�6T   $��2��6l   �� 5   
   -   5        $��cbComunidad��5      $��0��6&   $��1��66   $��24(�6C   $��84,30,28,102q�6Q   6g   �� 5   
   $��	Provincia��6   $��1��6$   $��24(�64   $
��Bold"�6B   $��250,142,240,151q�6T   $��2��6m   $��2��6~   �� 5   
   -   5         ��Name$ cbProvincia��5      $��1��6(   $��1��68   $��24(�6E   $��216,30,216,102q�6S   $��2��6k   6   �� 5   
   $��	Municipio��6   $��3��6$   $��24(�64   $
��Bold"�6B   $��56,142,116,151q�6T   $��2��6l   �� 5   
   -   5        $ cbMunicipio��5      $��3��6(   $��1��68   $��24(�6E   $��22,30,90,102q�6S   6i   �� 5!   
   -   5         ��Name$ chVisualizarTodas��5!      $��Visualizar Todas��6.   $��24(�6I   $��0��6W   $��222,113,205,23q�6g   $��2��6   $��1��6�   $��Center=�5"       d� 	IsChecked$
 TrueY�6W   	� 5#   
   -   5         	�Name$ rbBarato��5#      $��Gasoleo A mas barato��6(   $��24(�6G   $��1��6V   $��204,113,135,23q�6f   $��2��6~   $��1��6�   $��Center=�5$      ${�False��68   6J   	� 5%   
   -   5        $ rbCaro��5%      $��Gasoleo A mas caro��6&   $��24(�6C   $��2��6R   $��218,113,190,23q�6b   $��1��6z   $��Center=�5&      $��2��68   ${�False��6L   6^   '   !Microsoft.Maps.MapControl.WPF.Map  5'   
   -   5          Name$ miMapa��5'      +   %Microsoft.Maps.MapControl.WPF.MapCore   CredentialsProviderT   NMicrosoft.Maps.MapControl.WPF.Design.ApplicationIdCredentialsProviderConverter$r la2tSkgyLtanz4dqoKAxc~Tr5v_MweFHrJbuRyPfLt1A~AoAIKki7O1-hOPNl1kH5_x7LNHGdqKCFWNF__iifpYCtodKP2qmSTO0-4TBR1AQj 6    $��0��5(      $��4��6    $��2��64   $��2��6A   	   	ZoomLevel$	 15Q�5)   2   5*      5,      5-      