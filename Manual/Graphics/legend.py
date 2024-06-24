import matplotlib.figure as figs
import matplotlib.lines as lines


STYLES = ["-", "--", "-.", ":"]

for i in range(len(STYLES)):
    legend_fig = figs.Figure(figsize=(2, 0.2))
    legend_fig.add_artist(
        lines.Line2D(
            [0.05, 0.95], [0.5, 0.5], color="black", linewidth=2, linestyle=STYLES[i]
        )
    )

    legend_fig.savefig("Output/ls " + str(i + 1) + ".png")
