   M S B A M L   `   `   `  ����J  FFourInRowClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:FourInRowClientFourInRowClient  5   	        FourInRowClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:FourInRowClient     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��64* �6   �� 5      $	��71* �6   �� 5      $	��78* �6   �� 5      $
��106* �6    � ColumnDefinitions 5   
   �� 5      $
��123* �6   �� 5      $
��232* �6   �� 5      $
��162* �6   .+�5   
   �� -   5        $��10,44,10,43q�5      $��Send Game Request��6&   $��30(�6`   $��Italic$�6n   $��3��6�   $��1��6�   �� 5   
   -   5        $��10,44,28,43q�5      $��Search��6&   $��3��6L   $��30(�6Y   $��Italic$�6h   �� 5   
   $��2��6   $��4��6%   .+�5      �� .��6   Connected Clients:6*   �� 5      -   5        $��lbUsers��5   "   $��10q�6   $	��271��61   6>   5      �� 5   
   $��0��6   $��4��6%   .+�5      �� .��6   Player's Information:6-   �� 5      -   5        $��
lbInfoList��5   "   $��10q�6   $	��271��64   5       �� 5"   
   $��1��6   $��4��6&   .+�5#      �� .��6    Player's Games' inforrmations:64   �� 5$      -   5        $��lbGamesList��5$   ,   $��10,10,160,10q�6   $	��272��6?   5%      5&      5'      