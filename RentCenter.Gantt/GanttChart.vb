Imports System.Drawing.Drawing2D

Public Class GanttChart
    Inherits Control

    Private _mouseHoverPart As MouseOverPart = MouseOverPart.Empty
    Private _mouseHoverBarIndex As Integer = -1

    Private _bars As New List(Of ChartBarDate)
    Private _headerFromDate As Date = Nothing
    Private _headerToDate As Date = Nothing

    Private _barIsChanging As Integer = -1

    Private _barStartRight As Integer = 20
    Private _barStartLeft As Integer = 100
    Private _headerTimeStartTop As Integer = 30
    Private _shownHeaderList As List(Of Header)

    Private _barStartTop As Integer = 50
    Private _barHeight As Integer = 9
    Private _barSpace As Integer = 5

    Private _widthPerItem As Integer

    Private _mouseOverColumnValue As Date = Nothing
    Private _mouseOverRowText As String = ""
    Private _mouseOverRowValue As Object = Nothing

    Private _lineColor As Pen = Pens.Bisque
    Private _dateTextFont As Font = New Font("VERDANA", 8.0, FontStyle.Regular, GraphicsUnit.Point)
    Private _timeTextFont As Font = New Font("VERDANA", 8.0, FontStyle.Regular, GraphicsUnit.Point)
    Private _rowTextFont As Font = New Font("VERDANA", 8.0, FontStyle.Regular, GraphicsUnit.Point)

    Friend WithEvents ToolTip As New ToolTip()

    Public Event MouseDragged(sender As Object,e As MouseEventArgs)
    Public Event BarChanged(sender As Object, ByRef barValue As Object)

    Private _objBmp As Bitmap
    Private _objGraphics As Graphics

#Region "Public properties"

    Public Property AllowManualEditBar As Boolean = False

    Public Property FromDate As Date
        Get
            Return _headerFromDate
        End Get
        Set
            _headerFromDate = value
        End Set
    End Property

    Public Property ToDate As Date
        Get
            Return _headerToDate
        End Get
        Set
            _headerToDate = value
        End Set
    End Property


    Public ReadOnly Property MouseOverRowText As String
        Get
            Return _mouseOverRowText
        End Get
    End Property

    Public ReadOnly Property MouseOverRowValue As Object
        Get
            Return _mouseOverRowValue
        End Get
    End Property

    Public ReadOnly Property MouseOverColumnDate As Date
        Get
            Return _mouseOverColumnValue
        End Get
    End Property

    Public Property GridColor As Pen
        Get
            Return _lineColor
        End Get
        Set
            _lineColor = value
        End Set
    End Property

    Public Property RowFont As Font
        Get
            Return _rowTextFont
        End Get
        Set
            _rowTextFont = value
        End Set
    End Property

    Public Property DateFont As Font
        Get
            Return _dateTextFont
        End Get
        Set
            _dateTextFont = value
        End Set
    End Property

    Public Property TimeFont As Font
        Get
            Return _timeTextFont
        End Get
        Set
            _timeTextFont = value
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New()
        ToolTip.AutoPopDelay = 15000
        ToolTip.InitialDelay = 250
        ToolTip.OwnerDraw = True

        _objBmp = New Bitmap(1280, 1024, Imaging.PixelFormat.Format24bppRgb)
        _objGraphics = Graphics.FromImage(_objBmp)

        SetStyle(ControlStyles.DoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint, True)
    End Sub

#End Region

#Region "Bars"

    Private Sub SetBarStartLeft(rowText As String)
        Dim gfx As Graphics = CreateGraphics

        Dim length As Integer = gfx.MeasureString(rowText, _rowTextFont, 500).Width

        If length > _barStartLeft Then
            _barStartLeft = length
        End If
    End Sub

    Public Sub AddChartBar(rowText As String, barText As String, barValue As Object, fromTime As Date, toTime As Date, color As Color, hoverColor As Color, rowIndex As Integer)
        Dim bar As New ChartBarDate
        bar.Name = barText
        bar.Text = rowText
        bar.Value = barValue
        bar.StartValue = fromTime
        bar.EndValue = toTime
        bar.Color = color
        bar.HoverColor = hoverColor
        bar.RowIndex = rowIndex
        _bars.Add(bar)

        SetBarStartLeft(rowText)
    End Sub

    Public Sub AddChartBar(rowText As String, barText As String, barValue As Object, fromTime As Date, toTime As Date, color As Color, hoverColor As Color, rowIndex As Integer, hideFromMouseMove As Boolean)
        Dim bar As New ChartBarDate
        bar.Name = barText
        bar.Text = rowText
        bar.Value = barValue
        bar.StartValue = fromTime
        bar.EndValue = toTime
        bar.Color = color
        bar.HoverColor = hoverColor
        bar.RowIndex = rowIndex
        bar.HideFromMouseMove = hideFromMouseMove
        _bars.Add(bar)

        SetBarStartLeft(rowText)
    End Sub

    Public Function GetIndexChartBar(rowText As String) As Integer
        Dim index As Integer = -1

        For Each bar As ChartBarDate In _bars
            If bar.Text.Equals(rowText) = True Then
                Return bar.RowIndex
            End If
            If bar.RowIndex > index Then
                index = bar.RowIndex
            End If
        Next

        Return index + 1
    End Function

    Public Sub RemoveBars()
        _bars = New List(Of ChartBarDate)

        _barStartLeft = 100
    End Sub

#End Region

#Region "Draw"

    Public Sub PaintChart()
        Invalidate()
    End Sub

    Private Sub PaintChart(gfx As Graphics)
        gfx.Clear(BackColor)

        If _headerFromDate = Nothing Or _headerToDate = Nothing Then Exit Sub

        DrawScrollBar(gfx)
        DrawHeader(gfx, Nothing)
        DrawNetHorizontal(gfx)
        DrawNetVertical(gfx)
        DrawBars(gfx)

        _objBmp = New Bitmap(Width - _barStartRight, _lastLineStop, Imaging.PixelFormat.Format24bppRgb)
        _objGraphics = Graphics.FromImage(_objBmp)
    End Sub

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        MyBase.OnPaint(pe)
        PaintChart(pe.Graphics)
    End Sub

    Private Sub DrawHeader(gfx As Graphics, headerList As List(Of Header))
        If headerList Is Nothing Then
            headerList = GetFullHeaderList()
        End If

        If headerList.Count = 0 Then Exit Sub

        Dim availableWidth = Width - 10 - _barStartLeft - _barStartRight
        _widthPerItem = availableWidth / headerList.Count

        If _widthPerItem < 40 Then
            Dim newHeaderList As New List(Of Header)

            Dim showNext As Boolean = True

            For Each header As Header In headerList
                If showNext = True Then
                    newHeaderList.Add(header)
                    showNext = False
                Else
                    showNext = True
                End If
            Next

            DrawHeader(gfx, newHeaderList)
            Exit Sub
        End If

        Dim index As Integer = 0
        Dim lastHeader As Header = Nothing

        For Each header As Header In headerList
            Dim startPos As Integer = _barStartLeft + (index * _widthPerItem)
            Dim showDateHeader As Boolean = False

            header.StartLocation = startPos

            If lastHeader Is Nothing Then
                showDateHeader = True
            ElseIf header.Time.Hour < lastHeader.Time.Hour Then
                showDateHeader = True
            ElseIf header.Time.Minute = lastHeader.Time.Minute Then
                showDateHeader = True
            End If

            If showDateHeader = True Then
                Dim str As String

                If header.HeaderTextInsteadOfTime.Length > 0 Then
                    str = header.HeaderTextInsteadOfTime
                Else
                    str = header.Time.ToString("d-MMM")
                End If
                gfx.DrawString(str, _dateTextFont, Brushes.Black, startPos, 0)
            End If

            gfx.DrawString(header.HeaderText, _timeTextFont, Brushes.Black, startPos, _headerTimeStartTop)
            index += 1

            lastHeader = header
        Next

        _shownHeaderList = headerList
        _widthPerItem = (Width - 10 - _barStartLeft - _barStartRight) / _shownHeaderList.Count
    End Sub

    Private Sub DrawBars(grfx As Graphics, Optional ByVal ignoreScrollAndMousePosition As Boolean = False)
        If _shownHeaderList Is Nothing Then Exit Sub
        If _shownHeaderList.Count = 0 Then Exit Sub

        Dim index As Integer

        Dim timeBetween As TimeSpan = _shownHeaderList(1).Time - _shownHeaderList(0).Time
        Dim minutesBetween As Integer = (timeBetween.TotalMinutes) 
        Dim widthBetween = (_shownHeaderList(1).StartLocation - _shownHeaderList(0).StartLocation)
        Dim perMinute As Decimal = widthBetween / minutesBetween

        For Each bar As ChartBarDate In _bars
            index = bar.RowIndex

            Dim startLocation As Integer
            Dim startMinutes As Integer 
            Dim startTimeSpan As TimeSpan
            Dim lengthMinutes As Integer

            Dim scrollPos As Integer = 0

            If ignoreScrollAndMousePosition = False Then
                scrollPos = _scrollPosition
            End If

            startTimeSpan = bar.StartValue - FromDate
            startMinutes = (startTimeSpan.Days * 1440) + (startTimeSpan.Hours * 60) + startTimeSpan.Minutes

            startLocation = perMinute * startMinutes

            Dim endValue As Date = bar.EndValue

            If endValue = Nothing Then
                endValue = Date.Now
            End If

            Dim lengthTimeSpan As TimeSpan = endValue - bar.StartValue
            lengthMinutes = (lengthTimeSpan.Days * 1440) + (lengthTimeSpan.Hours * 60) + lengthTimeSpan.Minutes

            Dim widths As Integer = perMinute * lengthMinutes

            Dim a As Integer = _barStartLeft + startLocation
            Dim b As Integer = _barStartTop + (_barHeight * (index - scrollPos)) + (_barSpace * (index - scrollPos)) + 2
            Dim c As Integer = widths
            Dim d As Integer = _barHeight

            If c = 0 Then c = 1

            If a - _barStartLeft < 0 Then
                a = _barStartLeft
            End If

            Dim color As Color

            If MouseOverRowText = bar.Text And bar.StartValue <= _mouseOverColumnValue And bar.EndValue >= _mouseOverColumnValue Then
                color = bar.HoverColor
            Else
                color = bar.Color
            End If

            bar.TopLocation.Left = New Point(a, b)
            bar.TopLocation.Right = New Point(a + c, b)
            bar.BottomLocation.Left = New Point(a, b + d)
            bar.BottomLocation.Right = New Point(a, b + d)

            Dim obBrush As LinearGradientBrush
            Dim obRect As New Rectangle(a, b, c, d)

            If bar.StartValue <> Nothing And endValue <> Nothing Then
                If (index >= scrollPos And index < _barsViewable + scrollPos) Or ignoreScrollAndMousePosition = True Then

                    obBrush = New LinearGradientBrush(obRect, color, color.Gray, LinearGradientMode.Vertical)

                    grfx.DrawRectangle(Pens.Black, obRect)
                    grfx.FillRectangle(obBrush, obRect)

                    grfx.DrawString(bar.Text, _rowTextFont, Brushes.Black, 0, _barStartTop + (_barHeight * (index - scrollPos)) + (_barSpace * (index - scrollPos)))

                End If
            End If
        Next
    End Sub

    Public Sub DrawNetVertical(grfx As Graphics)
        If _shownHeaderList Is Nothing Then Exit Sub
        If _shownHeaderList.Count = 0 Then Exit Sub

        Dim index As Integer = 0
        Dim lastHeader As Header = Nothing

        For Each header As Header In _shownHeaderList
            Dim headerLocationY As Integer

            If lastHeader Is Nothing Then
                headerLocationY = 0
            ElseIf header.Time.Hour < lastHeader.Time.Hour Then
                headerLocationY = 0
            Else
                headerLocationY = _headerTimeStartTop
            End If

            grfx.DrawLine(Pens.Bisque, _barStartLeft + (index * _widthPerItem), headerLocationY, _barStartLeft + (index * _widthPerItem), _lastLineStop)
            index += 1

            lastHeader = header
        Next

        grfx.DrawLine(_lineColor, _barStartLeft + (index * _widthPerItem), _headerTimeStartTop, _barStartLeft + (index * _widthPerItem), _lastLineStop)
    End Sub

    Public Sub DrawNetHorizontal(grfx As Graphics)
        If _shownHeaderList Is Nothing Then Exit Sub
        If _shownHeaderList.Count = 0 Then Exit Sub

        Dim index As Integer
        Dim widths As Integer = (_widthPerItem * _shownHeaderList.Count) + _barStartLeft

        For index = 0 To GetIndexChartBar("QQQQQQ")
            For Each bar As ChartBarDate In _bars
                grfx.DrawLine(_lineColor, 0, _barStartTop + (_barHeight * index) + (_barSpace * index), widths, _barStartTop + (_barHeight * index) + (_barSpace * index))
            Next
        Next

        _lastLineStop = _barStartTop + (_barHeight * (index - 1)) + (_barSpace * (index - 1))
    End Sub

    Private _lastLineStop As Integer = 0

#End Region

#Region "Header list"

    Private Function GetFullHeaderList() As List(Of Header)
        Dim result As New List(Of Header)
        Dim newFromTime As New Date(FromDate.Year, FromDate.Month, FromDate.Day)
        Dim item As String

        Dim interval As TimeSpan = ToDate - FromDate

        If interval.TotalDays < 1 Then
            With newFromTime
                newFromTime = .AddHours(FromDate.Hour)

                If _headerFromDate.Minute < 59 And _headerFromDate.Minute > 29 Then
                    newFromTime = .AddMinutes(30)
                Else
                    newFromTime = .AddMinutes(0)
                End If
            End With

            While newFromTime <= ToDate
                item = newFromTime.Hour & ":"

                If newFromTime.Minute < 10 Then
                    item += "0" & newFromTime.Minute
                Else
                    item += "" & newFromTime.Minute
                End If

                Dim header As New Header

                header.HeaderText = item
                header.HeaderTextInsteadOfTime = ""
                header.Time = New Date(newFromTime.Year, newFromTime.Month, newFromTime.Day, newFromTime.Hour, newFromTime.Minute, 0)
                result.Add(header)

                newFromTime = newFromTime.AddMinutes(5)
            End While
        ElseIf interval.TotalDays < 60 Then
            While newFromTime <= ToDate
                Dim header As New Header

                header.HeaderText = ""
                header.HeaderTextInsteadOfTime = ""
                header.Time = New Date(newFromTime.Year, newFromTime.Month, newFromTime.Day, 0, 0, 0)
                result.Add(header)

                newFromTime = newFromTime.AddDays(1)
            End While
        Else
            While newFromTime <= ToDate
                Dim header As New Header

                header.HeaderText = ""
                header.Time = New Date(newFromTime.Year, newFromTime.Month, newFromTime.Day, 0, 0, 0)
                header.HeaderTextInsteadOfTime = newFromTime.ToString("MM.yy")
                result.Add(header)

                newFromTime = newFromTime.AddMonths(1)
            End While
        End If

        Return result
    End Function

#End Region

#Region "Mouse Move"

    Private Sub GanttChart_MouseMove(sender As System.Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If _shownHeaderList Is Nothing Then Exit Sub
        If _shownHeaderList.Count = 0 Then Exit Sub

        If e.Button <> MouseButtons.Left Then
            _mouseHoverPart = MouseOverPart.Empty

            If AllowManualEditBar = True Then
                If _barIsChanging >= 0 Then
                    RaiseEvent BarChanged(Me, _bars(_barIsChanging).Value)
                    _barIsChanging = -1
                End If
            End If
        End If

        _mouseHoverBarIndex = -1

        Dim localMousePosition As Point

        localMousePosition = PointToClient(Cursor.Position)

        Dim timeBetween As TimeSpan = _shownHeaderList(1).Time - _shownHeaderList(0).Time
        Dim minutesBetween As Integer = (timeBetween.Days * 1440) + (timeBetween.Hours * 60) + timeBetween.Minutes
        Dim widthBetween = (_shownHeaderList(1).StartLocation - _shownHeaderList(0).StartLocation)
        Dim perMinute As Decimal = widthBetween / minutesBetween

        Dim minutesAtCursor As Integer

        If localMousePosition.X > _barStartLeft Then
            minutesAtCursor = (localMousePosition.X - _barStartLeft) / perMinute
            _mouseOverColumnValue = FromDate.AddMinutes(minutesAtCursor)
        Else
            _mouseOverColumnValue = Nothing
        End If

        Dim rowText As String = ""
        Dim rowValue As Object = Nothing
        Dim scrollBarStatusChanged As Boolean = False

        If localMousePosition.X > _bottomPart.Left And localMousePosition.Y < _bottomPart.Right And localMousePosition.Y < _bottomPart.Bottom And localMousePosition.Y > _bottomPart.Top Then
            If _mouseOverBottomPart = False Then
                scrollBarStatusChanged = True
            End If

            _mouseOverBottomPart = True
        Else
            If _mouseOverBottomPart = False Then
                scrollBarStatusChanged = True
            End If

            _mouseOverBottomPart = False
        End If

        If localMousePosition.X > _topPart.Left And localMousePosition.Y < _topPart.Right And localMousePosition.Y < _topPart.Bottom And localMousePosition.Y > _topPart.Top Then
            If _mouseOverTopPart = False Then
                scrollBarStatusChanged = True
            End If

            _mouseOverTopPart = True
        Else
            If _mouseOverTopPart = False Then
                scrollBarStatusChanged = True
            End If

            _mouseOverTopPart = False
        End If

        If localMousePosition.X > _scroll.Left And localMousePosition.Y < _scroll.Right And localMousePosition.Y < _scroll.Bottom And localMousePosition.Y > _scroll.Top Then
            If _mouseOverScrollBar = False Then
                scrollBarStatusChanged = True
            End If

            _mouseOverScrollBar = True
            _mouseOverScrollBarArea = True
        Else
            If _mouseOverScrollBar = False Then
                scrollBarStatusChanged = True
            End If

            _mouseOverScrollBar = False
            _mouseOverScrollBarArea = False
        End If

        If _mouseOverScrollBarArea = False Then
            If localMousePosition.X > _scrollBarArea.Left And localMousePosition.Y < _scrollBarArea.Right And localMousePosition.Y < _scrollBarArea.Bottom And localMousePosition.Y > _scrollBarArea.Top Then
                _mouseOverScrollBarArea = True
            End If
        End If

        Dim index As Integer = 0

        For Each bar As ChartBarDate In _bars

            If bar.HideFromMouseMove = False Then
                If bar.EndValue = Nothing Then
                    bar.EndValue = Date.Now
                End If

                If localMousePosition.Y > bar.TopLocation.Left.Y And localMousePosition.Y < bar.BottomLocation.Left.Y Then
                    If localMousePosition.X > bar.TopLocation.Left.X And localMousePosition.X < bar.TopLocation.Right.X Then

                        rowText = bar.Text
                        rowValue = bar.Value
                        _mouseHoverBarIndex = index

                        If _mouseHoverPart <> MouseOverPart.BarLeftSide And _mouseHoverPart <> MouseOverPart.BarRightSide Then
                            _mouseHoverPart = MouseOverPart.Bar
                        End If
                    End If

                    If AllowManualEditBar = True Then
                        Dim areaSize As Integer = 5

                        If e.Button = Windows.Forms.MouseButtons.Left Then
                            areaSize = 50
                        End If

                        If localMousePosition.X > bar.TopLocation.Left.X - areaSize And localMousePosition.X < bar.TopLocation.Left.X + areaSize And _mouseHoverPart <> MouseOverPart.BarRightSide Then
                            Me.Cursor = Cursors.VSplit
                            _mouseHoverPart = MouseOverPart.BarLeftSide
                            _mouseHoverBarIndex = index
                        ElseIf localMousePosition.X > bar.TopLocation.Right.X - areaSize And localMousePosition.X < bar.TopLocation.Right.X + areaSize And _mouseHoverPart <> MouseOverPart.BarLeftSide Then
                            Me.Cursor = Cursors.VSplit
                            _mouseHoverPart = MouseOverPart.BarRightSide
                            _mouseHoverBarIndex = index
                        Else
                            Me.Cursor = Cursors.Default
                        End If
                    End If
                End If
            End If

            index += 1
        Next

        _mouseOverRowText = rowText
        _mouseOverRowValue = rowValue

        If e.Button = Windows.Forms.MouseButtons.Left Then
            RaiseEvent MouseDragged(sender, e)
        Else

            If (_mouseOverRowValue Is Nothing And Not rowValue Is Nothing) Or (Not _mouseOverRowValue Is Nothing And rowValue Is Nothing) Or scrollBarStatusChanged = True Then
                PaintChart()
            End If
        End If
    End Sub

    Private Sub GanttChart_MouseLeave(sender As System.Object, e As EventArgs) Handles MyBase.MouseLeave
        _mouseOverRowText = Nothing
        _mouseOverRowValue = Nothing
        _mouseHoverPart = MouseOverPart.Empty

        PaintChart()
    End Sub

    Public Sub GanttChart_MouseDragged(sender As Object, e As MouseEventArgs) Handles Me.MouseDragged
        If _mouseOverScrollBarArea = True Then
            ScrollPositionY = e.Location.Y
        End If

        If AllowManualEditBar = True Then
            If _mouseHoverBarIndex > -1 Then
                If _mouseHoverPart = MouseOverPart.BarLeftSide Then
                    _barIsChanging = _mouseHoverBarIndex
                    _bars(_mouseHoverBarIndex).StartValue = _mouseOverColumnValue
                    PaintChart()
                ElseIf _mouseHoverPart = MouseOverPart.BarRightSide Then
                    _barIsChanging = _mouseHoverBarIndex
                    _bars(_mouseHoverBarIndex).EndValue = _mouseOverColumnValue
                    PaintChart()
                End If
            End If
        End If
    End Sub


#End Region

#Region "ToolTipText"

    Private _toolTipText As New List(Of String)

    Private _myPoint As New Point(0, 0)

    Public Property ToolTipTextTitle As String = ""

    Public Property ToolTipText() As List(Of String)
        Get
            If _toolTipText Is Nothing Then _toolTipText = New List(Of String)
            Return _toolTipText
        End Get
        Set
            _toolTipText = value

            Dim localMousePosition As Point

            localMousePosition = Me.PointToClient(Cursor.Position)


            If localMousePosition = _myPoint Then Exit Property

            _myPoint = localMousePosition

            ToolTip.SetToolTip(Me, ".")
        End Set
    End Property

    Private Sub ToolTipText_Draw(sender As System.Object, e As DrawToolTipEventArgs) Handles ToolTip.Draw
        If ToolTipText Is Nothing Then
            ToolTipText = New List(Of String)
            Exit Sub
        End If

        If ToolTipText.Count = 0 Then
            Exit Sub
        ElseIf ToolTipText(0).Length = 0 Then
            Exit Sub
        End If

        Dim x As Integer
        Dim y As Integer

        e.Graphics.FillRectangle(Brushes.AntiqueWhite, e.Bounds)
        e.DrawBorder()

        Dim titleHeight As Integer = 14
        Dim fontHeight As Integer = 12

        e.Graphics.DrawLine(Pens.Black, 0, titleHeight, e.Bounds.Width, titleHeight)

        Dim lines As Integer = 1
        Dim texts As String = ToolTipTextTitle

        Using fonts As New Font(e.Font, FontStyle.Bold)
            x = (e.Bounds.Width - e.Graphics.MeasureString(texts, fonts).Width) \ 2
            y = (titleHeight - e.Graphics.MeasureString(texts, fonts).Height) \ 2
            e.Graphics.DrawString(texts, fonts, Brushes.Black, x, y)
        End Using

        For Each str As String In ToolTipText
            Dim fonts As New Font(e.Font, FontStyle.Regular)

            If str.Contains("[b]") Then
                fonts = New Font(fonts.FontFamily, fonts.Size, FontStyle.Bold, fonts.Unit)
                str = str.Replace("[b]", "")
            End If

            Using fonts
                x = 5
                y = (titleHeight - fontHeight - e.Graphics.MeasureString(str, fonts).Height) \ 2 + 10 + (lines * 14)
                e.Graphics.DrawString(str, fonts, Brushes.Black, x, y)
            End Using

            lines += 1
        Next
    End Sub

    Private Sub ToolTipText_Popup(sender As System.Object, e As PopupEventArgs) Handles ToolTip.Popup
        If ToolTipText Is Nothing Then
            ToolTipText = New List(Of String)
        End If

        If ToolTipText.Count = 0 Then
            e.ToolTipSize = New Size(0, 0)
            Exit Sub
        ElseIf ToolTipText(0).Length = 0 Then
            e.ToolTipSize = New Size(0, 0)
            Exit Sub
        End If

        Dim heights As Integer = 18 + (ToolTipText.Count * 15)
        e.ToolTipSize = New Size(200, heights)
    End Sub

#End Region

#Region "ChartBar"

    Public Class ChartBarDate

        Friend Class Location
            Public Property Right As Point = New Point(0, 0)
            Public Property Left As Point = New Point(0, 0)
        End Class

        Public Property StartValue As Date
        Public Property EndValue As Date
        Public Property Color As Color
        Public Property HoverColor As Color
        Public Property Name As String
        Public Property Text As String
        Public Property Value As Object
        Public Property RowIndex As Integer
        Public Property HideFromMouseMove As Boolean = False
        Friend Property TopLocation As Location = New Location
        Friend Property BottomLocation As Location = New Location
    End Class

#End Region

#Region "Headers"

    Private Class Header
        Public Property HeaderText As String
        Public Property StartLocation As Integer
        Public Property HeaderTextInsteadOfTime As String = ""
        Public Property Time As Date = Nothing
    End Class

#End Region

#Region "Resize"

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        _scrollPosition = 0

        If _lastLineStop > 0 Then
            _objBmp = New Bitmap(Width - _barStartRight, _lastLineStop, Imaging.PixelFormat.Format24bppRgb)
            _objGraphics = Graphics.FromImage(_objBmp)
        End If

        PaintChart()
    End Sub

#End Region

#Region "Scrollbar"

    Private _barsViewable As Integer = -1
    Private _scrollPosition As Integer = 0
    Private _topPart As Rectangle = Nothing
    Private _bottomPart As Rectangle = Nothing
    Private _scroll As Rectangle = Nothing
    Private _scrollBarArea As Rectangle = Nothing

    Private _mouseOverTopPart As Boolean = False
    Private _mouseOverBottomPart As Boolean = False
    Private _mouseOverScrollBar As Boolean = False
    Private _mouseOverScrollBarArea As Boolean = False

    Private Sub DrawScrollBar(grfx As Graphics)
        _barsViewable = (Height - _barStartTop) / (_barHeight + _barSpace)
        Dim barCount As Integer = GetIndexChartBar("QQQWWW")
        If barCount = 0 Then Exit Sub

        Dim maxHeight As Integer = Height - 30
        Dim scrollHeight As Decimal = (maxHeight / barCount) * _barsViewable

        If scrollHeight >= maxHeight Then Exit Sub

        Dim scrollSpeed As Decimal = (maxHeight - scrollHeight) / (barCount - _barsViewable)

        _scrollBarArea = New Rectangle(Width - 20, 19, 12, maxHeight)
        _scroll = New Rectangle(Width - 20, 19 + (_scrollPosition * scrollSpeed), 12, scrollHeight)

        _topPart = New Rectangle(Width - 20, 10, 12, 8)
        _bottomPart = New Rectangle(Width - 20, Height - 10, 12, 8)

        Dim colorTopPart As Brush
        Dim colorBottomPart As Brush
        Dim colorScroll As Brush

        If _mouseOverTopPart = True Then
            colorTopPart = Brushes.Black
        Else
            colorTopPart = Brushes.Gray
        End If

        If _mouseOverBottomPart = True Then
            colorBottomPart = Brushes.Black
        Else
            colorBottomPart = Brushes.Gray
        End If

        If _mouseOverScrollBar = True Then
            colorScroll = New LinearGradientBrush(_scroll, Color.Bisque, Color.Gray, LinearGradientMode.Horizontal)
        Else
            colorScroll = New LinearGradientBrush(_scroll, Color.White, Color.Gray, LinearGradientMode.Horizontal)
        End If

        grfx.DrawRectangle(Pens.Black, _topPart)
        grfx.FillRectangle(Brushes.LightGray, _topPart)

        grfx.DrawRectangle(Pens.Black, _bottomPart)
        grfx.FillRectangle(Brushes.LightGray, _bottomPart)

        Dim points(2) As PointF
        points(0) = New PointF(_topPart.Left, _topPart.Bottom - 1)
        points(1) = New PointF(_topPart.Right, _topPart.Bottom - 1)
        points(2) = New PointF((_topPart.Left + _topPart.Right) / 2, _topPart.Top + 1)

        grfx.FillPolygon(colorTopPart, points)

        points(0) = New PointF(_bottomPart.Left, _bottomPart.Top + 1)
        points(1) = New PointF(_bottomPart.Right, _bottomPart.Top + 1)
        points(2) = New PointF((_bottomPart.Left + _bottomPart.Right) / 2, _bottomPart.Bottom - 1)

        grfx.FillPolygon(colorBottomPart, points)

        grfx.DrawRectangle(Pens.Black, _scrollBarArea)
        grfx.FillRectangle(Brushes.DarkGray, _scrollBarArea)

        grfx.DrawRectangle(Pens.Black, _scroll)
        grfx.FillRectangle(colorScroll, _scroll)
    End Sub

    Private WriteOnly Property ScrollPositionY As Integer
        Set
            Dim barCount As Integer = GetIndexChartBar("QQQWWW")
            Dim maxHeight As Integer = Height - 30
            Dim scrollHeight As Decimal = (maxHeight / barCount) * _barsViewable
            Dim scrollSpeed As Decimal = (maxHeight - scrollHeight) / (barCount - _barsViewable)
            Dim index As Integer = 0
            Dim distanceFromLastPosition = 9999

            While index < barCount
                Dim newPositionTemp As Integer = (index * scrollSpeed) + (scrollHeight / 2) + (30 / 2)
                Dim distanceFromCurrentPosition = newPositionTemp - value

                If distanceFromLastPosition < 0 Then
                    If distanceFromCurrentPosition < distanceFromLastPosition Then
                        _scrollPosition = index - 1
                        PaintChart()
                        Exit Property
                    End If
                Else
                    If distanceFromCurrentPosition > distanceFromLastPosition Then
                        _scrollPosition = index - 1

                        If _scrollPosition + _barsViewable > GetIndexChartBar("QQQWWW") Then
                            _scrollPosition = GetIndexChartBar("QQQWWW") - _barsViewable
                        End If

                        PaintChart()
                        Exit Property
                    End If
                End If

                distanceFromLastPosition = distanceFromCurrentPosition

                index += 1
            End While
        End Set
    End Property

    Public Sub ScrollOneup()
        If _scrollPosition = 0 Then Exit Sub
        _scrollPosition -= 1
        PaintChart()
    End Sub

    Public Sub ScrollOneDown()
        If _scrollPosition + _barsViewable >= GetIndexChartBar("QQQWWW") Then Exit Sub
        _scrollPosition += 1
        PaintChart()
    End Sub

    Private Sub GanttChart_Click(sender As System.Object, e As MouseEventArgs) Handles MyBase.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If _mouseOverBottomPart = True Then
                ScrollOneDown()
            ElseIf _mouseOverTopPart = True Then
                ScrollOneup()
            End If
        End If
    End Sub

    Private Sub GanttChart_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If e.Delta > 0 Then
            ScrollOneup()
        Else
            ScrollOneDown()
        End If
    End Sub

#End Region

#Region "Save"

    Public Sub SaveImage(filePath As String)
        _objGraphics.SmoothingMode = SmoothingMode.HighSpeed
        _objGraphics.Clear(BackColor)

        If _headerFromDate = Nothing Or _headerToDate = Nothing Then Exit Sub

        DrawHeader(_objGraphics, Nothing)
        DrawNetHorizontal(_objGraphics)
        DrawNetVertical(_objGraphics)
        DrawBars(_objGraphics, True)

        _objBmp.Save(filePath)
    End Sub

#End Region

    Private Enum MouseOverPart

        Empty
        Bar
        BarLeftSide
        BarRightSide

    End Enum

End Class