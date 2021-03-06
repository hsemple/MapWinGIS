Private Sub AddWMSLayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddWMSLayerToolStripMenuItem.Click

        Dim hndl As Integer
        'Get layer handle for layer at position 0
        hndl = MapMain.get_LayerHandle(0)

        Dim WmsLayer As New WmsLayer()
        Dim Extents As New MapWinGIS.Extents

        'Set the wmsLayer properties, taken from geoserver.
        Extents.SetBounds(-104.5005, 32.7501, 0, -94.01, 37.2, 0)
        WmsLayer.BaseUrl = "http://ogi.state.ok.us/geoserver/wms"
        WmsLayer.BoundingBox = Extents
        WmsLayer.Contrast = 1.0
        WmsLayer.DoCaching = False
        WmsLayer.Epsg = 4326
        WmsLayer.Format = "image%2Fjpg"
        WmsLayer.Gamma = 1.0
        WmsLayer.Layers = "ogi:okcounties"
        WmsLayer.Name = "ok_counties_plus"
        WmsLayer.Opacity = 155
        WmsLayer.UseCache = False
        WmsLayer.Id = 1
        WmsLayer.UseTransparentColor = True
        WmsLayer.Key = "1"
        WmsLayer.Version = tkWmsVersion.wvAuto
        'WmsLayer.TransparentColor = 255
        hndl = MapMain.AddLayer(WmsLayer, True)

        'MsgBox("Layers added to the map:")
        'MapMain.ZoomToMaxExtents()
         MapMain.Redraw()

    End Sub
