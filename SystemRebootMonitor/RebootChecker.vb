Imports System.Diagnostics

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
            Environment.Exit(If(rebooted, 0, 1))
        Catch ex As Exception
            Console.WriteLine("An error occurred: " & ex.Message)
            Environment.Exit(1)
        End Try
    End Sub

    Sub ShowHelp()
        Console.WriteLine("Usage: RebootChecker [minutes]")
        Console.WriteLine("Checks if the system was rebooted within the specified number of minutes.")
        Console.WriteLine("If no parameter is provided, it defaults to 5 minutes.")
    End Sub

    Function CheckIfRebooted(minutes As Integer) As Boolean
        Try
            Dim lastBootTime As DateTime = GetLastBootTime()
            If (DateTime.Now - lastBootTime).TotalMinutes <= minutes Then
                Return True
            End If
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
        Return False
    End Function

    Function GetLastBootTime() As DateTime
        Dim eventLog As New EventLog("System")
        Dim bootEventIDs As Integer() = {12, 13, 6005, 6006, 6013}

        For i As Integer = eventLog.Entries.Count - 1 To 0 Step -1
            Dim entry As EventLogEntry = eventLog.Entries(i)
            If bootEventIDs.Contains(entry.EventID) Then
                Return entry.TimeGenerated
            End If
        Next

        Throw New Exception("Unable to retrieve last boot time from event log. Ensure the event log contains startup events with IDs: " & String.Join(", ", bootEventIDs))
    End Function
End Module
