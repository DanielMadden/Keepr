<template lang="">
  <div class="row"
       id="modal-keep-container"
       @click="clearDelete()"
  >
    <div class="col-6"
         id="modal-keep-img"
         :style="`background: linear-gradient( rgba(0, 0, 0, 0.0), rgba(0, 0, 0, 0)), url('${keep.img}') no-repeat center center /cover; overflow-y: hidden`"
    ></div>
    <div class="col-6"
         id="modal-keep-content"
    >
      <div class="row d-flex justify-content-end"
           id="modal-keep-row-x"
           @click="closeModals()"
      >
        X
      </div>
      <div class="row d-flex justify-content-center mb-3 align-items-center"
           id="modal-keep-row-stats"
      >
        <div class="modal-keep-stats">
          <i class="fas fa-eye"></i> {{ keep.views }}
        </div>
        <div class="modal-keep-stats mx-5">
          <i class="fab fa-kickstarter"></i> {{ keep.keeps }}
        </div>
        <div class="modal-keep-stats">
          <i class="fas fa-share-alt"></i> {{ keep.shares }}
        </div>
      </div>
      <div class="row d-flex justify-content-center mb-3 pt-2"
           id="modal-keep-row-title"
      >
        <h1>
          {{ keep.name }}
        </h1>
      </div>
      <div class="row"
           id="modal-keep-row-description"
      >
        <p class="m-0"
           id="modal-keep-description"
        >
          {{ keep.description }}
        </p>
      </div>
      <div class="row d-flex justify-content-center"
           id="modal-keep-row-tags"
      >
        <div class="d-flex justify-content-center mb-5 mt-5 pt-3"
             id="modal-keep-tags"
        >
          Hi
          {{ keep.tags }}
        </div>
      </div>
      <div class="row d-flex justify-content-between mt-3"
           id="modal-keep-row-bottom"
      >
        <div class="dropdown">
          <button id="modal-keep-add-to-vault"
                  class="dropdown-toggle"
                  type="button"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
          >
            Add To Vault
          </button>
          <div class="dropdown-menu" aria-labelledby="modal-keep-add-to-vault">
            <a class="dropdown-item"
               v-for="vault in vaults"
               :key="vault.id"
               @click="keepToVault(vault.id)"
            >{{ vault.name }}</a>
            <!-- <a class="dropdown-item" href="#">Another action</a>
            <a class="dropdown-item" href="#">Something else here</a> -->
          </div>
        </div>
        <div id="modal-keep-delete"
             v-if="account.id == keep.creator.id"
             :class="{'shake':deleting, 'confirm':deleting}"
             @click="deleteKeep($event)"
        >
          <i class="fa fa-trash"></i>
        </div>
        <div id="modal-keep-profile"
             class="d-flex justify-content-end align-items-center"
        >
          <div id="modal-keep-profile-picture"
               class="mr-2"
               :style="`background: 0, 0, url('${keep.creator.picture}') no-repeat center center /cover; overflow-y: hidden`"
          ></div>
          <div id="modal-keep-profile-name">
            {{ keep.creator.name }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onBeforeUnmount, onMounted } from 'vue'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import { vaultKeepService } from '../services/VaultKeepService'
import { closeModals } from '../utils/Modal'
import { keepService } from '../services/KeepService'
export default {
  setup() {
    // On Mounted
    onMounted(() => {
      profileService.getVaults(account.value.id, true)
    })
    // Variables
    const keep = computed(() => AppState.activeKeep)
    const account = computed(() => AppState.account)
    const vaults = computed(() => AppState.activeUserVaults)
    // Functions
    const keepToVault = (vaultId) => {
      vaultKeepService.create({
        vaultId: vaultId,
        keepId: keep.value.id
      })
    }
    // Clear delete
    let deleteTimeout
    const clearDelete = () => {
      clearInterval(deleteTimeout)
      AppState.deleting.keep = false
    }
    // Confirm delete, then delete
    const deleting = computed(() => AppState.deleting.keep)
    const deleteKeep = (e) => {
      e.stopPropagation()
      if (deleting.value) {
        keepService.delete(keep.value.id)
        closeModals()
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
      keep,
      vaults,
      account,
      deleting,
      // Functions
      keepToVault,
      closeModals,
      clearDelete,
      deleteKeep
    }
  }
}
</script>
<style scoped>
@import "../assets/css/modal-keep.css";
</style>
