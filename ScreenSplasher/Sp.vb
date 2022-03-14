Imports System.Windows.Forms
Imports System.Drawing

Public Class Sp

    Private TaskBarManger As New Shell.TaskbarItemInfo()

    ''' <summary>
    ''' 用于刷屏的Timer
    ''' </summary>
    WithEvents SpTimer As New System.Windows.Forms.Timer With {.Enabled = False}

    Public Property SpCOntent As String = vbNullString
    Public Property SpTime As UInteger = 1
    Public Property SpKey As String = "{enter}"
    Public Property preSpKey As String = ""

    Public Property Owner As MainWindow

    ''' <summary>
    ''' 开始刷屏
    ''' </summary>
    ''' <param name="Content">刷屏内容(留空为Ctrl+V)</param>
    ''' <param name="Time">刷屏次数</param>
    ''' <param name="Freq">刷屏间隔时间</param>
    ''' <param name="SpKey">消息发送按键，默认为{enter}</param>
    ''' <param name="preSpKey">消息输入前按键，默认为空</param>
    Sub StartSp(ByVal Content As String, ByVal Time As UInteger, Optional Freq As UInteger = 0, Optional SpKey As String = "{enter}", Optional preSpKey As String = "")
        Dim NoticeForm As New Form With {
            .Top = 0,
            .Left = 0,
            .Width = 256,
            .Height = 60,
            .Font = New Font("Microsoft YaHei", 18, FontStyle.Regular),
            .FormBorderStyle = FormBorderStyle.None,
            .TopMost = True,
            .BackColor = Color.AliceBlue,
            .StartPosition = FormStartPosition.Manual,
            .AllowTransparency = True,
            .Opacity = 0.8
        }
        Dim NoticeLabel As New Label With {
            .Top = 0,
            .Left = 0,
            .Width = 256,
            .Height = 40,
            .Font = New Font("Microsoft YaHei", 18, FontStyle.Regular),
            .Text = "0/" + Time.ToString() + "已完成",
            .BackColor = Color.AliceBlue
        }
        Dim NoticeLabel2 As New Label With {
            .Top = 40,
            .Left = 0,
            .Width = 256,
            .Height = 20,
            .Font = New Font("Microsoft YaHei", 8, FontStyle.Regular),
            .Text = "——来自HIM Best刷屏工具的提示",
            .BackColor = Color.AliceBlue,
            .ForeColor = Color.DarkGray
        }
        NoticeForm.Controls.Add(NoticeLabel)
        NoticeForm.Controls.Add(NoticeLabel2)
        NoticeForm.Show()
        Sleep(1000)
        Try
            If Freq = 0 Then
                For i = 0 To Time - 1
                    SendKeys.SendWait(preSpKey)
                    If Content = "[在此处输入刷屏内容]" Then
                        SendKeys.SendWait("^v")
                    Else
                        SendKeys.SendWait(Content)
                    End If
                    NoticeLabel.Text = (i + 1).ToString() + "/" + Time.ToString() + "已完成"
                    SendKeys.SendWait(MainWindow.ConvertKey(SpKey))
                Next
            Else
                For i = 0 To Time - 1
                    SendKeys.SendWait(preSpKey)
                    If Content = "[在此处输入刷屏内容]" Then
                        SendKeys.SendWait("^v")
                    Else
                        Clipboard.SetText(Content)
                        SendKeys.SendWait("^v")
                    End If
                    SendKeys.SendWait(MainWindow.ConvertKey(SpKey))
                    Sleep(Freq)
                    NoticeLabel.Text = (i + 1).ToString() + "/" + Time.ToString() + "已完成"
                Next
            End If
            Owner.WindowState = WindowState.Normal
            Owner.Topmost = True
            MsgBox("刷屏完成!", 64, "HIM Best刷屏工具")
        Catch ex As Exception
            MsgBox("在进行刷屏时发生了错误，一般是由于使用了填写错误的刷屏按键所导致。" + vbLf + "详细信息：" + ex.ToString(), 16, "HIM Best刷屏工具")
        End Try
        NoticeForm.Close()
        Try
            NoticeForm.Dispose()
        Finally
        End Try
    End Sub

    Private Declare Sub Sleep Lib "kernel32.dll" (ByVal dwMilliseconds As Long)
    Sub WaitOnSp()

    End Sub

    Dim SpedTime As Long = 0
    Sub SpLoop() Handles SpTimer.Tick
        Try
            If SpedTime = 0 Then
                Me.SpTimer.Stop()
            Else
                SendKeys.SendWait(Me.preSpKey)
                If Me.SpCOntent = "[在此处输入刷屏内容]" Then
                    SendKeys.SendWait("^v")
                Else
                    SendKeys.SendWait(Me.SpCOntent)
                End If
                SendKeys.SendWait(MainWindow.ConvertKey(Me.SpKey))
                SpedTime -= 1
            End If
        Catch ex As Exception
            MsgBox("在进行刷屏时发生了错误，一般是由于使用了填写错误的刷屏按键所导致。" + vbLf + "详细信息：" + ex.ToString(), 16, "HIM Best刷屏工具")
        End Try
    End Sub
End Class
