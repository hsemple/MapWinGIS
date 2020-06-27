 Private Sub OpenFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        'Create instances of the possible data objects,
        'to retrieve the dialog filters of supported formats.
        Dim grd As New MapWinGIS.Grid
        Dim img As New MapWinGIS.Image
        Dim gs As New MapWinGIS.GlobalSettings()
        Dim gp = New GeoProjection()

        Dim x As Double  'longitude
        Dim y As Double ' latitude
        Dim ext As New MapWinGIS.Extents
        Dim filename As String
        Dim filepath As System.IO.FileInfo


        MapMain.Tiles.ClearCache(tkCacheType.Disk)
        MapMain.TileProvider = MapWinGIS.tkTileProvider.OpenHumanitarianMap
        ' MapMain.Projection = MapWinGIS.tkMapProjection.PROJECTION_WGS84
        MapMain.KnownExtents = MapWinGIS.tkKnownExtents.keUSA
        MapMain.GrabProjectionFromData = False
        MapMain.Latitude = 38.1755562
        MapMain.Longitude = -95.85063
        MapMain.CurrentZoom = 4

        gs.ReprojectLayersOnAdding = True
        gs.AllowLayersWithoutProjections = True
        gs.AllowProjectionMismatch = True
        gs.StartLogTileRequests("C:\\MapWindow GIS\\tile.log", False)
        gp.SetWgs84Projection(tkWgs84Projection.Wgs84_UTM_zone_17N)


        Dim hndl As Integer = -1

        'We want users to be able to open any file type.
        'So merge the dialog filters by placing a pipe character between each:
        Dim dlg As New OpenFileDialog

        dlg.Filter = "All Files (*.*)|*.*|" & shpfile.CdlgFilter &
                     "|" & grd.CdlgFilter & "|" & img.CdlgFilter

        dlg.Multiselect = True

        If dlg.ShowDialog() = DialogResult.OK Then
            'If the user didn't cancel the dialog, try to open the file.
            'First, determine what kind it was:
            Dim extension As String = IO.Path.GetExtension(dlg.FileName).ToLower()


            If shpfile.CdlgFilter.ToLower().Contains(extension) Then
                shpfile.Open(dlg.FileName)

                ' Set Color Scheme
                Dim ColorScheme As New MapWinGIS.ColorScheme
                shpfile.Categories.Generate(4, MapWinGIS.tkClassificationType.ctNaturalBreaks, 5)

                ColorScheme.SetColors3(12, 34, 56, 123, 234, 156)
                ColorScheme.Reverse()

                shpfile.Categories.ApplyColorScheme(MapWinGIS.tkColorSchemeType.ctSchemeGraduated, ColorScheme)


                'MapMain.CurrentZoom = 4
                'Zoom to all visible layers
                ext.SetBounds(x, y, 0.0, x, y, 0.0)
                hndl = MapMain.AddLayer(shpfile, True)
                'Zoom to extent of USA
                MapMain.KnownExtents = MapWinGIS.tkKnownExtents.keUSA
                MapMain.CurrentZoom = 4

                ToolStripStatusLabel1.Text = "Number of Features in Shapefile: " & shpfile.Table.NumRows.ToString

                If CheckBox1.Checked = False Then
                    CheckBox1.Visible = True
                    CheckBox1.Checked = True

                    filepath = My.Computer.FileSystem.GetFileInfo(shpfile.Filename)
                    filename = filepath.Name
                    CheckBox1.Text = filename
                    End If

                MapMain.Redraw()
                Return 'Done


                ElseIf grd.CdlgFilter.ToLower().Contains(extension) Then
                    'NOTE: a .tif can be a GeoTIFF (a grid)
                    'or an image. If the file is a tif, then:
                    If dlg.FileName.ToLower().EndsWith("tif") Then
                        img.Open(dlg.FileName)
                        MapMain.AddLayer(img, True)

                        'Zoom to all visible layers
                        MapMain.ZoomToMaxExtents()
                        Return ' Done
                    End If

                    'Open the grid:
                    grd.Open(dlg.FileName)

                    'Define a coloring scheme to color this grid:
                    Dim sch As New MapWinGIS.GridColorScheme
                    'Use a predefined coloring scheme "Fall Leaves"
                    sch.UsePredefined(grd.Minimum, grd.Maximum, MapWinGIS.PredefinedColorScheme.Rainbow)

                    'Convert it to an image that can be displayed:
                    Dim u As New MapWinGIS.Utils

                    Dim MyHeader As New MapWinGIS.GridHeader()
                'MyHeader = grd.Header

                gp = grd.Header.GeoProjection

                Dim projection As String
                    'Get the projection information for the grid header 
                    projection = MyHeader.Projection


                    Dim gridimage As MapWinGIS.Image
                    gridimage = u.GridToImage(grd, sch)


                    'Add the generated image to the map:
                    MapMain.AddLayer(gridimage, True)


                    'Zoom to all visible layers
                    MapMain.ZoomToMaxExtents()

                If CheckBox1.Checked = False Then
                    CheckBox1.Visible = True
                    CheckBox1.Checked = True

                    filepath = My.Computer.FileSystem.GetFileInfo(dlg.FileName)
                    filename = filepath.Name
                    CheckBox1.Text = filename
                End If

                Return 'Done

            ElseIf img.CdlgFilter.ToLower().Contains(extension) Then
                    'It is a plain image
                    img.Open(dlg.FileName)
                MapMain.AddLayer(img, True)

                'Zoom to all visible layers
                MapMain.ZoomToMaxExtents()
                If CheckBox1.Checked = False Then
                    CheckBox1.Visible = True
                    CheckBox1.Checked = True

                    filepath = My.Computer.FileSystem.GetFileInfo(dlg.FileName)
                    filename = filepath.Name
                    CheckBox1.Text = filename
                End If

                Return 'Done
            End If

        End If

    End Sub
