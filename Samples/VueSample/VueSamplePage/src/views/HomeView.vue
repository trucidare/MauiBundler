<script setup lang="ts">
import { ref } from 'vue';


const pic = ref({} as {filename: string, fullPath: string, dataUrl: string });

const pick = async () => {
  //@ts-ignore
  pic.value = await window.MauiBundler.Plugins.Camera.pickPhoto();
};

const take = async () => {
  //@ts-ignore
  pic.value = await window.MauiBundler.Plugins.Camera.takePhoto();
};
</script>

<template>
  <main>
    <div style="display: flex; flex: auto; flex-grow: 1; justify-content: space-between;">
      <div>
        <a @click="pick()">Pick photo</a>
      </div>
      <div>
        <a @click="take()">Take photo</a>
      </div>
    </div>
    <div style="max-width: fit-content;">
      <img :src="pic?.dataUrl" />
    </div>
  </main>
</template>

<style scoped>
img {
  max-width: 100%;
  max-height: 100%;
}
</style>