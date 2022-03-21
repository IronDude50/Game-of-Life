' John Conway's Game of Life
' Version 5.3
' Last updated on 15th Dec 2021

Imports System

Module Program
    Public shape As String
    Public intGrid As Integer = 20000
    Public intRepitions As Double = Double.PositiveInfinity

    Public blnArrCells(intGrid - 1, intGrid - 1) As Integer
    Public arrNeighbours(intGrid - 1, intGrid - 1) As Integer
    ' Declaring two by two arrays

    Public intCol As Integer
    Public intRow As Integer

    Public title As String = ("JOHN CONWAY'S GAME OF LIFE" & vbCrLf & "BY DHRUV MENON" & vbCrLf)
    Public intro As String = ("This simulation is a recreation of John Conway's Game of life. It works by forming a grid containing dead (-) and live (X) cells (at random). Each stage of evolution obeys certain laws:" & vbCrLf & "1) If a cell has less than 2 neighbouring live cells, the cell will die (or stay dead)." & vbCrLf & "2) If a cell has 2 neighbouring live cells, the cell will remain in its current form." & vbCrLf & "3) If a cell has 3 neighbouring live cells, the cell will come alive (or stay alive)." & vbCrLf & "4) If a cell has more than 3 neighbouring live cells, the cell will die (or stay dead)." & vbCrLf & "" & vbCrLf & "Enter your desired grid size below (min = 2, max = 20000). Enter os for oscillators, gl for a glider, ls for light-weight spaceship, ms for middle-weight spaceship, hs for heavyweight spaceship or gg for gospel's glider gun. Then, click any key to start.")

    Sub Main(args As String())
        Dim blnValidated As Boolean = False

        Console.ForegroundColor = ConsoleColor.Cyan
        ' Changes the colour of the text
        Console.Title = "John Conway's Game of Life (by Dhruv Menon)"
        ' Changes the name of the console
        Console.WriteLine(title)
        Console.WriteLine(intro)

        While blnValidated = False
            shape = Console.ReadLine()

            If shape = "os" Then
                blnValidated = True
                Oscillators()
            ElseIf shape = "gl" Then
                blnValidated = True
                Glider()
            ElseIf shape = "ls" Then
                blnValidated = True
                LightWeightSpaceship()
            ElseIf shape = "ms" Then
                blnValidated = True
                MiddleWeightSpaceship()
            ElseIf shape = "hs" Then
                blnValidated = True
                HeavyWeightSpaceship()
            ElseIf shape = "gg" Then
                blnValidated = True
                GospelGliderGun()
            ElseIf IsNumeric(shape) = True Then
                If shape <= 20000 And shape >= 2 Then
                    blnValidated = True
                    intGrid = shape
                    Initialise()
                    ' Assigns random values at the start
                Else
                    blnValidated = False

                    Console.ForegroundColor = ConsoleColor.DarkRed
                    Console.WriteLine("Please enter a valid input.")
                    Console.ForegroundColor = ConsoleColor.Cyan
                End If
            Else
                blnValidated = False

                Console.ForegroundColor = ConsoleColor.DarkRed
                Console.WriteLine("Please enter a valid input.")
                Console.ForegroundColor = ConsoleColor.Cyan
            End If
        End While
        ' Validation

        Console.Clear()
        Draw()
        Console.ReadKey()
        ' Waits for a key to be pressed until the next iteration is performed
        Console.Clear()

        For i = 1 To intRepitions
            AllNeighbours()
            Evolution()
            Draw()

            If IsNumeric(shape) = False Then
                Threading.Thread.Sleep(1)
                ' Waits for a certain amount of time
            Else
                Threading.Thread.Sleep(300)
            End If

            Console.Clear()
        Next
        ' Loops as many times as required
    End Sub

    Sub Initialise()
        Randomize()
        ' Randomize function for random number (below)

        For intCol = 0 To intGrid - 1
            For intRow = 0 To intGrid - 1
                If CInt(Rnd() * 2) = 1 Then
                    blnArrCells(intCol, intRow) = 1
                Else
                    blnArrCells(intCol, intRow) = 0
                End If
            Next
        Next
        ' Assigning random values for grid
    End Sub

    Function CountNeighbours(intCol, intRow)
        Dim intNeighbourCount As Integer = blnArrCells(intCol, intRow) * -1

        For intLocalCol = intCol - 1 To intCol + 1
            For intLocalRow = intRow - 1 To intRow + 1
                intNeighbourCount += blnArrCells(intLocalCol, intLocalRow)
            Next
        Next

        Return intNeighbourCount
    End Function

    Sub AllNeighbours()
        For intCol = 1 To intGrid - 1
            For intRow = 1 To intGrid - 1
                arrNeighbours(intCol, intRow) = CountNeighbours(intCol, intRow)
            Next
        Next
    End Sub

    Sub Evolution()
        For intCol = 0 To intGrid - 1
            For intRow = 0 To intGrid - 1
                ' intNeighbourCount < 2 : DIE
                ' intNeighbourCount = 2 : LIVE
                ' intNeighbourCount = 3 : BREED
                ' intNeighbourCount > 3 : DIE

                Select Case arrNeighbours(intCol, intRow)
                    Case 2
                        blnArrCells(intCol, intRow) = blnArrCells(intCol, intRow)
                    Case 3
                        blnArrCells(intCol, intRow) = 1
                    Case Else
                        blnArrCells(intCol, intRow) = 0
                End Select
                ' Copies all contents of arrNeighbours into blnArrCells at the end
            Next
        Next


        ' Assigning dead cells for borders of grid
    End Sub

    Sub Draw()
        For intCol = 0 To intGrid - 1
            For intRow = 0 To intGrid - 1
                If blnArrCells(intCol, intRow) = 1 Then
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.Write(" X ")
                ElseIf blnArrCells(intCol, intRow) = 0 Then
                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.Write(" - ")
                End If
            Next
            ' Drawing in grid (based on code in Main)

            Console.WriteLine("")
            ' Shifts to the next line
        Next
        Console.WriteLine("")
    End Sub

    Sub Oscillators()
        intGrid = 29

        blnArrCells(1, 17) = 1
        blnArrCells(1, 23) = 1
        blnArrCells(2, 1) = 1
        blnArrCells(2, 2) = 1
        blnArrCells(2, 3) = 1
        blnArrCells(2, 7) = 1
        blnArrCells(2, 8) = 1
        blnArrCells(2, 9) = 1
        blnArrCells(2, 17) = 1
        blnArrCells(2, 23) = 1
        blnArrCells(3, 6) = 1
        blnArrCells(3, 7) = 1
        blnArrCells(3, 8) = 1
        blnArrCells(3, 17) = 1
        blnArrCells(3, 18) = 1
        blnArrCells(3, 22) = 1
        blnArrCells(3, 23) = 1
        blnArrCells(5, 13) = 1
        blnArrCells(5, 14) = 1
        blnArrCells(5, 15) = 1
        blnArrCells(5, 18) = 1
        blnArrCells(5, 19) = 1
        blnArrCells(5, 21) = 1
        blnArrCells(5, 22) = 1
        blnArrCells(5, 25) = 1
        blnArrCells(5, 26) = 1
        blnArrCells(5, 27) = 1
        blnArrCells(6, 1) = 1
        blnArrCells(6, 2) = 1
        blnArrCells(6, 15) = 1
        blnArrCells(6, 17) = 1
        blnArrCells(6, 19) = 1
        blnArrCells(6, 21) = 1
        blnArrCells(6, 23) = 1
        blnArrCells(6, 25) = 1
        blnArrCells(7, 1) = 1
        blnArrCells(7, 17) = 1
        blnArrCells(7, 18) = 1
        blnArrCells(7, 22) = 1
        blnArrCells(7, 23) = 1
        blnArrCells(8, 4) = 1
        blnArrCells(8, 8) = 1
        blnArrCells(9, 3) = 1
        blnArrCells(9, 4) = 1
        blnArrCells(9, 8) = 1
        blnArrCells(9, 17) = 1
        blnArrCells(9, 18) = 1
        blnArrCells(9, 22) = 1
        blnArrCells(9, 23) = 1
        blnArrCells(10, 15) = 1
        blnArrCells(10, 17) = 1
        blnArrCells(10, 19) = 1
        blnArrCells(10, 21) = 1
        blnArrCells(10, 23) = 1
        blnArrCells(10, 25) = 1
        blnArrCells(11, 8) = 1
        blnArrCells(11, 13) = 1
        blnArrCells(11, 14) = 1
        blnArrCells(11, 15) = 1
        blnArrCells(11, 18) = 1
        blnArrCells(11, 19) = 1
        blnArrCells(11, 21) = 1
        blnArrCells(11, 22) = 1
        blnArrCells(11, 25) = 1
        blnArrCells(11, 26) = 1
        blnArrCells(11, 27) = 1
        blnArrCells(12, 7) = 1
        blnArrCells(12, 9) = 1
        blnArrCells(13, 17) = 1
        blnArrCells(13, 18) = 1
        blnArrCells(13, 22) = 1
        blnArrCells(13, 23) = 1
        blnArrCells(14, 7) = 1
        blnArrCells(14, 8) = 1
        blnArrCells(14, 9) = 1
        blnArrCells(14, 17) = 1
        blnArrCells(14, 23) = 1
        blnArrCells(15, 17) = 1
        blnArrCells(15, 23) = 1
        blnArrCells(17, 7) = 1
        blnArrCells(17, 8) = 1
        blnArrCells(17, 9) = 1
        blnArrCells(19, 7) = 1
        blnArrCells(19, 9) = 1
        blnArrCells(20, 8) = 1
        blnArrCells(22, 8) = 1
        blnArrCells(23, 8) = 1
    End Sub

    Sub Glider()
        intGrid = 10

        blnArrCells(1, 2) = 1
        blnArrCells(2, 3) = 1
        blnArrCells(3, 1) = 1
        blnArrCells(3, 2) = 1
        blnArrCells(3, 3) = 1
    End Sub

    Sub LightWeightSpaceship()
        intGrid = 15

        blnArrCells(5, 2) = 1
        blnArrCells(5, 3) = 1
        blnArrCells(6, 1) = 1
        blnArrCells(6, 2) = 1
        blnArrCells(6, 3) = 1
        blnArrCells(6, 4) = 1
        blnArrCells(7, 1) = 1
        blnArrCells(7, 2) = 1
        blnArrCells(7, 4) = 1
        blnArrCells(7, 5) = 1
        blnArrCells(8, 3) = 1
        blnArrCells(8, 4) = 1
    End Sub

    Sub MiddleWeightSpaceship()
        intGrid = 18

        blnArrCells(6, 3) = 1
        blnArrCells(7, 1) = 1
        blnArrCells(7, 5) = 1
        blnArrCells(8, 6) = 1
        blnArrCells(9, 1) = 1
        blnArrCells(9, 6) = 1
        blnArrCells(10, 2) = 1
        blnArrCells(10, 3) = 1
        blnArrCells(10, 4) = 1
        blnArrCells(10, 5) = 1
        blnArrCells(10, 6) = 1
    End Sub

    Sub HeavyWeightSpaceship()
        intGrid = 21

        blnArrCells(8, 2) = 1
        blnArrCells(8, 3) = 1
        blnArrCells(8, 4) = 1
        blnArrCells(8, 5) = 1
        blnArrCells(8, 6) = 1
        blnArrCells(8, 7) = 1
        blnArrCells(9, 1) = 1
        blnArrCells(9, 7) = 1
        blnArrCells(10, 7) = 1
        blnArrCells(11, 1) = 1
        blnArrCells(11, 6) = 1
        blnArrCells(12, 3) = 1
        blnArrCells(12, 4) = 1
    End Sub

    Sub GospelGliderGun()
        intGrid = 38

        blnArrCells(1, 25) = 1
        blnArrCells(1, 27) = 1
        blnArrCells(2, 24) = 1
        blnArrCells(2, 27) = 1
        blnArrCells(3, 14) = 1
        blnArrCells(3, 23) = 1
        blnArrCells(3, 24) = 1
        blnArrCells(3, 35) = 1
        blnArrCells(3, 36) = 1
        blnArrCells(4, 13) = 1
        blnArrCells(4, 15) = 1
        blnArrCells(4, 21) = 1
        blnArrCells(4, 22) = 1
        blnArrCells(4, 26) = 1
        blnArrCells(4, 35) = 1
        blnArrCells(4, 36) = 1
        blnArrCells(5, 1) = 1
        blnArrCells(5, 2) = 1
        blnArrCells(5, 13) = 1
        blnArrCells(5, 14) = 1
        blnArrCells(5, 16) = 1
        blnArrCells(5, 23) = 1
        blnArrCells(5, 24) = 1
        blnArrCells(5, 30) = 1
        blnArrCells(5, 31) = 1
        blnArrCells(6, 1) = 1
        blnArrCells(6, 2) = 1
        blnArrCells(6, 13) = 1
        blnArrCells(6, 14) = 1
        blnArrCells(6, 16) = 1
        blnArrCells(6, 17) = 1
        blnArrCells(6, 24) = 1
        blnArrCells(6, 27) = 1
        blnArrCells(6, 32) = 1
        blnArrCells(7, 13) = 1
        blnArrCells(7, 14) = 1
        blnArrCells(7, 16) = 1
        blnArrCells(7, 25) = 1
        blnArrCells(7, 27) = 1
        blnArrCells(8, 13) = 1
        blnArrCells(8, 15) = 1
        blnArrCells(9, 14) = 1
    End Sub
End Module
