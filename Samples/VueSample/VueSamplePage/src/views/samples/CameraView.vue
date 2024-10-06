<template>
    <header>
        <a href="/" style="font-size: 1.5em; display: flex; align-items: center; gap: 5px;">
            <iconify-icon icon="ic:baseline-arrow-back" />
            <span>Back</span>
        </a>
    </header>
    <hr />
    <div style="padding-top: 20px; display: flex; flex-direction: column; gap: 5px;">
        <div style="display: flex; flex: auto; flex-grow: 1; justify-content: center; gap: 2rem">
            <div  @click="pick()" style="display: flex; flex-direction: column; align-items: center; padding: 10px; border: .5px solid rgba(255,255,255,.4); border-radius: 8px;">
                <iconify-icon icon="ic:baseline-image" style="font-size: xx-large;"/>
                <a>Pick photo</a>
            </div>
            <div  @click="take()" style="display: flex; flex-direction: column; align-items: center; padding: 10px; border: .5px solid rgba(255,255,255,.4); border-radius: 8px;">
                <iconify-icon icon="ic:baseline-camera" style="font-size: xx-large;"/>
                <a>Take photo</a>
            </div>
        </div>
        <div style="max-width: fit-content; padding-top: 35px;">
            <img :src="pic?.dataUrl" />
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';


const pic = ref({} as {filename: string, fullPath: string, dataUrl: string });

const pick = async () => {
  //@ts-ignore
  pic.value = await window.MauiBundler.Plugins.Camera.pickPhoto();
};

const take = async () => {
  //@ts-ignore
  pic.value = await window.MauiBundler.Plugins.Camera.takePhoto();
};

onMounted(async () => {

})

</script>

<style lang="css" scoped>
img {
  max-width: 100%;
  max-height: 100%;
}
</style>