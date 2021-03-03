<template>
  <div class="viewport">
    <div class="container-fluid">
      <div class="row">
        <h1>{{ vault.name }}</h1>
        <div
          id="modal-keep-delete"
          v-if="account.id == keep.creator.id"
          :class="{ shake: deleting, confirm: deleting }"
          @click="deleteKeep($event)"
        >
          <i class="fa fa-trash"></i>
        </div>
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
import { computed, onBeforeUnmount, onMounted } from 'vue'
import { vaultService } from '../services/VaultService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    // Variables
    const route = useRoute()
    const account = computed(() => AppState.account)
    const vault = computed(() => AppState.activeVault)
    const keeps = computed(() => AppState.activeVaultKeeps)
    // On Mounted
    onMounted(() => {
      vaultService.getVault(route.params.id)
      vaultService.getKeeps(route.params.id)
    })
    // Functions
    let deleteTimeout
    const clearDelete = () => {
      clearInterval(deleteTimeout)
      AppState.deleting.keep = false
    }
    const deleting = computed(() => AppState.deleting.vault)
    const deleteVault = (e) => {
      e.stopPropagation()
      if (deleting.value) {
        vaultService.delete(vault.value.id)
      } else {
        AppState.deleting.keep = true
        deleteTimeout = setTimeout(() => { AppState.deleting.keep = false }, 3000)
      }
    }
    // Before UnMount
    onBeforeUnmount(() => {
      clearDelete()
    })
    return {
      // Variables
      account,
      vault,
      keeps,
      // Functions
      clearDelete,
      deleteVault
    }
  }
}
</script>
<style scoped>
@import "../assets/css/page-vault.css";
</style>
