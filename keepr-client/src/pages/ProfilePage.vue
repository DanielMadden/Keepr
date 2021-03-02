<template lang="">
  <div class="viewport">
    <div class="container-fluid">
      <div v-if="profile">
        <div id="profile-row-profile"
             class="row d-flex justify-content-start"
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
        <div id="profile-row-name-vaults"
             class="row d-flex justify-content-start align-items-center"
        >
          <h1 class="mr-3">
            Vaults
          </h1>
          <div class="profile-button-add d-flex justify-content-center align-items-center"
               @click="addVault()"
          >
            <span>+</span>
          </div>
        </div>
        <div id="profile-row-vaults"
             class="row"
        >
          <div class="profile-col-vaults col-2"
               v-for="vault in vaults"
               :key="vault.id"
          >
            <Vault :vault="vault"></Vault>
          </div>
        </div>
        <div id="profile-row-name-keeps"
             class="row d-flex justify-content-start align-items-center"
        >
          <h1 class="mr-3">
            Keeps
          </h1>
          <div class="profile-button-add d-flex justify-content-center align-items-center"
               @click="addKeep()"
          >
            +
          </div>
        </div>
        <div id="profile-row-keeps"
             class="row"
        >
          <div class="masonry-6">
            <Keep v-for="keep in keeps" :key="keep.id" :keep="keep"></Keep>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onMounted } from 'vue'
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
    // On Mount
    onMounted(async () => {
      profileService.getProfile(route.params.id)
      profileService.getVaults(route.params.id)
      profileService.getKeeps(route.params.id)
    })
    // Functions
    const addVault = () => { openModal('add_vault') }
    const addKeep = () => { openModal('add_keep') }
    return {
      // Variables
      account,
      profile,
      vaults,
      keeps,
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
