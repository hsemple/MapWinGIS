Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles My-Base.Load
        Dim gs As New MapWinGIS.GlobalSettings()

        MapMain.TileProvider = MapWinGIS.tkTileProvider.OpenStreetMap
        MapMain.Projection = MapWinG-IS.tkMapProjection.PROJECTION_GOOGLE_MERCATOR
        MapMain.KnownExtents = MapWinGIS.tkKnownExtents.keUSA
        MapMain.GrabProjectionFromData = True
        MapMain.Latitude = 38.1755562
        MapMain.Longitude = -95.85063
        MapMain.CurrentZoom = 4

        gs.ReprojectLayersOnAdding = True
        gs.AllowLayersWithoutProjections = True
        gs.AllowProjectionMismatch = True


    End Sub





