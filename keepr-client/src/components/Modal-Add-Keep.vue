<template lang="">
  <div class="row">
    <div class="col-12 d-flex flex-column">
      <h1>New Keep</h1>
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
      <span>Tags</span>
      <input type="text"
             v-model="form.tagInput"
      />
      <div class="d-flex justify-content-end">
        <button class="modal-add-button-create"
                @click="createKeep()"
        >
          Create
        </button>
      </div>
      {{ form }}
    </div>
  </div>
</template>
<script>
import { computed, reactive } from 'vue'
import { keepService } from '../services/KeepService'
import { closeModals } from '../utils/Modal'
export default {
  setup() {
    // Variables
    const form = reactive({
      name: '',
      img: '',
      description: '',
      tagInput: '',
      tags: computed(() => form.tagInput.split(', ').join(',').split(',').filter(str => str !== ''))
    })
    // Functions
    const createKeep = () => {
      keepService.create(form)
      closeModals()
    }
    return {
      // Variables
      form,
      // Functions
      createKeep
    }
  }
}
</script>
<style scoped>
@import "../assets/css/modal-add.css";
</style>
