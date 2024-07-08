// commands:
// typst compile symbols.typ "OrionDown/Assets/Resources/Symbols/{n}.png"
// typst compile symbols.typ "Manual/Graphics/Symbols/{n}.png"

#set page(
  width: 3cm, height: 3cm,
  fill: silver
)
#set text(
  font: "Junicode",
  features: (
    "cv45": 2,
    "cv53": 3,
    "cv14": 1,
    "cv36": 3
  ),
  size: 3cm
)

// 1
#page(
  align(center + horizon)[
    W
  ]
)

// 2
#page(
  align(center + horizon)[
    #str.from-unicode(8468)
  ]
)

// 3
#page(
  align(center)[
    #text(size: 2.5cm)[Ą]
  ]
)

// 4
#page(
  align(center)[
    #text(baseline: -.2em, tracking: -.35em)[g˛]
  ]
)

// 5
#page(
  align(center + horizon)[
    #set text(size: 2.5cm, baseline: -.1em)
    #str.from-unicode(59575)
  ]
)

// 6
#page(
  align(center + horizon)[
    #set text(baseline: -.05em)
    #str.from-unicode(42850)
  ]
)

// 7
#page(
  align(center + horizon)[
    #set text(size: 2.6cm, baseline: -.12em)
    #str.from-unicode(42856)
  ]
)

// 8
#page(
  align(center + horizon)[
    #set text(size: 2.6cm, baseline: -.13em)
    #str.from-unicode(42840)
  ]
)

// 9
#page(
  align(center + horizon)[
    #text(size: 2.3cm, str.from-unicode(62176))
  ]
)

// 10
#page(
  align(center + horizon)[
    #set text(size: 2.8cm, baseline: -.11em)
    #str.from-unicode(62193)
  ]
)

// 11
#page(
  align(center + horizon)[
    #set text(size: 2.5cm, baseline: -.1em)
    #str.from-unicode(62184)
  ]
)

// 12
#page(
  align(center + horizon)[
    #set text(size: 3.2cm, baseline: .03em)
    #str.from-unicode(62188)
  ]
)

// 13
#page(
  align(center + horizon)[
    #set text(size: 3.3cm, baseline: -.26em)
    #str.from-unicode(61348)
  ]
)

// 14
#page(
  align(center + horizon)[
    #str.from-unicode(8577)
  ]
)

// 15
#page(
  align(center + horizon)[
    #set text(size: 2.2cm)
    #str.from-unicode(8578)
  ]
)

// 16
#page(
  align(center + horizon)[
    #set text(size: 2.5cm, baseline: -.13em)
    #str.from-unicode(8485)
  ]
)

// 17
#page(
  align(center)[
    #text(baseline: -.2em)[g]
  ]
)

// 18
#page(
  align(center + horizon)[
    #set text(size: 2.6cm, baseline: -.12em)
    #str.from-unicode(42852)
  ]
)

// 19
#page(
  align(center + horizon)[
    #set text(size: 3.5cm, baseline: -.01em)
    #str.from-unicode(61660)
  ]
)

// 20
#page(
  align(center + horizon)[
    #set text(size: 3.3cm, baseline: .03em)
    #str.from-unicode(61673)
  ]
)
