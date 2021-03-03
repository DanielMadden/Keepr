<template>
  <div
    id="myModal-screen"
    class="d-flex justify-content-center py-5"
    :class="{
      show: modalState.show,
      hide: !modalState.show,
    }"
  >
    <div id="myModal-clickoff" @click="closeModals"></div>
    <div
      id="myModal-container"
      class="container"
      :class="{
        adding: modalState.active_add_vault || modalState.active_add_keep,
      }"
    >
      <!-- Insert all modal components here with their respective v-if's -->
      <!-- <modal-group-info v-if="modalChoice.groupInfo"></modal-group-info> -->
      <transition name="fade">
        <Modal-Keep v-if="modalState.active_keep"></Modal-Keep>
      </transition>
      <transition name="fade">
        <Modal-Add-Vault v-if="modalState.active_add_vault"></Modal-Add-Vault>
      </transition>
      <transition name="fade">
        <Modal-Add-Keep v-if="modalState.active_add_keep"></Modal-Add-Keep>
      </transition>
    </div>
  </div>
</template>
<script>
import { computed } from 'vue'
import { closeModals, ModalState } from '../utils/Modal'
export default {
  setup() {
    const modalState = computed(() => ModalState)
    return {
      modalState,
      closeModals
    }
  }
}
</script>
<style scoped>
@import "../assets/css/modals.css";
</style>
