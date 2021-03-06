Imports Microsoft.Win32.TaskScheduler
Module SettingsModul
    Public Sub FindTask(strTaskName As String)
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask(strTaskName)
            If tTask Is Nothing Then
                MsgBox("Scheduler failed to create !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                MsgBox("Scheduler successfully created !", MsgBoxStyle.Information, "MigrateToGDrive")
            End If
        End Using
    End Sub
    Public Sub WriteForAutoBackup(confPath As String, cliSrcPath As String, cliDestPath As String, cliDatePath As String, timePath As String)
        Dim trimSrc As String
        Dim trimDest As String
        Dim trimBak As String
        trimSrc = PathVal(confPath, 1).Replace("Source Directory: ", "")
        trimDest = PathVal(confPath, 2).Replace("Destination Directory: ", "")
        trimBak = PathVal(confPath, 3).Replace("Backup Preferences: ", "")
        CheckFileExist(cliSrcPath, trimSrc)
        CheckFileExist(cliDestPath, trimDest)
        If trimBak = "null" Then
            MsgBox("Please select backup preferences first !", MsgBoxStyle.Critical, "MigrateToGDrive")
        Else
            If trimBak = "Today" Then
                If File.Exists(cliDatePath) Then
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    File.Delete(cliDatePath)
                    File.Create(cliDatePath).Dispose()
                    Dim destWriter As New StreamWriter(cliDatePath, True)
                    Dim dt As Date = Today
                    destWriter.WriteLine(dt.ToString("MM-dd-yyyy"))
                    destWriter.Close()
                    If File.Exists(timePath) Then
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(timePath)
                        File.Create(timePath).Dispose()
                        Dim timeWriter As New StreamWriter(timePath, True)
                        timeWriter.WriteLine("Today")
                        timeWriter.Close()
                    Else
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(timePath)
                        File.Create(timePath).Dispose()
                        Dim timeWriter As New StreamWriter(timePath, True)
                        timeWriter.WriteLine("null")
                        timeWriter.Close()
                    End If
                End If
            Else
                File.Create(cliDatePath).Dispose()
                Dim destWriter As New StreamWriter(cliDatePath, True)
                Dim dt As Date = Today
                destWriter.WriteLine("Anytime")
                destWriter.Close()
                If File.Exists(timePath) Then
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    File.Delete(timePath)
                    File.Create(timePath).Dispose()
                    Dim timeWriter As New StreamWriter(timePath, True)
                    timeWriter.WriteLine("Anytime")
                    timeWriter.Close()
                Else
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    File.Delete(timePath)
                    File.Create(timePath).Dispose()
                    Dim timeWriter As New StreamWriter(timePath, True)
                    timeWriter.WriteLine("null")
                    timeWriter.Close()
                End If
            End If
        End If
    End Sub
    Public Function CustRepDurIntDaily(cmbx4 As String) As Integer
        Dim task As Integer
        If cmbx4 = "Disabled" Then
            task = 0
            Return task
        ElseIf cmbx4 = "5 Minutes" Then
            task = 5
            Return task
        ElseIf cmbx4 = "10 Minutes" Then
            task = 10
            Return task
        ElseIf cmbx4 = "15 Minutes" Then
            task = 15
            Return task
        ElseIf cmbx4 = "30 Minutes" Then
            task = 30
            Return task
        ElseIf cmbx4 = "1 Hours" Then
            task = 1
            Return task
        Else
            task = 0
            Return task
        End If
    End Function
    Public Function CustRepDurValDaily(cmbx5 As String) As Integer
        Dim repDurValue As Integer
        If cmbx5 = "Disabled" Then
            repDurValue = 0
            Return repDurValue = 0
        ElseIf cmbx5 = "15 Minutes" Then
            repDurValue = 15
            Return repDurValue
        ElseIf cmbx5 = "30 Minutes" Then
            repDurValue = 30
            Return repDurValue
        ElseIf cmbx5 = "1 Hours" Then
            repDurValue = 1
            Return repDurValue
        ElseIf cmbx5 = "12 Hours" Then
            repDurValue = 12
            Return repDurValue
        ElseIf cmbx5 = "1 Day" Then
            repDurValue = 1
            Return repDurValue
        Else
            repDurValue = 0
            Return repDurValue
        End If
    End Function
    Public Function CustRepDurIntDecDaily(cmbx4 As String) As Integer
        Dim repDurIntDec As Integer
        If cmbx4 = "Disabled" Then
            repDurIntDec = 0
            Return repDurIntDec
        ElseIf cmbx4 = "5 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx4 = "10 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx4 = "15 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx4 = "30 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx4 = "1 Hours" Then
            repDurIntDec = 2
            Return repDurIntDec
        Else
            repDurIntDec = 0
            Return repDurIntDec
        End If
    End Function
    Public Function CustRepDurValDecDaily(cmbx5 As String) As Integer
        Dim repDurValDec As Integer
        If cmbx5 = "Disabled" Then
            repDurValDec = 0
            Return repDurValDec
        ElseIf cmbx5 = "15 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf cmbx5 = "30 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf cmbx5 = "1 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf cmbx5 = "12 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf cmbx5 = "1 Day" Then
            repDurValDec = 3
            Return repDurValDec
        Else
            repDurValDec = 0
            Return repDurValDec
        End If
    End Function
    Public Function CustRepDurIntWeek(cmbx7 As String) As Integer
        Dim task As Integer
        If cmbx7 = "Disabled" Then
            task = 0
            Return task
        ElseIf cmbx7 = "5 Minutes" Then
            task = 5
            Return task
        ElseIf cmbx7 = "10 Minutes" Then
            task = 10
            Return task
        ElseIf cmbx7 = "15 Minutes" Then
            task = 15
            Return task
        ElseIf cmbx7 = "30 Minutes" Then
            task = 30
            Return task
        ElseIf cmbx7 = "1 Hours" Then
            task = 1
            Return task
        Else
            task = 0
            Return task
        End If
    End Function
    Public Function CustRepDurValWeek(cmbx6 As String) As Integer
        Dim repDurValue As Integer
        If cmbx6 = "Disabled" Then
            repDurValue = 0
            Return repDurValue = 0
        ElseIf cmbx6 = "15 Minutes" Then
            repDurValue = 15
            Return repDurValue
        ElseIf cmbx6 = "30 Minutes" Then
            repDurValue = 30
            Return repDurValue
        ElseIf cmbx6 = "1 Hours" Then
            repDurValue = 1
            Return repDurValue
        ElseIf cmbx6 = "12 Hours" Then
            repDurValue = 12
            Return repDurValue
        ElseIf cmbx6 = "1 Day" Then
            repDurValue = 1
            Return repDurValue
        Else
            repDurValue = 0
            Return repDurValue
        End If
    End Function
    Public Function CustRepDurIntDecWeek(cmbx7 As String) As Integer
        Dim repDurIntDec As Integer
        If cmbx7 = "Disabled" Then
            repDurIntDec = 0
            Return repDurIntDec
        ElseIf cmbx7 = "5 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx7 = "10 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx7 = "15 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx7 = "30 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf cmbx7 = "1 Hours" Then
            repDurIntDec = 2
            Return repDurIntDec
        Else
            repDurIntDec = 0
            Return repDurIntDec
        End If
    End Function
    Public Function CustRepDurValDecWeek(cmbx6 As String) As Integer
        Dim repDurValDec As Integer
        If cmbx6 = "Disabled" Then
            repDurValDec = 0
            Return repDurValDec
        ElseIf cmbx6 = "15 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf cmbx6 = "30 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf cmbx6 = "1 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf cmbx6 = "12 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf cmbx6 = "1 Day" Then
            repDurValDec = 3
            Return repDurValDec
        Else
            repDurValDec = 0
            Return repDurValDec
        End If
    End Function
    Public Sub DailyTrigger(custDate As String, repDayInt As Integer, custInt As Integer, custrepdur As Integer, custrepint As Integer, cmbx5 As String, cmbx4 As String)
        Dim appPath As String = Application.StartupPath()
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask("MigrateToGDrive")
            Dim repDurVal As Integer = CustRepDurValDecDaily(cmbx5)
            Dim repDurInt As Integer = CustRepDurIntDecDaily(cmbx4)
            Dim tDefinition As TaskDefinition = tService.NewTask
            Dim tTrigger As New DailyTrigger()
            If tTask Is Nothing Then
                tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Daily Task"
                tTrigger.StartBoundary = custDate
                If repDayInt = 1 Then
                    tTrigger.DaysInterval = custInt
                End If
                If repDurInt = 1 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                ElseIf repDurInt = 2 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                End If
                If repDurVal = 1 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                ElseIf repDurVal = 2 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                ElseIf repDurVal = 3 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                End If
                tDefinition.Triggers.Add(tTrigger)
                tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                FindTask("MigrateToGDrive")
            Else
                Dim validation As Integer
                validation = MsgBox("Old scheduler already exist !, Create a new scheduler ?", vbExclamation + vbYesNo + vbDefaultButton2, "MigrateToGDrive")
                If validation = vbYes Then
                    tService.RootFolder.DeleteTask("MigrateToGDrive")
                    tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Daily Task"
                    tTrigger.StartBoundary = custDate
                    If repDayInt = 1 Then
                        tTrigger.DaysInterval = custInt
                    End If
                    If repDurInt = 1 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                    ElseIf repDurInt = 2 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                    End If
                    If repDurVal = 1 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                    ElseIf repDurVal = 2 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                    ElseIf repDurVal = 3 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                    End If
                    tDefinition.Triggers.Add(tTrigger)
                    tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                    tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                    FindTask("MigrateToGDrive")
                Else
                    MsgBox("Cancel To Created Scheduler !", MsgBoxStyle.Information, "MigrateToGDrive")
                End If
            End If
        End Using
    End Sub
    Public Sub WeeklyTrigger(custdate As String, repDayInt As Integer, custInt As Integer, custrepdur As Integer, custrepint As Integer, cb1 As DaysOfTheWeek, cb2 As DaysOfTheWeek, cb3 As DaysOfTheWeek, cb4 As DaysOfTheWeek, cb5 As DaysOfTheWeek, cb6 As DaysOfTheWeek, cb7 As DaysOfTheWeek, cmbx6 As String, cmbx7 As String)
        Dim appPath As String = Application.StartupPath()
        Dim repDurVal As Integer = CustRepDurValDecWeek(cmbx6)
        Dim repDurInt As Integer = CustRepDurIntDecWeek(cmbx7)
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask("MigrateToGDrive")
            Dim tDefinition As TaskDefinition = tService.NewTask
            Dim tTrigger As New WeeklyTrigger()
            If tTask Is Nothing Then
                tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Weekly Task"
                tTrigger.StartBoundary = custdate
                tTrigger.DaysOfWeek = cb1 Or cb2 Or cb3 Or cb4 Or cb5 Or cb6 Or cb7
                If repDayInt = 1 Then
                    tTrigger.WeeksInterval = custInt
                End If
                If repDurInt = 1 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                ElseIf repDurInt = 2 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                End If
                If repDurVal = 1 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                ElseIf repDurVal = 2 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                ElseIf repDurVal = 3 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                End If
                tDefinition.Triggers.Add(tTrigger)
                tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                FindTask("MigrateToGDrive")
            Else
                Dim validation As Integer
                validation = MsgBox("Old scheduler already exist !, Create a new scheduler ?", vbExclamation + vbYesNo + vbDefaultButton2, "MigrateToGDrive")
                If validation = vbYes Then
                    tService.RootFolder.DeleteTask("MigrateToGDrive")
                    tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Weekly Task"
                    tTrigger.StartBoundary = custdate
                    tTrigger.DaysOfWeek = cb1 Or cb2 Or cb3 Or cb4 Or cb5 Or cb6 Or cb7
                    If repDayInt = 1 Then
                        tTrigger.WeeksInterval = custInt
                    End If
                    If repDurInt = 1 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                    ElseIf repDurInt = 2 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                    End If
                    If repDurVal = 1 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                    ElseIf repDurVal = 2 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                    ElseIf repDurVal = 3 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                    End If
                    tDefinition.Triggers.Add(tTrigger)
                    tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                    tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                    FindTask("MigrateToGDrive")
                Else
                    MsgBox("Cancel created scheduler !", MsgBoxStyle.Information, "MigrateToGDrive")
                End If
            End If
        End Using
    End Sub
End Module