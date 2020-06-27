Private Sub btnZoomIn_Click(sender As Object, e As EventArgs) Handles btnZoomIn.Click
        mapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomIn
    End Sub

Private Sub btnZoomOut_Click(sender As Object, e As EventArgs) Handles btnZoomOut.Click
        mapMain.CursorMode = MapWinGIS.tkCursorMode.cmZoomOut
 End Sub



Private Sub btnPan_Click(sender As Object, e As EventArgs) Handles btnPan.Click
        mapMain.CursorMode = MapWinGIS.tkCursorMode.cmPan
End Sub


Private Sub btnFullExtent_Click(sender As Object, e As EventArgs) Handles btnFullExtent.Click
        mapMain.ZoomToMaxExtents()
End Sub

