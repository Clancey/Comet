﻿using System;

namespace Comet.Samples
{
    public class Question1a : View
    {
        [Body]
        View body() =>
            new VStack {
                        new Image("turtlerock.jpg").Frame(75, 75).Padding(4),
                        new Text("Title"),
                        new Text("Description").FontSize(12).Color(Color.Grey),
                    }.FillHorizontal();
    }
}
