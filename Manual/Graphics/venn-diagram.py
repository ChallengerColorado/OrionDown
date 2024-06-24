import matplotlib.pyplot as plt
import matplotlib.patches as mptch
import matplotlib.transforms as trnf
import math

SKEW = 0.6
WIDTH = 0.4
HEIGHT = 0.8

HORIZONTAL_OFFSET = 0.15
VERTICAL_OFFSET = 0.1

PADDING_BOTTOM = 0.01

THICKNESS = 2

fig = plt.figure(figsize=(5, 4))


def add_ellipse(transform, linestyle, horizontal=0, vertical=0):
    fig.add_artist(
        mptch.Ellipse(
            (0.5 + horizontal, HEIGHT / 2 + PADDING_BOTTOM + vertical),
            WIDTH,
            HEIGHT,
            transform=fig.transFigure + transform,
            linestyle=linestyle,
            linewidth=THICKNESS,
            edgecolor="black",
            fill=False,
        ),
    )


# Blue
add_ellipse(trnf.Affine2D.from_values(1, 0, SKEW, 1, 0, 0), "-")
# Offset
add_ellipse(trnf.Affine2D.from_values(1, 0, -SKEW, 1, 0, 0), "--")

# P 1-4
add_ellipse(
    trnf.Affine2D.from_values(1, 0, SKEW, 1, 0, 0),
    "-.",
    -HORIZONTAL_OFFSET,
    VERTICAL_OFFSET,
)

# Red
add_ellipse(
    trnf.Affine2D.from_values(1, 0, -SKEW, 1, 0, 0),
    ":",
    HORIZONTAL_OFFSET,
    VERTICAL_OFFSET,
)

plt.figtext(0.5, PADDING_BOTTOM + 0.05, "N", ha="center")
plt.figtext(0.555, PADDING_BOTTOM + 0.13, "Y", ha="center")
plt.figtext(0.445, PADDING_BOTTOM + 0.13, "N", ha="center")
plt.figtext(0.63, PADDING_BOTTOM + 0.17, "N", ha="center")
plt.figtext(0.37, PADDING_BOTTOM + 0.17, "Y", ha="center")
plt.figtext(0.5, PADDING_BOTTOM + 0.23, "N", ha="center")
plt.figtext(0.75, PADDING_BOTTOM + 0.3, "N", ha="center")
plt.figtext(0.25, PADDING_BOTTOM + 0.3, "Y", ha="center")
plt.figtext(0.58, PADDING_BOTTOM + 0.35, "Y", ha="center")
plt.figtext(0.42, PADDING_BOTTOM + 0.35, "N", ha="center")
plt.figtext(0.7, PADDING_BOTTOM + 0.5, "Y", ha="center")
plt.figtext(0.3, PADDING_BOTTOM + 0.5, "Y", ha="center")
plt.figtext(0.5, PADDING_BOTTOM + 0.54, "Y", ha="center")
plt.figtext(0.58, PADDING_BOTTOM + 0.65, "Y", ha="center")
plt.figtext(0.42, PADDING_BOTTOM + 0.65, "Y", ha="center")
plt.figtext(0.5, PADDING_BOTTOM + 0.8, "N", ha="center")

plt.savefig("Output/venn-diagram.png")
