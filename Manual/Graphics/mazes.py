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
                (i/8 + 1/16 - RECT_SIZE/2, j/8 + 1/16 - RECT_SIZE/2),
                RECT_SIZE, RECT_SIZE,
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


maze_specs = (
    {  # 1
        "horizontal": (
            (1, 7), (2, 7), (4, 7),
            (3, 6), (4, 6), (5, 6), (7, 6),
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
        "end": (4, 5)
    },
    {  # 2
        "horizontal": (
            (1, 7), (3, 7), (6, 7),
            (2, 6), (4, 6), (5, 6), (7, 6),
            (5, 5), (6, 5), (8, 5),
            (1, 4), (2, 4), (3, 4), (4, 4), (7, 4),
            (1, 3), (4, 3), (6, 3),
            (2, 2), (5, 2), (7, 2), (8, 2),
            (3, 1), (6, 1)),
        "vertical": (
            (2, 8), (4, 8), (7, 8),
            (1, 7), (5, 7),
            (2, 6), (3, 6), (4, 6), (6, 6),
            (1, 5), (3, 5), (5, 5),
            (2, 4), (7, 4),
            (2, 3), (4, 3), (6, 3), (7, 3),
            (2, 2), (3, 2), (5, 2), (7, 2),
            (1, 1), (4, 1), (5, 1)),
        "end": (6, 5)
    },
    {  # 3
        "horizontal": (
            (3, 7), (6, 7),
            (2, 6), (5, 6), (7, 6),
            (1, 5), (3, 5), (8, 5),
            (2, 4), (4, 4), (7, 4),
            (3, 3), (4, 3), (6, 3),
            (1, 2), (3, 2), (8, 2),
            (2, 1), (5, 1), (7, 1)),
        "vertical": (
            (1, 8), (4, 8), (6, 8), (7, 8),
            (3, 7), (5, 7), (7, 7),
            (1, 6), (2, 6), (3, 6), (4, 6), (5, 6), (6, 6),
            (1, 5), (5, 5), (7, 5),
            (2, 4), (4, 4), (6, 4),
            (1, 3), (4, 3), (6, 3), (7, 3),
            (2, 2), (4, 2), (5, 2), (6, 2),
            (1, 1), (3, 1)),
        "end": (6, 1)
    },
    {  # 4
        "horizontal": (
            (2, 7), (3, 7), (7, 7), (8, 7),
            (4, 6), (6, 6), (7, 6),
            (2, 5), (5, 5), (7, 5), (8, 5),
            (1, 4), (3, 4), (6, 4),
            (2, 3), (4, 3), (7, 3), (8, 3),
            (3, 2), (5, 2), (7, 2),
            (2, 1), (3, 1), (6, 1), (8, 1)),
        "vertical": (
            (2, 8), (4, 8),
            (2, 7), (3, 7), (5, 7), (7, 7),
            (1, 6), (2, 6), (5, 6),
            (3, 5), (4, 5),
            (2, 4), (3, 4), (5, 4), (7, 4),
            (1, 3), (2, 3), (5, 3),
            (3, 2), (4, 2), (5, 2), (7, 2),
            (1, 1), (4, 1)),
        "end": (3, 7)
    },
    {  # 5
        "horizontal": (
            (1, 1),),
        "vertical": (
            (1, 1),),
        "end": (1, 1)
    },
    {  # 6
        "horizontal": (
            (1, 1),),
        "vertical": (
            (1, 1),),
        "end": (1, 1)
    }
)

for m in range(len(maze_specs)):

    spec = maze_specs[m]

    maze(
        spec["horizontal"],
        spec["vertical"],
        spec["end"]).savefig("Output/" + str(m + 1) + ".png")
