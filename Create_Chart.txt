Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Dim hndl As Integer
        Dim shpfile = MapMain.get_GetObject(hndl)
        Dim charts As Charts = shpfile.Charts
        Dim state As Boolean
        state = True

        If CheckBox2.Checked Then
            charts.Clear()
            state = False
            charts.ChartType = tkChartType.chtBarChart
            charts.Generate(tkLabelPositioning.lpCentroid)
            charts.AddField2(5, 8088314)
            charts.BarHeight = 100
            charts.Thickness = 10
            charts.ValuesVisible = False
            charts.Visible = True
                     

        Else state = False
            charts.Visible = False
            charts.Clear()
            charts.ClearFields()

        End If

        MapMain.Redraw()
    End Sub



