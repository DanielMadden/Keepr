<template>
  <div>
    <!-- TODO change v-if back to vault -->
    <div class="viewport" v-if="vault" @click="clearDelete()">
      <div v-show="!animating">
        <div class="container-fluid" v-if="vault == 'deleting'">
          <span>Deleting...</span>
        </div>
        <div class="container-fluid" v-else>
          <div class="row d-flex align-items-center">
            <h1>{{ vault.name }}</h1>
            <div
              id="vault-delete"
              v-if="account.id == vault.creator.id"
              :class="{ shake: deleting, confirm: deleting }"
              @click="deleteVault($event)"
            >
              <h1>
                <i class="fa fa-trash"></i>
              </h1>
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
          <Keep
            v-for="keep in keeps"
            :key="keep.id"
            :keep="keep"
            :page="'Vault'"
          ></Keep>
        </div>
      </div>
    </div>
    <div class="viewport" v-show="!vault || animating">
      <div
        class="loader-page-container"
        :class="{ showMessage: animating && !opening }"
      >
        <div
          class="loader-page-spinner d-flex justify-content-center align-items-center"
          :class="{ 'animate-out': authorized }"
        >
          <i
            class="fas fa-certificate"
            :class="{ 'slow-spin': authorizing || authorized }"
          ></i>
        </div>
        <div
          class="loader-page-logo no-stretch d-flex justify-content-center align-items-center"
          :class="{ 'animate-out': authorized }"
        >
          <!-- <i
            v-if="authorized && !loggingIn && !account.id"
            class="fab fa-kickstarter-k"
          ></i> -->
          <i v-if="!authorized && loggingIn" class="fas fa-key"></i>
          <i v-else-if="!authorized" class="fas fa-lock"></i>
          <i v-else class="fas fa-unlock-alt"></i>
        </div>
        <div
          class="loader-page-message d-flex justify-content-center align-items-center"
          :class="{ 'pb-5 px-5': animating && !opening }"
        >
          <span v-if="animating && !opening">
            <span v-if="isPrivate && authorizing && !loggingIn && !account.id">
              Vault is private. Please log in to view.
            </span>
            <span v-else-if="isPrivate && authorizing && loggingIn">
              Logging in...
            </span>
            <span v-else-if="isPrivate && !authorizing && !authorized">
              You are not authorized to open this vault.
            </span>
            <span v-else>Opening Vault...</span>
          </span>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onBeforeUnmount, onMounted, watchEffect } from 'vue'
import { vaultService } from '../services/VaultService'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    // Variables
    const route = useRoute()
    const router = useRouter()
    const account = computed(() => AppState.account)
    const vault = computed(() => AppState.activeVault)
    const keeps = computed(() => AppState.activeVaultKeeps)
    const authorized = computed(() => AppState.authorized.vault)
    const authorizing = computed(() => AppState.authorizing.vault)
    const loggingIn = computed(() => AppState.loggingIn)
    const isPrivate = computed(() => AppState.isPrivate.vault)
    const animating = computed(() => AppState.animating.vault)
    const opening = computed(() => AppState.opening.vault)
    // Get Vault
    const getVault = async () => {
      try {
        await vaultService.getVault(route.params.id)
        await vaultService.getKeeps(route.params.id)
      } catch (error) {
        AppState.isPrivate.vault = true
      }
    }
    // On Mounted
    onMounted(() => {
      AppState.opening.vault = false
      AppState.isPrivate.vault = false
      AppState.animating.vault = true
      AppState.authorizing.vault = true // Begin attempts to authorize
      getVault()
    })
    // Watch for login, try getting again in case it's a private vault
    watchEffect(async () => {
      if (account.value.id) {
        await getVault()
        // Once this is sent, we know that we will no longer attempt to authorize:
        AppState.authorizing.vault = false
      }
    })
    // Functions
    let deleteTimeout
    const clearDelete = () => {
      clearInterval(deleteTimeout)
      AppState.deleting.vault = false
    }
    const deleting = computed(() => AppState.deleting.vault)
    const deleteVault = async (e) => {
      e.stopPropagation()
      if (deleting.value) {
        const vaultId = vault.value.id
        AppState.activeVault = 'deleting'
        await vaultService.delete(vaultId)
        router.push({ path: '/profile/' + account.value.id })
      } else {
        AppState.deleting.vault = true
        deleteTimeout = setTimeout(() => { AppState.deleting.vault = false }, 3000)
      }
    }
    // If this is changed (through navbar), clear the delete timeout
    watchEffect(() => { if (AppState.deleting.vault === false) { clearInterval(deleteTimeout) } })
    // Before UnMount
    onBeforeUnmount(() => {
      clearDelete()
      AppState.activeVault = null
      AppState.activeVaultKeeps = []
      AppState.isPrivate.vault = false
    })
    return {
      // Variables
      account,
      vault,
      keeps,
      deleting,
      authorized,
      authorizing,
      loggingIn,
      isPrivate,
      animating,
      opening,
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
