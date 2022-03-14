Imports System.Windows.Forms

Class MainWindow
    Private Sub SpContent_Controls_MouseMove() Handles Lb_SpContent.MouseMove, Tb_SpContent.MouseMove
        Lb_SpContent.Background = Brushes.AliceBlue
        Lb_SpContent.BorderBrush = Brushes.SkyBlue
    End Sub

    Private Sub SpContent_Controls_MouseLeave() Handles Lb_SpContent.MouseLeave, Tb_SpContent.MouseLeave
        Lb_SpContent.Background = Brushes.Transparent
        Lb_SpContent.BorderBrush = Brushes.Transparent
    End Sub

    Private Sub Tb_SpTime_LostFocus(sender As Object, e As RoutedEventArgs) Handles Tb_SpTime.LostFocus
        VerifySpTime()
    End Sub

    ''' <summary>
    ''' 验证刷屏次数格式是否正确
    ''' </summary>
    Sub VerifySpTime()
        Dim Val As UInteger
        Try
            Val = Int(Me.Tb_SpTime.Text)
        Catch ex As Exception
            Val = 1
        End Try
        If Val = 0 Then
            Val = 1
        End If
        Me.Tb_SpTime.Text = Val
    End Sub

    Private Sub Bt_SpTimeDown_Click(sender As Object, e As RoutedEventArgs) Handles Bt_SpTimeDown.Click
        Me.Tb_SpTime.Text -= 1
        VerifySpTime()
    End Sub

    Private Sub Bt_SpTimeUp_Click(sender As Object, e As RoutedEventArgs) Handles Bt_SpTimeUp.Click
        Me.Tb_SpTime.Text += 1
        VerifySpTime()
    End Sub

    Private Sub Sp_Time_Controls_MouseMove() Handles Lb_SpTime.MouseMove, Tb_SpTime.MouseMove, Bt_SpTimeDown.MouseMove, Bt_SpTimeUp.MouseMove
        Me.Lb_SpTime.Background = Brushes.AliceBlue
    End Sub

    Private Sub Sp_Time_Controls_MouseLeave() Handles Lb_SpTime.MouseLeave, Tb_SpTime.MouseLeave, Bt_SpTimeDown.MouseLeave, Bt_SpTimeUp.MouseLeave
        Me.Lb_SpTime.Background = Brushes.Transparent
    End Sub

    Private Sub Sp_Key_Controls_MoudeMove() Handles Lb_SpKey.MouseMove, Cb_SpKey.MouseMove
        Me.Lb_SpKey.Background = Brushes.AliceBlue
    End Sub

    Private Sub Sp_Key_Controls_MoudeLeave() Handles Lb_SpKey.MouseLeave, Cb_SpKey.MouseLeave
        Me.Lb_SpKey.Background = Brushes.Transparent
    End Sub

    Private Sub preSp_Key_Controls_MouseMove() Handles Lb_PreSpKey.MouseMove, Cb_PreSpKey.MouseMove
        Me.Lb_PreSpKey.Background = Brushes.AliceBlue
    End Sub

    Private Sub preSp_Key_Controls_MouseLeave() Handles Lb_PreSpKey.MouseLeave, Cb_PreSpKey.MouseLeave
        Me.Lb_PreSpKey.Background = Brushes.Transparent
    End Sub

    Private Sub Sp_Freq_Controls_MouseMove() Handles Lb_SpFreq.MouseMove, Tb_SpFreq.MouseMove
        Me.Lb_SpFreq.Background = Brushes.AliceBlue
    End Sub

    Private Sub Sp_Freq_Controls_MouseLeave() Handles Lb_SpFreq.MouseLeave, Tb_SpFreq.MouseLeave
        Me.Lb_SpFreq.Background = Brushes.Transparent
    End Sub

    Private Sub Tb_SpContent_GotFocus(sender As Object, e As RoutedEventArgs) Handles Tb_SpContent.GotFocus
        If Me.Tb_SpContent.Text = "[在此处输入刷屏内容]" Then
            Me.Tb_SpContent.Text = vbNullString
        End If
    End Sub

    Private Sub Tb_SpContent_LostFocus(sender As Object, e As RoutedEventArgs) Handles Tb_SpContent.LostFocus
        If Me.Tb_SpContent.Text = vbNullString Then
            Me.Tb_SpContent.Text = "[在此处输入刷屏内容]"
        End If
    End Sub

    Private Sub Grid_Main_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Grid_Main.MouseDown
        Keyboard.Focus(Me.Bt_SpTimeDown)
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Cb_SpKey.Text = "Enter"
        Me.Cb_PreSpKey.Text = "(无)"
    End Sub

    ''' <summary>
    ''' 用于控制主循环的Timer
    ''' </summary>
    WithEvents LoopTimer As New System.Windows.Forms.Timer With {.Interval = 200, .Enabled = True}

    Sub MainLoop() Handles LoopTimer.Tick
        Cb_SpKey_SelectionChanged()
        Cb_PreSpKey_SelectionChanged()
    End Sub

    Private Sub Cb_SpKey_SelectionChanged() '(sender As Object, e As SelectionChangedEventArgs) Handles Cb_SpKey.SelectionChanged
        Me.LoopTimer.Enabled = False
        Dim newSpKey As String = "{enter}"
        If Me.Cb_SpKey.Text = "自定义" Then
            Dim tempStr = InputBox("请输入发送消息时使用的按键" + vbCrLf + "输入的内容需Sandkeys()函数可识别" + vbCrLf + "如Ctrl+Enter需输入{ctrl}{enter}")
            If tempStr = "" Then
                Me.Cb_SpKey.Text = "Enter"
            Else
                newSpKey = tempStr
                Me.Cb_SpKey.Items.Add(newSpKey)
                Me.Cb_SpKey.Items.Remove("自定义")
                Me.Cb_SpKey.Text = newSpKey
                Me.Cb_SpKey.Items.Add("自定义")
            End If
        End If
        Me.LoopTimer.Enabled = True
    End Sub

    Private Sub Cb_PreSpKey_SelectionChanged() '(sender As Object, e As SelectionChangedEventArgs) Handles Cb_PreSpKey.SelectionChanged
        Me.LoopTimer.Enabled = False
        Dim newPreSpKey As String = "{enter}"
        If Me.Cb_PreSpKey.Text = "自定义" Then
            Dim tempStr = InputBox("请输入键入消息前使用的按键" + vbCrLf + "输入的内容需Sandkeys()函数可识别" + vbCrLf + "如Ctrl+Enter需输入{ctrl}{enter}")
            If tempStr = "" Then
                Me.Cb_PreSpKey.Text = "(无)"
            Else
                newPreSpKey = tempStr
                Me.Cb_PreSpKey.Items.Add(newPreSpKey)
                Me.Cb_PreSpKey.Items.Remove("自定义")
                Me.Cb_PreSpKey.Text = newPreSpKey
                Me.Cb_PreSpKey.Items.Add("自定义")
            End If
        End If
        Me.LoopTimer.Enabled = True
    End Sub

    Private Sub Tb_SpFreq_LostFocus(sender As Object, e As RoutedEventArgs) Handles Tb_SpFreq.LostFocus
        Dim temp As Integer
        Try
            temp = Int(Me.Tb_SpFreq.Text)
        Catch ex As Exception
            temp = 0
        End Try
        Me.Tb_SpFreq.Text = temp
    End Sub

    ''' <summary>
    ''' 转换按键值
    ''' </summary>
    ''' <param name="Key">源按键值</param>
    Public Shared Function ConvertKey(ByVal Key As String) As String
        Select Case Key
            Case "Enter"
                Return "{enter}"
            Case "Ctrl+Enter"
                Return "{ctrl}{enter}"
            Case "(无)"
                Return vbNullString
            Case Else
                Return Key
        End Select
    End Function

    Dim SpTool As New Sp With {.Owner = Me}

    Private Sub Bt_StartSp_Click(sender As Object, e As RoutedEventArgs) Handles Bt_StartSp.Click
        Try
            Dim LastTime As Long = Me.Tb_SpTime.Text * (20 + Me.Tb_SpFreq.Text) + 1000
            Dim MSGR = MsgBox("即将开始刷屏，预计耗时" + LastTime.ToString() + "毫秒，请在刷屏过程中关闭输入法并且将光标保持在聊天软件的输入框中。" + vbCrLf + "若您已准备完毕，请单击确定开始刷屏。", 65)
            If MSGR = MsgBoxResult.Ok Then
                Me.WindowState = WindowState.Minimized
                SpTool.StartSp(Me.Tb_SpContent.Text, Me.Tb_SpTime.Text, Me.Tb_SpFreq.Text, ConvertKey(Me.Cb_SpKey.Text), ConvertKey(Me.Cb_PreSpKey.Text))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainWindow_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Topmost = False
    End Sub
End Class
