Imports System.Management

Module RebootChecker
    Sub Main(args As String())
        Dim minutes As Integer = 5
        If args.Length > 0 Then
            If args(0).Equals("/?") Or args(0).Equals("-?") Then
                ShowHelp()
                Return
            End If

            If Integer.TryParse(args(0), minutes) = False Then
                Console.WriteLine("Invalid parameter. Using default value of 5 minutes.")
                minutes = 5
            End If
        End If

        Try
            Dim rebooted As Boolean = CheckIfRebooted(minutes)
            Console.WriteLine("System rebooted within {0} minutes: {1}", minutes, rebooted)
        Catch ex As Exception
            Console.WriteLine("An error occurred: " & ex.Message)
        End Try
    End Sub

    Sub ShowHelp()
        Console.WriteLine("Usage: RebootChecker [minutes]")
        Console.WriteLine("Checks if the system was rebooted within the specified number of minutes.")
        Console.WriteLine("If no parameter is provided, it defaults to 5 minutes.")
    End Sub

    Function CheckIfRebooted(minutes As Integer) As Boolean
        Dim searcher As New ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem")
        For Each os As ManagementObject In searcher.Get()
            Dim lastBootUpTime As DateTime = ManagementDateTimeConverter.ToDateTime(os("LastBootUpTime").ToString())
            If (DateTime.Now - lastBootUpTime).TotalMinutes <= minutes Then
                Return True
            End If
        Next
        Return False
    End Function
End Module
