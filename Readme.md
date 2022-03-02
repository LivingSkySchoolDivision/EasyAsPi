# Easy as Pi

This code is for the _Easy As Pi_ project at Living Sky School Division.

This project involves getting 300 Raspberry Pis to collect environmental data using Pimoroni Enviro+ modules, and to report that data back to a central system where that data can be displayed in a central manner, ideally on TV-sized displays, in map form.

## Anatomy of a Pi sensor module
The following components make up a sensor module:
 - A Raspberry Pi Zero W board (we are using the first generation boards)
 - An Enviro+ module
   - https://learn.pimoroni.com/article/getting-started-with-enviro-plus
   - https://www.pishop.ca/product/enviro-for-raspberry-pi/
 - A 3D-Printed case - https://www.thingiverse.com/thing:3954675
   - Note: We did not design this case, and claim no credit for it. We merely found it on Thingiverse.
 - A MicroSD card for the Pi
 - An official Raspberry Pi power supply



 # API Usage

The API should _not_ be accessible to the Internet, as it has _no_ security of any kind built in.

 ## Retrieving a list of sensors

 ```
 /Sensors
 ```

 ## Retrieving sensor readings for a sensor
 ```
 /SensorReading/00000000-0000-0000-0000-000000000000                
 ```
 Where `00000000-0000-0000-0000-000000000000` is the GUID of the sensor you wish to retrieve readings from.

 ## Sending data from the sensor Pi to the API
 Do a _POST_ HTTP request to `/SensorReading`, with a payload that looks like this:

```json
{
  "deviceSerialNumber": "SERIAL NUMBER GOES HERE",
  "deviceModel": "MODEL NAME GOES HERE",
  "temperatureCelsius": 999.99,
  "humidityPercent": 999.99,
  "pressure": 999.99,
  "lux": 999.99,
  "noise": 999.99,
  "oxidisingGasLevel": 999.99,
  "reducingGasLevel": 999.99,
  "nH3Level": 999.99,
  "nitrogenDioxideLevel": 999.99,
  "carbonMonoxideLevel": 999.99,
  "ammoniaLevel": 999.99,
  "cpuTemperatureCelsius": 999.99,
  "temperatureAlarmHigh": false,
  "temperatureAlarmLow": false,
  "humidityAlarmHigh": false,
  "humidityAlarmLow": false,
  "oxidisedAlarmHigh": false,
  "reducingGasAlarmHigh": false,
  "nH3AlarmHigh": false,
  "noiseAlarmHigh": false
}
```

The API will automatically create a new `Sensor` object for the associated sensor if it does not exist already - this is based on the serial number given in the sensor reading payload.

All fields are optional _except_ `deviceSerialNumber`, though the API will reject payloads that do not have enough sensor readings in them. 

The following is would be considered a minimum viable payload, assuming a requirement of 3 data points:

```json
{
  "deviceSerialNumber": "SERIAL NUMBER GOES HERE",
  "temperatureCelsius": 999.99,
  "humidityPercent": 999.99,
  "pressure": 999.99
}
```

## Sensor reading reply

If accepted, the API will always respond with an HTTP status 200, and the following JSON content in the body:

```json
{
  "assignedNumber": 0
}
```

This assigned number is unique per sensor, and is/will be displayed on the Pi screen to make identification easier at a glance. The API will automatically assign numbers to new devices.
