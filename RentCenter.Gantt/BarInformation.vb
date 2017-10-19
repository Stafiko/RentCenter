Public Class BarInformation
    Public Property BarText As String
    Public Property RowText As String
    Public Property FromTime As Date
    Public Property ToTime As Date
    Public Property Color As Color
    Public Property HoverColor As Color
    Public Property RowIndex As Integer

    Public Sub New(rowText As String, barText As String, fromTime As Date, totime As Date, color As Color, hoverColor As Color, ByVal rowIndex As Integer)
        Me.RowText = rowText
        Me.BarText = barText
        Me.FromTime = fromTime
        Me.ToTime = totime
        Me.Color = color
        Me.HoverColor = hoverColor
        Me.RowIndex = rowIndex
    End Sub
End Class