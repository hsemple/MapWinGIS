Private Sub RemoveLabelsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveLabelsToolStripMenuItem.Click
        Dim lb As MapWinGIS.Labels
        If sf Is Nothing Then Exit Sub
        lb = sf.Labels
        lb.Clear()
        mapMain.Redraw()
    End Sub


