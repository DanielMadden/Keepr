<template>
  <div class="viewport">
    <div class="container-fluid">
      <div class="row">
        <h1>{{ vault.name }}</h1>
      </div>
      <div class="row">
        {{ vault.description }}
      </div>
      <div class="row">
        <span>Keeps: {{ keeps.length }}</span>
      </div>
    </div>
    <div class="masonry-4">
      <Keep v-for="keep in keeps" :key="keep.id" :keep="keep"></Keep>
    </div>
  </div>
</template>
<script>
import { computed, onMounted } from 'vue'
import { vaultService } from '../services/VaultService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    // Variables
    const route = useRoute()
    const vault = computed(() => AppState.activeVault)
    const keeps = computed(() => AppState.activeVaultKeeps)
    // On Mounted
    onMounted(() => {
      vaultService.getVault(route.params.id)
      vaultService.getKeeps(route.params.id)
    })
    return {
      // Variabls
      vault,
      keeps
    }
  }
}
</script>
<style scoped>
@import "../assets/css/page-vault.css";
</style>
