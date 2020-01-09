using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworkLibrary.Reports
{
   public class PieChart {

	// Number of radians in each quadrant of the chart
	private static double radiansPerQuadrant = 2.0 * Math.PI / 4.0;

	// Side of the div that should be colored for each quadrant
	private static String[] coloredBorderSides = new String[] { "bottom",
			"right", "top", "left" };

	// Side of the div that should be transparent for each quadrant
	private static String[] transparentBorderSides = new String[] { "left",
			"bottom", "right", "top" };

	// Dimension of the div that adjusts the colored block thickness,
	// for each quadrant
	private static String[] coloredBlockDimensions = new String[] { "width",
			"height", "width", "height" };

	public static String generate(double[] values, String[] colors,
			double radius) {
		StringBuilder output = new StringBuilder(
				"<div class=\"piechart\"><img HEIGHT=\"20\" WIDTH=\"20\" alt=\"\" src=\"data:image/png;base64,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\" border=\"0\"/>");

		double sum = 0;

        foreach (var i in values)
	    {
		     sum += i;
	    }

		// Number of radians per 1.0 of a value
		double radiansPerUnit = 2.0 * Math.PI / sum;

		// State
		int colorIndex = 0;
		double segmentEndAngle = 0;
		int quadrant = 0;

		for (int valueIndex = 0; valueIndex < values.Length; valueIndex++) {

			// The angle from the beginning of the current quadrant
			// to the end of this segment
			segmentEndAngle += radiansPerUnit * values[valueIndex];

			// Pick the next color
			String color = colors[colorIndex];
            colorIndex = (colorIndex + 1) % colors.Length;

			// Place this segment behind the last
            int zOrder = values.Length - valueIndex;

			// If the segment carries on into the next quadrant, just finish
			// off this quadrant with a box covering the entire quadrant,
			// filling all remaining space.

			while (segmentEndAngle >= radiansPerQuadrant) {

				output.Append("<div class=\"box quadrant").Append(quadrant)
						.Append("\" style=\"background: ").Append(color)
						.Append("; z-index: ").Append(zOrder)
						.Append("\"></div>");
				segmentEndAngle -= radiansPerQuadrant;
				quadrant++;

			}

			// Calculate the border thicknesses to create the desired angle
			double coloredBorderThickness;
			double transparentBorderThickness;
			if (segmentEndAngle < radiansPerQuadrant / 2) {
				transparentBorderThickness = radius;
				coloredBorderThickness = radius * Math.Tan(segmentEndAngle);
			} else {
				coloredBorderThickness = radius;
				transparentBorderThickness = radius / Math.Tan(segmentEndAngle);
			}
			double coloredBlockThickness = radius - transparentBorderThickness;

			output.Append("<div class=\"wedge quadrant").Append(quadrant)
					.Append("\" style=\" border-")
					.Append(transparentBorderSides[quadrant % 4])
					.Append(": transparent ")
					.Append(transparentBorderThickness)
					.Append("em solid; border-")
					.Append(coloredBorderSides[quadrant % 4]).Append(": ")
					.Append(color).Append(" ").Append(coloredBorderThickness)
					.Append("em solid; z-index: ").Append(zOrder).Append(";")
					.Append(coloredBlockDimensions[quadrant % 4]).Append(": ")
					.Append(coloredBlockThickness).Append("em;\"></div>");

		}

		output.Append("</div>");
		return output.ToString();

	}

}
}
