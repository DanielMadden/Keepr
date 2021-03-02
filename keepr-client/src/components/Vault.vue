<template lang="">
  <div class="vault"
       :style="`background: linear-gradient( rgba(0, 0, 0, 0.0), rgba(0, 0, 0, 0)), url('${vault.img}') no-repeat center center /cover; overflow-y: hidden`"
       @click="viewVault()"
  >
    <!-- <img class="vault-img" :src="vault.img" /> -->
    <div class="vault-darken"></div>
    <div class="vault-name">
      {{ vault.name }}
    </div>
  </div>
</template>
<script>
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const travel = () => {
      router.push('/vault/' + props.vault.id)
    }
    const viewVault = () => {
      AppState.activeVault = props.vault
      vaultService.getVault(props.vault.id)
      vaultService.getKeeps(props.vault.id)
      travel()
    }
    return {
      // Variables
      // Functions
      travel,
      viewVault
    }
  }
}
</script>
<style scoped>
@import "../assets/css/vault.css";
</style>
