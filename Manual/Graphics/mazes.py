import matplotlib as mpl
import matplotlib.lines as lines
import matplotlib.pyplot as plt
import matplotlib.patches as mptch
import matplotlib.figure as figs

RECT_SIZE = 1/32


def maze(vertical, horizontal, end):

    maze_fig = figs.Figure(figsize=(3, 3), dpi=150)

    maze_fig.add_artist(mptch.Rectangle(
        (.0, .0), 1, 1,
        edgecolor="black", fill=False, linewidth=4))

    for i in range(8):
        for j in range(8):
            maze_fig.add_artist(mptch.Rectangle(
                (i/8 + 1/16 - RECT_SIZE/2, j/8 + 1/16 - RECT_SIZE/2), RECT_SIZE, RECT_SIZE,
                edgecolor="black", fill=False, linewidth=2
                ))

    for i in horizontal:
        maze_fig.add_artist(lines.Line2D(
            (i[0]/8, i[0]/8), ((i[1]-1)/8, i[1]/8),
            color="black", linewidth=2,
            transform=maze_fig.transFigure
            ))

    for i in vertical:
        maze_fig.add_artist(lines.Line2D(
            ((i[0]-1)/8, i[0]/8), (i[1]/8, i[1]/8),
            color="black", linewidth=2,
            transform=maze_fig.transFigure
            ))

    maze_fig.add_artist(mptch.Circle(
        (end[0]/8 - 1/16, end[1]/8 - 1/16), 1/32,
        color="black"
        ))

    return maze_fig


maze_specs = {
    "AE": {
        "horizontal": (
            (1, 7), (2, 7), (4, 7),
            (3, 6), (4, 6), (5, 7), (6, 7),
            (2, 5), (4, 5), (6, 5),
            (1, 4), (2, 4), (4, 4), (7, 4),
            (3, 3), (4, 3), (6, 3), (8, 3),
            (2, 2), (4, 2), (5, 2), (7, 2),
            (1, 1), (6, 1)),
        "vertical": (
            (4, 8), (6, 8),
            (2, 7), (5, 7), (7, 7),
            (1, 6), (3, 6), (6, 6),
            (2, 5), (4, 5), (5, 5), (7, 5),
            (2, 4), (5, 4), (7, 4),
            (1, 3), (6, 3),
            (2, 2), (3, 2), (5, 2), (6, 2),
            (2, 1), (4, 1), (5, 1), (7, 1)),
        "end": (4, 5),
    }
}

for m in maze_specs:

    spec = maze_specs[m]

    maze(
        spec["horizontal"],
        spec["vertical"],
        spec["end"]).savefig("Output/" + m + ".png")