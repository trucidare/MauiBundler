export interface ScanResult {
    isConnectable: Boolean | null;
    isConnected: Boolean,
    lastSeen: Date | null,
    localName: String | null
    manufacturerData: object | null,
    name: string | null,
    peripheral: Peripheral | null,
    rssi: number,
    serviceData: object | null,
    serviceUuids: string[] | null,
    txPower: object | null,
    uuid: string | null
}

export interface Peripheral 
{
    uuid: String | null;
    name: String | null;
    mtu: Number,
    status: Number
}

class Bluetooth extends PluginBase {

    scanForDevices() {
        this.invokeMethodAsync("scanForDevices");
        console.log("MauiBundler::JS -> Start BLE scanning");
    }

    stopDeviceScan() {
        this.invokeMethodAsync("stopDeviceScan");
    }
    
    deviceFound(device: ScanResult) {
        console.log("DeviceFound", device);
    }
}

MauiBundler.Plugins.Bluetooth = new Bluetooth();
