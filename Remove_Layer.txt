Private Sub RemoveLayerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim hndl As Integer
        
        'Get handle for layer 0 which must contain a shapefile
        hndl = MapMain.get_LayerHandle(0)
        MapMain.RemoveLayer(hndl)
        MapMain.Redraw()
    End Sub
