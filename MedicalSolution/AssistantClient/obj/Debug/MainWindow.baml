   M S B A M L   `   `   `  ����J  FAssistantClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AssistantClientAssistantClient  5   	        AssistantClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:AssistantClient     5   	     � Title$  AssistantWelcome��5   	    � WindowState$ 	Maximized=�6"    � WindowStyle$
 None=�5	   	   .��5      � .+�5   
   �� $��Üdvözöljük!��6   $��Center=�6&   $��0,50,0,0q�6C   $	��Top=�6U   $��Center=�6m   $��Center=�6�   $	��100��6�   �� 5   
   -   5        $��AddPatientButton��5      $��Új Beteg Felvétele��6)   $��Center=�6F   $��-180,100,0,0q�6c   $	��Top=�6y   $��Center=�6�   $��Center=�6�   $	��150��6�   6�   �� 5   
   -   5        $��PatientListButton��5      $��Felvett Betegek��6*   $��Center=�6D   $��180,100,0,0q�6a   $	��Top=�6v   $��Center=�6�   $��Center=�6�   $	��150��6�   6�   �� 5   
   -   5        $��CloseButton��5      $��	Kilépés��6$   $��Center=�66   $��	0,140,0,0q�6S   $	��Top=�6f   $��Center=�6~   $��Center=�6�   $	��150��6�   6�   �� 5   
   -   5        $��PatientListView��5   �   $��Disabled=�6y   $	��300��6�   $	��300��6�   $��	0,190,0,0q�6�   ��6   �� *PatientList+ �� View 5      �� 5      .P�5      �� $��	TAJ-szám��6%    �� Width$	 100��67    �� DisplayMemberBinding 6C   �� *SSN+�� 5      $
��Név��6%   $	 200��62    6>   �� *Name+5      5      �� 5   
   -   5        $��NewPatientView��5      $��Center=�6+   $��	0,190,0,0q�6H   $	��300��6[   $��Center=�6h   $	��300��6�   .+�5      �� $��	Új Beteg��6   �� 5      $��	TAJ-szám��6   $��	0,0,198,0q�6'   �� 5      -   5        $��AddSSN��5      $��23��6O    �� TextWrapping$ NoWrap=�6[   $�� ��6q   $
��10,0q�6y    J� RenderTransformOrigin$ 1.508,0.607&�6�   �� 5      $
��Név��6   $��	0,0,198,0q�6"   �� 5      -   5        $��AddName��5      $��23��6%   $ NoWrap=�61   $�� ��6G   $
��10,0q�6O   $ 1.508,0.607&�6]   �� 5      $
��Cím��6   $��	0,0,198,0q�6"   �� 5      -   5        $��
AddAddress��5      $��23��6(   $ NoWrap=�64   $�� ��6J   $
��10,0q�6R   $ 1.508,0.607&�6`   �� 5       $��Panasz��6   $��	0,0,198,0q�6%   �� 5!      -	   5        $��AddComplaint��5!      $��50��6*   $
 Wrap=�66   $�� ��6J   $
��10,0q�6R   $ 1.508,0.607&�6`   �� 5"      -
   5        $��	Felvétel��5"      $��Center=�6(   $��-120,15,0,0q�6E   $	��Top=�6Z   $��Center=�6r   $��Center=�6�   $	��100��6�   6�   �� 5#      -   5        $��Mégsem��5#      $��Center=�6&   $��120,-19,0,0q�6C   $	��Top=�6X   $��Center=�6p   $��Center=�6�   $	��100��6�   6�   5$      �� 5%   
   -   5        $��PatientView��5%      $��Center=�6(   $��	0,190,0,0q�6E   $	��300��6X   $��Center=�6e   $	��300��6�   .+�5&      �� $��Beteg adatai��6   �� 5'      $��	TAJ-szám��6   $��	0,0,198,0q�6'   �� 5(      -   5        $��EditSSN��5(       �� 
IsReadOnly . $��23��67   $ NoWrap=�6C   $
��10,0q�6�   $ 1.508,0.607&�6�   ��6Y   �� *SelectedItem.SSN+	 �� ElementName$	 PatientListView��
 �� UpdateSourceTrigger$
 Explicit=�6�   �� 5)      $
��Név��6   $��	0,0,198,0q�6"   �� 5*      -   5        $��EditName��5*      $��23��6&   $ NoWrap=�62   $
��10,0q�6�   $ 1.508,0.607&�6�   ��6H   �� *SelectedItem.Name+$	 PatientListView��$
 PropertyChanged=�6�   �� 5+      $
��Cím��6   $��	0,0,198,0q�6"   �� 5,      -   5        $��EditAddress��5,      $��23��6)   $ NoWrap=�65   $
��10,0q�6�   $ 1.508,0.607&�6�   ��6K   �� *SelectedItem.Address+$	 PatientListView��$
 PropertyChanged=�6�   �� 5-      $��Panasz��6   $��	0,0,198,0q�6%   �� 5.      -   5        $��EditComplaint��5.      $��50��6+   $
 Wrap=�67   $
��10,0q�6�   $ 1.508,0.607&�6�   ��6K   �� *SelectedItem.Complaint+$	 PatientListView��$
 PropertyChanged=�6�   �� 5/      -   5        $��Mentés��5/      $��Center=�6&   $��-120,15,0,0q�6C   $	��Top=�6X   $��Center=�6p   $��Center=�6�   $	��100��6�   6�   �� 50      -   5        $��Mégsem��50      $��Center=�6&   $��120,-19,0,0q�6C   $	��Top=�6X   $��Center=�6p   $��Center=�6�   $	��100��6�   6�   51      53      54      