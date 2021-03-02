<template lang="">
  <div class="row">
    <div class="col-12 d-flex flex-column">
      <h1>New Vault</h1>
      <span>Title</span>
      <input type="text"
             v-model="form.name"
      />
      <span>Image Url</span>
      <input type="text"
             v-model="form.img"
      />
      <span>Description</span>
      <textarea v-model="form.description" />
      <div>
        <input type="checkbox"
               v-model="form.isPrivate"
        /> Private?
      </div>
      <div class="d-flex justify-content-end">
        <button class="modal-add-button-create"
                @click="createVault()"
        >
          Create
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { reactive } from 'vue'
import { vaultService } from '../services/VaultService'
export default {
  setup() {
    // Variables
    const form = reactive({
      name: '',
      img: '',
      description: '',
      isPrivate: false
    })
    // Functions
    const createVault = () => {
      vaultService.create(form)
    }
    return {
      // Variables
      form,
      // Functions
      createVault
    }
  }
}
</script>
<style scoped>
@import "../assets/css/modal-add.css";
</style>
