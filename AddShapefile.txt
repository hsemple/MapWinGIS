Private Sub btnAddShapefile_Click(sender As Object, e As EventArgs) Handles btnAddShapefile.Click
        Dim shpfile As New MapWinGIS.Shapefile
        Dim openDlg As New OpenFileDialog

        'Initialize Dialog
        openDlg.Filter = "Supported Formats|*.shp"
        openDlg.CheckFileExists = True

        If openDlg.ShowDialog() = DialogResult.OK Then

            'Open the shapefile
            shpfile.Open(openDlg.FileName)

            'Add the layer to the map
            mapMain.AddLayer(shpfile, True)
        End If
    End Sub
