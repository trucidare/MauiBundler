<template>
  <header>
    <img src="@/assets/images/maui.png" class="img-maui" />
    <img src="@/assets/images/logo.svg" class="img-vue" />
  </header>
  <main>
    <div style="padding-top: 15px;">
      <span style="font-size: x-large;">Plugin Samples</span>
    </div>
    <hr />
    <div class="pt-20 flex flex-col gap">
      <RouterLink class="item-link" to="/samples/device">
        <iconify-icon icon="ic:round-devices-other" style="font-size: 3em;"  />
        <span>Device</span>
      </RouterLink>
      <a class="item-link">
        <iconify-icon icon="ic:baseline-attractions" style="font-size: 3em;"  />
        <span>App Actions</span>
      </a>
      <RouterLink class="item-link" to="/samples/camera">
        <iconify-icon icon="ic:baseline-camera" style="font-size: 3em;"  />
        <span>Camera</span>
      </RouterLink>
      <a class="item-link">
        <iconify-icon icon="ic:baseline-bluetooth" style="font-size: 3em;"  />
        <span>Bluetooth</span>
      </a>
      <a class="item-link">
        <iconify-icon icon="ic:baseline-file-copy" style="font-size: 3em;"  />
        <span>Filesystem</span>
      </a>
      <a class="item-link">
        <iconify-icon icon="ic:baseline-add-location" style="font-size: 3em;"  />
        <span>Geolocation</span>
      </a>
      <a class="item-link">
        <iconify-icon icon="ic:baseline-message" style="font-size: 3em;"  />
        <span>Intent</span>
      </a>
    </div>
  </main>
</template>

<script setup lang="ts">
import { ref } from 'vue';


const pic = ref({} as { filename: string, fullPath: string, dataUrl: string });

const pick = async () => {
  //@ts-ignore
  pic.value = await window.MauiBundler.Plugins.Camera.pickPhoto();
};

const take = async () => {
  //@ts-ignore
  pic.value = await window.MauiBundler.Plugins.Camera.takePhoto();
};
</script>


<style scoped>
  .pt-20 {
    padding-top: 20px;
  }
  .flex {
    display: flex;
  }

  .flex-col {
    flex-direction: column;
  }

  .gap {
    gap: 1rem;
  }

  .img-maui {
    width: 100%;
  }

  .img-vue {
    width: 20%;
    position: absolute;
    right: 3rem;
    top: 6rem;
  }

  .item-link {
    border: .5px solid rgba(255,255,255,.2); 
    border-radius: 8px; 
    padding: 5px 10px; 
    display: flex; 
    align-items: center; 
    justify-content: space-between;
  }
</style>