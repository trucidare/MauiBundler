<template>
    <header>
        <a href="/" style="font-size: 1.5em; display: flex; align-items: center; gap: 5px;">
            <iconify-icon icon="ic:baseline-arrow-back" />
            <span>Back</span>
        </a>
    </header>
    <hr />
    <div style="padding-top: 20px; display: flex; flex-direction: column; gap: 5px;">
        <div style="border-bottom: 1px solid rgba(255,255,255,.4);">
            <span style="font-size: larger;">Installation ID</span>
            <p style="font-size: small;">-> {{ deviceInfos.installationId }}</p>
        </div>
        <div style="border-bottom: 1px solid rgba(255,255,255,.4);">
            <span style="font-size: larger;">Device ID</span>
            <p style="font-size: small;">-> {{ deviceInfos.deviceId }}</p>
        </div>
        <div style="border-bottom: 1px solid rgba(255,255,255,.4);">
            <span style="font-size: larger;">Device Info</span>
            <pre style="font-size: small;">
                {{ deviceInfos.info }}
            </pre>
        </div>
        <div style="border-bottom: 1px solid rgba(255,255,255,.4);">
            <span style="font-size: larger;">Display Info</span>
            <pre style="font-size: small;">
                {{ deviceInfos.display }}
            </pre>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';

const deviceInfos = ref({} as {info: object, display: object, installationId: string, deviceId: string})

async function readDeviceInfo() {
    //@ts-expect-error
    deviceInfos.value.info = await window?.MauiBundler?.Plugins.Device.readDeviceInfo();
   //@ts-expect-error
    deviceInfos.value.display = await window?.MauiBundler?.Plugins.Device.displayInfo();
    //@ts-expect-error
    deviceInfos.value.installationId = await window?.MauiBundler?.Plugins.Device.installationId();
    //@ts-expect-error
    deviceInfos.value.deviceId = await window?.MauiBundler?.Plugins.Device.deviceId();
}

onMounted(async () => {
    readDeviceInfo();
})

</script>