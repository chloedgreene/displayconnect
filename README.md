#  Display Connect

Display Connect is a simple utility to control a raspberry PI sense hat.

## Install:

### Step 1: Node Red
Get a node-red server set up with the [sense hat package](https://flows.nodered.org/node/node-red-node-pi-sense-hat "sense hat package") and flow that is in the node-red branch

### Step 2: DisplayConnect Software
Next, just from the releses tab get the "DisplayConnect.exe" file.

#### Windows:
Just make sure you have .net runtime installed

#### Linux/MacOs:
Use the Mono Runtime to launch the software

### Step 3: Connect Windows/Linux/MacOS to the raspberry pi
On the raspberry pi run:
`hostname -I`
The Output should be like:
`192.168.0.11 2607:blah blah blah...'

Then is the DisplayConnect Software put the IP(Ex, 192.168.0.11) in to the IP address Field
And is everything went right, It should work

## Build:
Install VisualStudio or Monodevelop and open the project on the main branch and click "ReBuild"
