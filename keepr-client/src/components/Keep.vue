<template lang="">
  <div class="keep-glitch-protection">
    <div class="keep"
         @click="viewKeep()"
    >
      <img class="keep-img" :src="keep.img" />
      <div class="keep-darken"></div>
      <div class="keep-name-pos">
        <div class="keep-name">
          {{ keep.name }}
        </div>
      </div>
      <button class="keep-profile d-flex justify-content-center align-items-center"
              :style="`background: 0, 0, url('${keep.creator.picture}') no-repeat center center /cover; overflow-y: hidden`"
              @click="openProfile($event)"
      >
      </button>
      <button class="keep-remove d-flex justify-content-center align-items-center"
              v-if="page == 'Vault' && keep.creator.id == account.id"
              @click="removeFromVault($event)"
      >
        <i class="fas fa-minus"></i>
      </button>
    </div>
  </div>
</template>
<script>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { vaultKeepService } from '../services/VaultKeepService'
import { openModal } from '../utils/Modal'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    },
    page: {
      type: String,
      default: 'Home'
    }
  },
  setup(props) {
    // Variables
    const router = useRouter()
    const account = computed(() => AppState.account)
    // Functions
    const travel = () => {
      router.push('/keep/' + props.keep.id)
    }
    const viewKeep = () => {
      AppState.activeKeep = props.keep
      keepService.getOne(props.keep.id)
      // TODO open the modal
      openModal('keep')
    }
    const openProfile = (e) => {
      e.stopPropagation()
      router.push('/profile/' + props.keep.creator.id)
    }
    const removeFromVault = (e) => {
      e.stopPropagation()
      vaultKeepService.delete(props.keep.vaultKeepId)
    }
    return {
      // Variables
      account,
      // Functions
      travel,
      viewKeep,
      openProfile,
      removeFromVault
    }
  }
}
</script>
<style scoped>
@import "../assets/css/keep.css";
</style>
