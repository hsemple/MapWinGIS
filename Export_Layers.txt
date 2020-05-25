Private Sub ExportLayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportLayerToolStripMenuItem.Click
        Dim myImage As New MapWinGIS.Image
        Dim myExtents As MapWinGIS.Extents
        myExtents = MapMain.Extents
        'myImage = MapMain.SnapShot(myExtents)

        Dim xName As String
        xName = Format(DateTime.Now, "MMddyyyy-hhmmss") & ".tif"

        If (myImage.Save("C:\MapWindow GIS\" & xName, True, MapWinGIS.ImageType.JPEG_FILE)) Then
            MsgBox("Image Exported")
        End If

    End Sub


