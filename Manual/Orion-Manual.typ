#set page(
  "us-letter",
  margin: 1in
)
#set par(
  leading: 0.55em,
  first-line-indent:
  1.8em,
  justify: true
)
#set text(
  size: 12pt,
  font: "Latin Modern Sans 12"
  //font: "Latin Modern Sans 12"
)

#show par: set block(spacing: 0.55em)
#show heading: it => {
  set block(above: 1.4em, below: 1em)
  set text(font: "Latin Modern Sans")
  it
}

#let ovr = [=== Overview]
#let ins = [=== Repair Instructions]

#let status(status-text) = box(
  stroke: .4pt,
  inset: (x: 3pt),
  outset: (top: 3.5pt, bottom: 3pt),
  text(font: "Latin Modern Mono", status-text)
)

#let arrof(len, content) = for i in range(len) { (content, ) }

#let mdot(a, b) = if a == b {sym.circle.filled}
#let hstable(pos, text) = table(
  rows: arrof(3, .7cm),
  columns: arrof(3, .9cm),
  align: center + horizon,
  stroke: .4pt,
  mdot(0, pos), mdot(1, pos), mdot(2, pos), // indexed from 0 to match code
  mdot(3, pos), mdot(4, pos), mdot(5, pos),
  table.hline(stroke: 2pt),
  table.cell(colspan: 3, text)
)

#let graphics-path = "Graphics/Output/"

#let maze(num) = image(
  width: 2in,
  graphics-path + str(num) + ".png"
)

#let line-sample(num) = image(
  graphics-path + "ls " + str(num) + ".png"
)

#let keysym(name) = image(
  width: 1.8cm,
  "Graphics/Symbols/" + name + ".png"
)

#page[
  #set align(
  center)
  #set block(spacing: .2em)
  #text(font: "Latin Modern Roman 12", size: 25pt)[*Orion Capsule Repair
Manual*]
  #v(1cm)
  #text(font: "Latin Modern Roman 17", size: 18pt)[Mission Control Guide]
]
//#counter(page).update(1)

#set page(
  header: context[
    _Orion Capsule Repair Manual_
    #h(1fr)
    #counter(page).display()
  ]
)

= Introduction

Although everything possible is done beforehand to limit the possibility of
the
spacecraft being damaged during a mission, it is still a scenario that both the
crew of the spacecraft and Mission Control must be prepared for. This manual has
been written to assist Mission Control in guiding the astronauts through the
progress of diagnosing and fixing issues that may arise with the Orion capsule.

== Time

First and foremost, it is of the utmost importance that problems be fixed in a
timely manner. In a situation in which a second can mean the difference between
mission success and failure, diagnosing and fixing issues is a matter of extreme
urgency. In short: time is of the essence! In an emergency situation, all
astronauts will be relayed the remaining window to fix all issues with the
spacecraft before the mission must be aborted. The crew and Mission Control must
therefore communicate quickly and effectively. However, although members of the
crew and Mission Control must act quickly, they must also act cautiously, as
taking the wrong action could further jeopardize the mission!

== System Modules

Throughout the interior of the Orion capsule, several panels can be found. Each
one interfaces with a different essential system in the spacecraft and can be
used to detect and fix problems. Should any problems arise, it is imperative
that the astronauts make an exhaustive search of the capsule interior for panels
with an error status. All surfaces of the capsule should be thouroughly
searched. Each panel has a title on the top indicating which system it is
connected to and a status indicator on the top-right consisting of a
two-character display that conveys information about the state of the connected
system through the code it displays. Below the title and status indicator is the
system interface, which contains elements allowing astronauts to interact with
the system in order to repair it. If the system is repaired or progress is made,
this may be deduced from the status code. Thus, it is important for the
astronauts to relay the status code of a given panel and any changes thereto to
Mission Control, so that they may identify any essential information carried by
it using this manual.

#page[
  = Panel Usage

  This section details the operation and functionality of each type of panel
  present in the interior of the Orion capsule. To diagnose and fix issues with
  a given panel, consult the corresponding subsection and follow the
  instructions given.
]

#show heading.where(level: 2): it => [
  #pagebreak()
  #it
]

== Propulsion System

#ovr

In order for the propulsion system to work properly, it is important to ensure
that all of the electrical components involved are correctly connected. In the
event of a propulsion system failure, the wires must be checked and reconnected
so that the correct configuration is achieved for the launch sequence.

#ins

On the panel, there are two rows of sockets, each with eight sockets each. Above
each socket in the upper row, there is a button that can be toggled between
green and red, denoting the socket connection being activated and deactivated,
respectively. At the bottom of the panel there is a submit button. Observe the
Venn diagram and accompanying legend below. Each ellipse corresponds to a
property of the wires on the panel. For each wire, determine which region the
wire falls in. The unique letter in that region corresponds to whether the
corresponding connection needs to be activated (Y) or deactivated (N).

#align(
  center,
  table(
    columns: 2,
    align: (center + horizon, left + horizon),
    stroke: .4pt,
    line-sample(1), table.vline(stroke: none), [Blue present on wire],
    line-sample(2), [Upper socket is directly above lower socket],
    line-sample(3), [Upper socket is in left half of panel],
    line-sample(4), [Red present on wire]
  )
)

#v(-.03in) // get image to fit on page

#align(
  center,
  image(graphics-path + "venn-diagram.png")
)

Once you have activated and deactivated the connections appropriately, click the
submit button to apply the new configuration. If the configuration is correct,
the status indicator will show the code #status[BB] to indicate that the
propulsion system is working properly and no further action is required. Take
care when reconfiguring the propulsion system, as applying an improper
configuration will further jeopardize the mission and shrink the window of time
available before the mission must be called off.

== Heat Shield

#ovr

To repair the heat shield, it is necessary to reconfigure the six-block control
panel in order to provide full protection for the vehicle.

#ins

The heat shield interface has two parts: six buttons arranged in a grid and a
display below them. Multiple rounds of reconfiguration may be necessary; if the
heat shield has been damaged, the status indicator will display the number of
rounds of reconfiguration necessary to fully repair it. The display will show a
word. In order to perform the reconfiguration, read the button (marked with a
dot) corresponding to the word in the table below.

#align(
  center,
  grid(
    columns: 4,
    column-gutter: .7cm,
    row-gutter: 1.5mm,
    align: center + horizon,
    hstable(4)[right], hstable(1)[soyuz], hstable(3)[orbit], hstable(5)[optic],
    hstable(0)[their], hstable(2)[world], hstable(3)[would], hstable(0)[bezel],
    hstable(1)[oxide], hstable(4)[write], hstable(5)[there], hstable(2)[orion],
  )
)

Next, find the row in the table below corresponding to the word on the button
read and press the button containing the first word that appears in the
corresponding list, and you will move to the next round.

#align(
  center,
  table(
    columns: 2,
    align: (center + horizon, left + horizon),
    stroke: .4pt,
    [right], [world, bezel, write, orion],
    [soyuz], [bezel, orbit, right, optic],
    [orbit], [soyuz, would, right, their],
    [optic], [oxide, world, their, write],
    [their], [optic, orion, bezel, there],
    [world], [would, oxide, orbit, world],
    [would], [orion, write, world, right],
    [bezel], [optic, orbit, soyuz, would],
    [oxide], [write, right, oxide, soyuz],
    [write], [their, world, there, orbit],
    [there], [world, optic, there, bezel],
    [orion], [orbit, their, soyuz, right]
  )
)

Once all rounds have been completed, the status indicator will show the code
#status[BB] to signal that no more rounds of reconfiguration and the heat shield
has returned to proper functionality.

== Radiation Protection System

#ovr

Due to the extreme forces incurred by the spacecraft during flight, the
radiation control filters may become misaligned. When this happens, it is
necessary to realign them to ensure complete protection from radiation in the
Van Allen Belt.

#ins

On the control panel, a grid is displayed with four buttons below it, each with
an arrow facing a different direction. There is also a Submit button below
these. One square in the grid will be highlighted; this square represents the
current orientation of the control filter. Use the panel status to determine the
layout of the filter space according to the diagram below. The target position
of the filter in each layout is marked with a dot. The lines between the spaces
represent the movement contstraints of the filter; they can be treated as
barriers that the filters path cannot cross.

#align(
  center,
  grid(
    align: center + horizon,
    columns: 3, //3,
    column-gutter: 3mm,
    row-gutter: 3mm,
    stroke: none,
    status[AE], status[%4], status[8Þ],
    maze(1), maze(2), maze(3),
    status[Ð!], status[Q§], status[\<Y],
    maze(4), maze(5), maze(6)
  )
)

To realign the radiation control filter, use the buttons below the grid to plan
out a path for the radiation control filter to return to the target state from
the starting state. A movement in one direction will be canceled out by a
movement immediately following it in the opposite direction; thus, any errors
made while planning out the path can be corrected. Once the path has been
planned out, click the Submit button to execute the realignment. The highlighted
square will follow the designated path. If an incorrect step is encountered, an
error message will appear and the filter state will reset, allowing you to
attempt the realignment again. Once the filter has been properly realigned, the
panel status will change to #status[BB] to indicate that no further actions are
necessary.

== Life Support System

#ovr

Due to internal malfunctions and equipment damage, the life support
configuration files may become corrupted. When this happens, it is necessary for
the astronaut to reconfirm the crew settings through the digital control
panel.

#ins

On the panel, there are five letter displays laid out in a row, each surrounded
on the top and bottom by buttons that can be used to cycle through the available
letters for that display. There may be multiple rounds of settings to be
confirmed. For each round, choose letters to spell out one of the words in the
table below. Only one word will be possible to spell per round.

#align(
  center,
  table(
    columns: for i in range(5) { (2.5cm, ) },
    rows: for i in range(4) { (1.2cm, ) },
    align: center + horizon,
    stroke: .4pt,
    [orion], [orbit], [oxide], [optic], [admin],
    [about], [after], [again], [below], [bezel],
    [write], [right], [sound], [point], [there],
    [their], [world], [soyuz], [learn], [would]
  )
)

Once you have spelled the word on the letter displays, click the submit button
to check whether the correct input was given and move onto the next round. Once
all rounds have been completed, the status indicator will show the code
#status[BB] to signal that the settings have all successfully been restored and
the life support system is in working order.

== Keypad

#ovr

#ins

The panel contains four buttons arranged in a 2x2 grid, each bearing a unique
symbol. This combination of symbols will be present in only one of the tables
below.

#align(
  center,
  grid(
    columns: (2cm, 1cm, 2cm, 1cm, 2cm, 1cm, 2cm, 1cm, 2cm),
    rows: arrof(8, 2cm),
    align: center + horizon,
    stroke: (x, y) => if calc.rem(x, 2) == 0 {
      (
        left: .4pt,
        right: .4pt,
        top: .4pt,
        bottom: .4pt,
      )
    },
    keysym("01"), [], keysym("05"), [], keysym("15"), [], keysym("04"), [],
keysym("08"),
    keysym("02"), [], keysym("09"), [], keysym("10"), [], keysym("16"), [],
keysym("15"),
    keysym("03"), [], keysym("10"), [], keysym("16"), [], keysym("09"), [],
keysym("02"),
    keysym("04"), [], keysym("11"), [], keysym("17"), [], keysym("06"), [],
keysym("10"),
    keysym("05"), [], keysym("12"), [], keysym("04"), [], keysym("13"), [],
keysym("03"),
    keysym("06"), [], keysym("13"), [], keysym("18"), [], keysym("18"), [],
keysym("14"),
    keysym("07"), [], keysym("02"), [], keysym("19"), [], keysym("07"), [],
keysym("19"),
    keysym("08"), [], keysym("14"), [], keysym("20"), [], keysym("12"), [],
keysym("12"),
  )
)

First, identify which table contains all four symbols. Then, the
astronaut must press the button in the order in which the corresponding symbols
appear in the table, with the table read from top to bottom. If the buttons were
pressed in the correct order, the panel status will change to #status[BB] to
indicate that no further action is needed. If not, the panel will reset and the
process must be repeated.
