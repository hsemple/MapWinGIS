Private Sub LabelFeatures_Click(sender As Object, e As EventArgs) Handles LabelFeatures.Click

        Dim hndl As Integer, field As Integer, i As Integer
        Dim text As String
        Dim x As Double, y As Double
        Dim col As UInt32

        'Get handle for layer 0 which must contain a shapefile
        hndl = MapMain.get_LayerHandle(0)
        'Get the shapefile contained in layer 0
        shpfile = MapMain.get_GetObject(hndl)
        'Set shapefile field to use when labeling layer as field 0
        field = 3
        'Set the color for the labels to be black
        col = System.Convert.ToUInt32(RGB(0, 0, 204))

        'Label every shape in the shapefile
        For i = 0 To shpfile.NumShapes - 1
            'Set the text for this shape
            text = shpfile.CellValue(field, i)
            'Set the x and y coordinates for this label to be the min x and y coordinates of this shape
            x = shpfile.Shape(i).Extents.xMin
            y = shpfile.Shape(i).Extents.yMin

            'Add the label to the layer by the shape centering the text and rotating it 45 degrees

            shpfile.Labels.FontSize = 9
            shpfile.Labels.FontBold = True
            shpfile.Labels.FontColor = col
            shpfile.Labels.Alignment = tkLabelAlignment.laTopCenter
            shpfile.Labels.AutoOffset = True
            shpfile.Labels.AvoidCollisions = True
            shpfile.Labels.FrameVisible = False
            shpfile.GenerateLabels(3, tkLabelPositioning.lpCenter, False)
        Next
        MapMain.Redraw()

    End Sub
