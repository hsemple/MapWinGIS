Private Sub OpenAllLayersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAllLayersToolStripMenuItem.Click
        'Create instances of the possible data objects,
        'to retrieve the dialog filters of supported formats.
        Dim shpfile As New MapWinGIS.Shapefile
        Dim grd As New MapWinGIS.Grid
        Dim img As New MapWinGIS.Image
        Dim MainHandler As Integer

        Legend1.Map = mapMain.GetOcx()

        'We want users to be able to open any file type.
        'So merge the dialog filters by placing a pipe character
        'in between each:
        Dim dlg As New OpenFileDialog
        dlg.Filter = "All Files (*.*)|*.*|" & shpfile.CdlgFilter &
                     "|" & grd.CdlgFilter & "|" & img.CdlgFilter


        If dlg.ShowDialog() = DialogResult.OK Then
            'If the user didn't cancel the dialog, try to open the file.
            'First, determine what kind it was:
            Dim extension As String = IO.Path.GetExtension(dlg.FileName).ToLower()
            If shpfile.CdlgFilter.ToLower().Contains(extension) Then
                'It's a shapefile
                shpfile.Open(dlg.FileName)
                ' mapMain.AddLayer(sf, True)
                MainHandler = Legend1.Layers.Add(shpfile, True)
                Legend1.Map.LayerName(MainHandler) = System.IO.Path.GetFileNameWithoutExtension(shpfile.Filename)
                Legend1.Layers.ItemByHandle(Legend1.SelectedLayer).Refresh()

                'Zoom to all visible layers
                'mapMain.ZoomToMaxExtents()

                Return 'Done

            ElseIf grd.CdlgFilter.ToLower().Contains(extension) Then
                'NOTE: a .tif can be a GeoTIFF (a grid)
                'or an image. Check this, if the file is a tif:
                If dlg.FileName.ToLower().EndsWith("tif") Then
                    If Not mapMain.IsTIFFGrid(dlg.FileName) Then
                        'It's an image, not a grid.
                        'Open it as an image.
                        img.Open(dlg.FileName)
                        'mapMain.AddLayer(img, True)
                        MainHandler = Legend1.Layers.Add(img, True)
                        Legend1.Map.LayerName(MainHandler) = System.IO.Path.GetFileNameWithoutExtension(shpfile.Filename)
                        Legend1.Layers.ItemByHandle(Legend1.SelectedLayer).Refresh()


                        'Zoom to all visible layers
                        mapMain.ZoomToMaxExtents()
                        Return ' Done
                    End If
                End If


                'Open the grid:
                grd.Open(dlg.FileName)

                'Define a coloring scheme to color this grid:
                Dim sch As New MapWinGIS.GridColorScheme
                'Use a predefined coloring scheme "Fall Leaves"
                sch.UsePredefined(grd.Minimum, grd.Maximum,
                     MapWinGIS.PredefinedColorScheme.FallLeaves)

                'Convert it to an image that can be displayed:
                Dim u As New MapWinGIS.Utils

                Dim gridimage As MapWinGIS.Image
                gridimage = u.GridToImage(grd, sch)

                'Add the generated image to the map:
                'mapMain.AddLayer(gridimage, True)
                MainHandler = Legend1.Layers.Add(gridimage, True)
                Legend1.Map.LayerName(MainHandler) = System.IO.Path.GetFileNameWithoutExtension(shpfile.Filename)
                Legend1.Layers.ItemByHandle(Legend1.SelectedLayer).Refresh()

                'Zoom to all visible layers
                mapMain.ZoomToMaxExtents()

                Return 'Done

            ElseIf img.CdlgFilter.ToLower().Contains(extension) Then
                'It's a plain image
                img.Open(dlg.FileName)
                mapMain.AddLayer(img, True)

                'Zoom to all visible layers
                mapMain.ZoomToMaxExtents()
                Return 'Done
            End If
        End If


    End Sub

