import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DeviceView from '@/views/samples/DeviceView.vue'
import CameraView from '@/views/samples/CameraView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    }, 
    {
      path: '/samples/device',
      name: 'device',
      component: DeviceView
    },
    {
      path: '/samples/camera',
      name: 'camera',
      component: CameraView
    }
  ]
})

export default router
