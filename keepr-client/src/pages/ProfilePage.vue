<template lang="">
  <div class="viewport pinch">
    <div class="container-fluid">
      <div v-if="profile">
        <div id="profile-row-profile"
             class="row d-flex justify-content-start px-2"
        >
          <div id="profile-picture"
               class="mr-5"
               :style="`background: 0, 0, url('${profile.picture}') no-repeat center center /cover; overflow-y: hidden`"
          ></div>
          <div id="profile-stats"
               class="d-flex flex-column justify-content-center"
          >
            <h1>{{ profile.name }}</h1>
            <h3>Vaults: {{ vaults.length }}</h3>
            <h3>Keeps: {{ keeps.length }}</h3>
          </div>
        </div>
        <div class="row pt-3 pb-5"></div>
        <div id="profile-row-name-vaults"
             class="row d-flex justify-content-start align-items-center px-2 mb-3"
        >
          <h1 class="mr-3">
            Vaults
          </h1>
          <button class="profile-button-add d-flex justify-content-center align-items-center"
                  v-if="profile.id == account.id"
                  @click="addVault()"
          >
            <i class="fas fa-plus"></i>
          </button>
        </div>
        <div id="profile-row-vaults"
             class="row"
        >
          <div class="profile-col-vaults col-2"
               v-for="vault in vaults"
               :key="vault.id"
               v-show="loadedVaults"
          >
            <Vault :vault="vault"></Vault>
          </div>
          <div class="profile-loading pl-2 col-2 d-flex justify-content-center align-items-center" v-if="!loadedVaults">
            <i class="fas fa-sync-alt fa-spin"></i>
          </div>
        </div>
        <div class="row py-3"></div>
        <div id="profile-row-name-keeps"
             class="row d-flex justify-content-start align-items-center px-2 mb-3"
        >
          <h1 class="mr-3">
            Keeps
          </h1>
          <button class="profile-button-add d-flex justify-content-center align-items-center"
                  v-if="profile.id == account.id"
                  @click="addKeep()"
          >
            <i class="fas fa-plus"></i>
          </button>
        </div>
        <div id="profile-row-keeps"
             class="row"
        >
          <div v-show="loadedKeeps" class="masonry-6">
            <Keep v-for="keep in keeps" :key="keep.id" :keep="keep"></Keep>
          </div>
          <div class="profile-loading pl-2 col-2 d-flex justify-content-center align-items-center" v-if="!loadedKeeps">
            <i class="fas fa-sync-alt fa-spin"></i>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onBeforeUnmount, onMounted, watchEffect } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import { openModal } from '../utils/Modal'
export default {
  setup() {
    // Varaibles
    const route = useRoute()
    const account = computed(() => AppState.account)
    const profile = computed(() => AppState.activeProfile)
    const vaults = computed(() => AppState.activeProfileVaults)
    const keeps = computed(() => AppState.activeProfileKeeps)
    const loadedVaults = computed(() => AppState.loaded.profileVaults)
    const loadedKeeps = computed(() => AppState.loaded.profileKeeps)
    // On Mount
    onMounted(async () => {
      AppState.loaded.profileVaults = false
      AppState.loaded.profileKeeps = false
      profileService.getProfile(route.params.id)
      profileService.getVaults(route.params.id)
      profileService.getKeeps(route.params.id)
    })
    // Watch for login, try getting private vaults
    watchEffect(() => { if (account.value.id) { profileService.getVaults(route.params.id) } })
    // Watch for keeps and vaults loading in
    watchEffect(() => { if (vaults.value.length > 0) { setTimeout(() => { AppState.loaded.profileVaults = true }, 500) } })
    watchEffect(() => { if (keeps.value.length > 0) { setTimeout(() => { AppState.loaded.profileKeeps = true }, 500) } })
    // Functions
    const addVault = () => { openModal('add_vault') }
    const addKeep = () => { openModal('add_keep') }
    // Clear the user after leaving
    onBeforeUnmount(() => {
      AppState.activeProfile = null
      AppState.activeProfileVaults = []
      AppState.activeProfileKeeps = []
    })
    return {
      // Variables
      account,
      profile,
      vaults,
      keeps,
      loadedVaults,
      loadedKeeps,
      // Functions
      addVault,
      addKeep
    }
  }
}
</script>
<style scoped >
@import "../assets/css/page-profile.css";
</style>
