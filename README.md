# QM1571-Logger

This is a simple logger for a cheap and not very accurate multimeter from JayCar sold as Digitech QM1571.

The app comes with it was not coping well with the logging task. I reverse-enginnered the code, removed all the visual part and turned it into a CLI app.

### How to use it

1. Connect the receiver to a USB port on your computer.
2. Power on the multimeter.
3. Press the Wifi button on the mutimeter front panel.
4. The light on the receiver should start flashing when the connection has been established.
5. Run the app from Windows command line.

The app will output some state info and start logging the data into a CSV file with a timestamp for the file name saved on your Desktop.

### What works

I successfully logged about 2.5 events per second for more than 48 hrs until the battery in the mutimeter ran out.

### What needs work

The app is pretty crude. I wrote it for a one-off use, so it wasn't worth making it pretty or robust. There may be bugs and there are a few places where the code is very inefficient.

An attempt to connect more than one multimeter via multiple receivers failed. Both multimeters tried to connect to the same receiver. There may be a workaround for that. I suspect they are paired automatically on the first run. Try pairing the new one with its own receiver first and then connect and power up the second one.

The app selects the first (or the last?) COM port with a matching name to get the data feed, but it's not a big change to make it configurable. So if you can pair them successfully you can change the app to allow configurable COM ports.
