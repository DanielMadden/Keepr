<template>
  <div class="viewport">
    <div class="masonry-4">
      <Keep v-for="keep in keeps" :key="keep.id" :keep="keep"></Keep>
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
    onMounted(async () => {
      keepService.getAll()
    })
    return {
      // Variables
      keeps
      // Functions
    }
  }
}
</script>

<style scoped>
@import "../assets/css/page-home.css";
</style>
