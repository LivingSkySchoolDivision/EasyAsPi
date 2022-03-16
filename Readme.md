# Easy as Pi
This code is for the _Easy As Pi_ project at Living Sky School Division.

This project involves getting 300 Raspberry Pis to collect environmental data using Pimoroni Enviro+ modules, and to report that data back to a central system where that data can be displayed in a central manner, ideally on TV-sized displays, in map form.

# Raspberry Pi specific code and documentation
This repository contains all code for this project __except__ the code that runs on the Raspberry Pis - Raspberry Pi code has it's own repository here: https://github.com/LivingSkySchoolDivision/EasyAsPi-Pi

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

## Sending a heartbeat signal to the API
A "heartbeat" signal is used for several things:
- Retrieving the assigned ID number for a sensor
- Registering new sensors in the system
- Update checks
- Tracking the last time the system has seen a given sensor

Heartbeat signals should be sent to the API at regular intervals, but potentially at a much slower rate than normal sensor data is. It's recommended to send a signal at least once per 24 hours, and no more than once per hour.

A heartbeat is a _POST_ HTTP request to `/Heartbeat`, with a `JSON` body that looks like this:

```json
{
	"deviceSerialNumber": "SERIAL NUMBER GOES HERE",
	"deviceModel" : "DEVICE MODEL NAME GOES HERE"
}
```
Where `deviceSerialNumber` is the unique serial number of the raspberry pi board, and `deviceModel` is the full model name of the raspberry pi board.

If successful, you will receive a response like this:

```json
{
	"assignedNumber": 7,
	"versionNumber": "VERSIONNUMBERHERE"
}
```

`assignedNumber` is an API-assigned number for a specific sensor unit. It's intended for this number to be displayed on the sensor unit itself, or at least written on it somewhere. This number is so that sensor units can be easily identified by non-technical staff during installation or troubleshooting (ie: so that non-technical staff don't need to find a serial number or MAC address when installing them). The API will automatically generate this number for sensors that it has not seen before.

`versionNumber` is a string that indicates if the sensor is using the correct version of it's code. The sensors are expected to keep track of which version of the scripts they are using, and use this as an indicator of when to update themselves. This value could be _any_ string, and may not be sequential - it's best to simply check to see if this value matches the known value on the sensor, and attempt an update if the values differ.

## Sending data from the sensor Pi to the API
Do a _POST_ HTTP request to `/SensorReading`, with a `JSON` body that looks like this:

```json
{
  "deviceSerialNumber": "SERIAL NUMBER GOES HERE",
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
It's recommended to send sensor data at least several times per hour, ideally no more than once every 5 minutes.

The API will reject a request if it does not contain a serial number, and/or if it does not contain at least 1 valid sensor reading.
