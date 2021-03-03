<template>
  <div>
    <div class="viewport" v-show="loaded && !animating">
      <!-- <transition name="fade"> -->
      <div class="masonry-6">
        <Keep v-for="keep in keeps" :key="keep.id" :keep="keep"></Keep>
      </div>
      <!-- </transition> -->
    </div>
    <!-- TODO I'm hiding this because I swear Parker's only here to watch our computers and try to 1-up us FFS -->
    <div class="viewport" v-show="animating || !loaded">
      <div class="loader-page-container">
        <div
          class="loader-page-spinner d-flex justify-content-center align-items-center animate-in"
        >
          <i class="fas fa-certificate slow-spin"></i>
        </div>
        <div
          class="loader-page-logo no-stretch d-flex justify-content-center align-items-center animate-in"
        >
          <i class="fab fa-kickstarter-k"></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { keepService } from '../services/KeepService'
import { AppState } from '../AppState'

export default {
  name: 'Home',
  setup() {
    const keeps = computed(() => AppState.homeKeeps)
    const loaded = computed(() => AppState.loaded.home)
    const animating = computed(() => AppState.animating.home)
    onMounted(async () => {
      AppState.animating.home = true
      await keepService.getAll()
      setTimeout(() => {
        AppState.loaded.home = true
      }, 1000)
      setTimeout(() => {
        AppState.animating.home = false
      }, 1000)
    })
    return {
      // Variables
      keeps,
      loaded,
      animating
      // Functions
    }
  }
}
</script>

<style scoped>
@import "../assets/css/page-home.css";
</style>
