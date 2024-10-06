export enum BatterySource {
    /// <summary>Power source cannot be determined.</summary>
    Unknown = 0,
    /// <summary>Power source is the battery and is currently not being charged.</summary>
    Battery = 1,
    /// <summary>Power source is an AC Charger.</summary>
    AC = 2,
    /// <summary>Power source is a USB port.</summary>
    Usb = 3,
    /// <summary>Power source is wireless.</summary>
    Wireless = 4
}

export enum BatteryState
{
    /// <summary>Battery state could not be determined.</summary>
    Unknown = 0,
    /// <summary>Battery is actively being charged by a power source.</summary>
    Charging = 1,
    /// <summary>Battery is not plugged in and discharging.</summary>
    Discharging = 2,
    /// <summary>Battery is full.</summary>
    Full = 3,
    /// <summary>Battery is not charging or discharging, but in an inbetween state.</summary>
    NotCharging = 4,
    /// <summary>Battery does not exist on the device.</summary>
    NotPresent = 5
}

export interface Battery {
    chargeLevel: Number
    powerSource: BatterySource
    state: BatteryState
}

type BatteryWatchCallback = (battery: Battery) => void;

class Device extends PluginBase {
    #batteryCallback: BatteryWatchCallback | null = null;

    async deviceId() {
        return await this.invokeMethodAsync("deviceId");
    }

    async installationId() {
        return await this.invokeMethodAsync("installationId");
    }

    async readDeviceInfo() {
        return await this.invokeMethodAsync("readDeviceInfo");
    }

    async displayInfo() {
        return await this.invokeMethodAsync("displayInfo")
    }
    
    async watchBattery(cb: BatteryWatchCallback) {
        this.#batteryCallback = cb;
        await this.invokeMethodAsync("watchBattery");
    }

    batteryStateChanged(info: Battery) {
        this.#batteryCallback(info);
    }
}
  
MauiBundler.Plugins.Device = new Device();
  