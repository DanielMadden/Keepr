<template>
  <header>
    <!-- <Navbar /> -->
    <Nav></Nav>
  </header>
  <main>
    <Modal></Modal>
    <router-view v-slot="{ Component }">
      <transition name="route" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </main>
  <!-- <footer></footer> -->
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style scoped>
@import "./assets/css/global.css";
</style>
